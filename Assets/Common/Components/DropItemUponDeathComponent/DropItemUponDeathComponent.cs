using UnityEngine;
using System.Collections.Generic;

public class DropItemUponDeathComponent : MonoBehaviour
{
    private const float DROP_DISTANCE = 0.1F;
    [SerializeField] private LootTable lootTable;
    [SerializeField] private LifeComponent lifeComponent;



    private void OnEnable()
    {
        lifeComponent.Died += DropItems;
    }


    private void OnDisable()
    {
        lifeComponent.Died -= DropItems;
    }


    private void DropItems()
    {
        if (lootTable.lootTable.Count == 0)
        {
            return;
        }

        foreach (ItemDropEntry item in lootTable.lootTable)
        {
            int dropAmount = item.EvaluateDropAmount();

            for (int i = 0; i < dropAmount; i++)
            {
                float randomDisplacementX = Random.Range(-DROP_DISTANCE, DROP_DISTANCE);
                float randomDisplacementY = Random.Range(-DROP_DISTANCE, DROP_DISTANCE);

                Vector3 spawnPosition = new Vector3(
                        transform.position.x + randomDisplacementX, 
                        transform.position.y + randomDisplacementY, 
                        0f
                );

                GameManager.Instance.poolManager.Spawn(
                        item.gameObject,
                        spawnPosition,
                        transform.rotation,
                        GameManager.Instance.entityManager.transform);
            }
        }
    }
}
