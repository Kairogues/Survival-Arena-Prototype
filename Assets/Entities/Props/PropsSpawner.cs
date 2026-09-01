using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BulkRockSpawner2D : EditorWindow
{
    private GameObject targetParentObject;

    private Vector2 mapCenter = Vector2.zero;
    private Vector2 mapSize = new Vector2(1000f, 1000f);
    private LayerMask groundLayer2D = ~0;

    private List<GameObject> propPrefabs = new();
    private Vector2 scrollPos;

    // Generation rules
    private int rockCount = 500;
    private bool requireGroundCollision = true;

    [MenuItem("Tools/Level Design/Bulk Rock Spawner (2D)")]
    public static void ShowWindow()
    {
        GetWindow<BulkRockSpawner2D>("Rock Spawner 2D");
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        GUILayout.Label("Target Container (Mandatory)", EditorStyles.boldLabel);
        targetParentObject = (GameObject)EditorGUILayout.ObjectField("Target Parent GameObject", targetParentObject, typeof(GameObject), true);

        if (targetParentObject == null)
        {
            EditorGUILayout.HelpBox("Drag and drop a GameObject from your Scene Hierarchy here.", MessageType.Warning);
        }

        EditorGUILayout.Space(5);
        GUILayout.Label("2D Area Settings (X, Y)", EditorStyles.boldLabel);
        mapCenter = EditorGUILayout.Vector2Field("Map Center (X, Y)", mapCenter);
        mapSize = EditorGUILayout.Vector2Field("Map Size (Width, Height)", mapSize);
        groundLayer2D = LayerMaskField("Ground Layer 2D", groundLayer2D);

        requireGroundCollision = EditorGUILayout.Toggle(new GUIContent("Require Ground Hit", "If enabled, only places rocks on top of an existing 2D Collider."), requireGroundCollision);

        EditorGUILayout.Space(5);
        GUILayout.Label("Rock Prefabs List", EditorStyles.boldLabel);

        int newCount = Mathf.Max(0, EditorGUILayout.IntField("Prefab Count", propPrefabs.Count));
        while (newCount > propPrefabs.Count) propPrefabs.Add(null);
        while (newCount < propPrefabs.Count) propPrefabs.RemoveAt(propPrefabs.Count - 1);

        for (int i = 0; i < propPrefabs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            propPrefabs[i] = (GameObject)EditorGUILayout.ObjectField($"Rock {i + 1}", propPrefabs[i], typeof(GameObject), false);
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                propPrefabs.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("+ Add Prefab Slot", GUILayout.Height(20)))
        {
            propPrefabs.Add(null);
        }

        EditorGUILayout.Space(5);
        GUILayout.Label("Generation Count", EditorStyles.boldLabel);
        rockCount = EditorGUILayout.IntField("Total Rocks", rockCount);

        EditorGUILayout.Space(15);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Generate Rocks (2D)", GUILayout.Height(35)))
        {
            GenerateRocks2D();
        }

        GUI.backgroundColor = new Color(1f, 0.4f, 0.4f);
        if (GUILayout.Button("Clear Rocks Inside Target", GUILayout.Height(25)))
        {
            ClearRocks();
        }
        GUI.backgroundColor = Color.white;

        EditorGUILayout.EndScrollView();
    }

    private void GenerateRocks2D()
    {
        if (targetParentObject == null)
        {
            EditorUtility.DisplayDialog("Missing Target", "Please assign 'Target Parent GameObject' first.", "OK");
            return;
        }

        List<GameObject> validPrefabs = propPrefabs.FindAll(p => p != null);
        if (validPrefabs.Count == 0)
        {
            EditorUtility.DisplayDialog("Missing Prefabs", "Please assign at least one rock prefab.", "OK");
            return;
        }

        Undo.IncrementCurrentGroup();
        int undoGroup = Undo.GetCurrentGroup();
        Undo.SetCurrentGroupName("Bulk Place Rocks 2D");

        float halfWidth = mapSize.x * 0.5f;
        float halfHeight = mapSize.y * 0.5f;
        int spawnedCount = 0;

        for (int i = 0; i < rockCount; i++)
        {
            // Random point across the 2D plane (X, Y)
            float randomX = Random.Range(mapCenter.x - halfWidth, mapCenter.x + halfWidth);
            float randomY = Random.Range(mapCenter.y - halfHeight, mapCenter.y + halfHeight);
            Vector2 randomPos2D = new Vector2(randomX, randomY);

            // Check if point touches BoxCollider2D
            if (requireGroundCollision)
            {
                Collider2D hitCollider = Physics2D.OverlapPoint(randomPos2D, groundLayer2D);
                if (hitCollider == null)
                {
                    continue; // Skip if point is outside the collider
                }
            }

            GameObject chosenPrefab = validPrefabs[Random.Range(0, validPrefabs.Count)];
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(chosenPrefab, targetParentObject.transform);

            instance.transform.position = new Vector3(randomPos2D.x, randomPos2D.y, 0f);
            instance.transform.rotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;

            Undo.RegisterCreatedObjectUndo(instance, "Spawn Rock 2D");
            spawnedCount++;
        }

        Undo.CollapseUndoOperations(undoGroup);
        Debug.Log($"[BulkRockSpawner2D] Successfully placed {spawnedCount}/{rockCount} rocks inside '{targetParentObject.name}'.");
    }

    private void ClearRocks()
    {
        if (targetParentObject == null)
        {
            EditorUtility.DisplayDialog("Missing Target", "Please assign the target GameObject first.", "OK");
            return;
        }

        Undo.RegisterFullObjectHierarchyUndo(targetParentObject, "Clear Rocks");

        for (int i = targetParentObject.transform.childCount - 1; i >= 0; i--)
        {
            Undo.DestroyObjectImmediate(targetParentObject.transform.GetChild(i).gameObject);
        }

        Debug.Log($"[BulkRockSpawner2D] Cleared all rocks inside '{targetParentObject.name}'.");
    }

    private static LayerMask LayerMaskField(string label, LayerMask layerMask)
    {
        List<string> layers = new();
        List<int> layerNumbers = new();

        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (!string.IsNullOrEmpty(layerName))
            {
                layers.Add(layerName);
                layerNumbers.Add(i);
            }
        }

        int maskWithoutEmpty = 0;
        for (int i = 0; i < layerNumbers.Count; i++)
        {
            if (((1 << layerNumbers[i]) & layerMask.value) > 0)
                maskWithoutEmpty |= 1 << i;
        }

        maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers.ToArray());

        int mask = 0;
        for (int i = 0; i < layerNumbers.Count; i++)
        {
            if ((maskWithoutEmpty & (1 << i)) > 0)
                mask |= 1 << layerNumbers[i];
        }

        layerMask.value = mask;
        return layerMask;
    }
}