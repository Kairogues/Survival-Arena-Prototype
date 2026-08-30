using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    // oldValue, currentValue, maxValue
    public event Action<float, float, float> StatChanged;

    [SerializeField] private StatType statType;
    public StatType GetStatType()
    {
        return statType;
    }

    [SerializeField] private float currentValue;
    public float GetCurrentValue()
    {
        return currentValue;
    }

    [SerializeField] private bool useMaxValue;
    [SerializeField] private float maxValue;
    public float GetMaxValue()
    {
        return maxValue;
    }
    public void SetMaxValue(float newMaxValue) 
    {
        maxValue = newMaxValue;
    }


    public Stat(StatType type = StatType.HEALTH, float initCurrentValue = 0, bool initUseMaxValue = false, float initMaxValue = 0)
    {
        statType = type;
        currentValue = initCurrentValue;
        useMaxValue = initUseMaxValue;
        maxValue = initMaxValue;
    }


    public Stat(Stat original)
    {
        statType = original.statType;
        currentValue = original.currentValue;
        useMaxValue = original.useMaxValue;
        maxValue = original.maxValue;
    }


    public void MaximizeCurrentStat()
    {
        currentValue = maxValue;
    }


    public void UpdateStat(float newValue) 
    {
        float oldValue = currentValue;
        currentValue = newValue;
        if (useMaxValue && (currentValue > maxValue))
        {
            currentValue = maxValue;
        }

        StatChanged?.Invoke(oldValue, currentValue, maxValue);
    }
}
