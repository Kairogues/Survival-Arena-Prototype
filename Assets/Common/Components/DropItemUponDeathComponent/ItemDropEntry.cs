using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct DropAmountChance
{
    public int amount;
    public float chance;
}

[System.Serializable]
public struct ItemDropEntry
{
    [SerializeField] private List<DropAmountChance> itemDropList;
    [SerializeField] private Pickupable itemToDrop;
    public Pickupable GetItemToDrop()
    {
        return itemToDrop;
    }

    public int EvaluateDropAmount()
    {
        if (itemToDrop == null || itemDropList == null || itemDropList.Count == 0)
        {
            return 0;
        }

        int random = Random.Range(0, 100);
        float accumulation = 0;
        foreach (DropAmountChance itemDropEntry in itemDropList)
        {
            if (random < (accumulation + itemDropEntry.chance))
            {
                return itemDropEntry.amount;
            }

            accumulation += itemDropEntry.chance;
        }

        return itemDropList[0].amount;
    }
}
