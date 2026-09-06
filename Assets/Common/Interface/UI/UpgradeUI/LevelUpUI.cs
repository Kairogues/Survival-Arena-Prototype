using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private UpgradeCardUI cardPrefab;

    private List<UpgradeCardUI> spawnedCards = new();
    private UpgradeReceiverComponent currentReceiver;

    private void Awake()
    {
        rootPanel.SetActive(false);
    }


    private void Start()
    {
        InterfaceManager.Instance.SetLevelUpUI(this);
    }


    public void Show(List<UpgradeData> options, UpgradeReceiverComponent receiver)
    {
        currentReceiver = receiver;

        ClearCards();

        foreach (UpgradeData upgrade in options)
        {
            UpgradeCardUI card = Instantiate(cardPrefab, cardContainer);
            int nextLevel = receiver.GetUpgradeLevel(upgrade) + 1;

            card.Setup(upgrade, nextLevel, OnOptionSelected);
            spawnedCards.Add(card);
        }

        rootPanel.SetActive(true);
        Time.timeScale = 0f;
    }


    private void OnOptionSelected(UpgradeData selectedUpgrade)
    {
        Time.timeScale = 1f;
        rootPanel.SetActive(false);

        if (currentReceiver != null && selectedUpgrade != null)
        {
            currentReceiver.ApplyUpgrade(selectedUpgrade);
        }

        ClearCards();
    }


    private void ClearCards()
    {
        for (int i = 0; i < spawnedCards.Count; i++)
        {
            if (spawnedCards[i] != null)
            {
                Destroy(spawnedCards[i].gameObject);
            }
        }
        spawnedCards.Clear();
    }
}