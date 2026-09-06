using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    private Action<UpgradeData> onSelectedCallback;

    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button selectButton;
    private UpgradeData currentUpgrade;
    


    private void Awake()
    {
        selectButton.onClick.AddListener(OnCardClicked);
    }


    public void Setup(UpgradeData upgrade, int nextLevel, Action<UpgradeData> onSelected)
    {
        currentUpgrade = upgrade;
        onSelectedCallback = onSelected;

        if (iconImage != null)
        {
            iconImage.sprite = upgrade.GetIcon();
            iconImage.enabled = upgrade.GetIcon() != null;
        }

        if (titleText != null) 
            titleText.text = upgrade.GetTitle();

        if (descriptionText != null) 
            descriptionText.text = upgrade.GetDescription();

        if (levelText != null)
            levelText.text = upgrade.GetMaxLevel() > 1 ? $"Lvl {nextLevel}/{upgrade.GetMaxLevel()}" : "New!";
    }


    private void OnCardClicked()
    {
        onSelectedCallback?.Invoke(currentUpgrade);
    }
}