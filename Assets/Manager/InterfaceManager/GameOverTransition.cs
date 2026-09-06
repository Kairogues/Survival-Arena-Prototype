using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GameOverTransition : MonoBehaviour
{
    private Image panelImage;

    [System.Serializable]
    public struct ColorKeyframe
    {
        public Color color;
        [Range(0f, 1f)] public float alpha;
        public float duration;
    }

    [SerializeField] private ColorKeyframe[] sequence;



    private void Awake()
    {
        panelImage = GetComponent<Image>();
    }


    private void Start()
    {
        InterfaceManager.Instance.SetGameOverTransition(this);
    }


    public void PlayColorAnimation(Action onComplete = null)
    {
        StartCoroutine(AnimateColors(onComplete));
    }


    private IEnumerator AnimateColors(Action onComplete)
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            Color startColor = panelImage.color;

            Color targetColor = sequence[i].color;
            targetColor.a = sequence[i].alpha;

            float duration = sequence[i].duration;
            float elapsed = 0f;

            if (duration > 0f)
            {
                while (elapsed < duration)
                {
                    elapsed += Time.unscaledDeltaTime;
                    panelImage.color = Color.Lerp(startColor, targetColor, Mathf.Clamp01(elapsed / duration));
                    yield return null;
                }
            }

            panelImage.color = targetColor;
        }

        onComplete?.Invoke();
    }
}