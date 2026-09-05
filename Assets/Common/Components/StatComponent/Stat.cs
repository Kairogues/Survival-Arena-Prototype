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
    [SerializeField] private float baseValue;
    public float GetBaseValue()
    {
        return baseValue;
    }
    public void SetBaseValue(float newBaseValue)
    {
        baseValue = newBaseValue;
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


    public Stat(StatType type = StatType.HEALTH, float initBaseValue = 0, float initCurrentValue = -1, bool initUseMaxValue = false, float initMaxValue = 0)
    {
        statType = type;
        baseValue = initBaseValue;
        // If initCurrentValue is left default (-1f), initialize currentValue to match baseValue
        currentValue = (initCurrentValue < 0) ? initBaseValue : initCurrentValue;
        useMaxValue = initUseMaxValue;
        maxValue = initMaxValue;
    }


    public Stat(Stat original)
    {
        statType = original.statType;
        baseValue = original.baseValue;
        currentValue = original.currentValue;
        useMaxValue = original.useMaxValue;
        maxValue = original.maxValue;
    }


    public void StatCopy(Stat stat)
    {
        currentValue = stat.currentValue;
        baseValue = stat.baseValue;
        useMaxValue = stat.useMaxValue;
        maxValue = stat.maxValue;
    }


    public void MaximizeCurrentStat()
    {
        currentValue = maxValue;
    }


    public void ResetToBaseValue()
    {
        UpdateStat(baseValue);
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
