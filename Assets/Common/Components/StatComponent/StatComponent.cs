using System;
using UnityEngine;
using System.Collections.Generic;

public class StatComponent : MonoBehaviour
{
    [SerializeField] private StatSet statSetPrototype;
    private Dictionary<StatType, Stat> statDictionary = new();
    private Dictionary<int, StatBuff> buffDictionary = new Dictionary<int, StatBuff>();
    private int nextBuffID = 0;



    private void Awake()
    {
        SetUpStatDictionary();
    }



    public void SetUpStatDictionary()
    {
        foreach (Stat stat in statSetPrototype.GetStatList())
        {
            Stat runtimeStat = new Stat(stat);
            statDictionary.Add(runtimeStat.GetStatType(), runtimeStat);
        }
    }


    public void AddStat(Stat newStat)
    {
        if (GetStatInCurrentList(newStat.GetStatType()) != null)
        {
            Debug.LogWarning("Duplicated Stat Type found!");
            return;
        }

        statDictionary.Add(newStat.GetStatType(), newStat);
    }


    public bool RemoveStat(StatType type)
    {
        Stat target = GetStatInCurrentList(type);
        
        if (target != null)
        {
            statDictionary.Remove(type);
            return true;
        }
        
        return false;
    }


    public Stat GetStatInCurrentList(StatType type)
    {
        Stat returnStat = statDictionary[type];

        if (returnStat == null)
        {
            return null;
        }
        
        return returnStat;
    }


    public void RecalculateStatAfterBuff(StatType type)
    {
        Stat stat = GetStatInCurrentList(type);
        if (stat == null)
        {
            Debug.LogWarning("Trying to recalculate non-existing stat!");
            return;
        }

        float addAmount = 0.0f;
        float multiplyAmount = 0.0f;

        foreach (StatBuff buff in buffDictionary.Values)
        {
            if (buff.GetStatType() == type)
            {
                if (buff.GetBuffType() == StatBuffType.ADD)
                {
                    addAmount += buff.GetBuffAmount();
                } else if (buff.GetBuffType() == StatBuffType.MULTIPLY)
                {
                    multiplyAmount += buff.GetBuffAmount();
                }
            }
        }

        float currentStatValue = GetStatInCurrentList(type).GetCurrentValue();
        currentStatValue += addAmount;
        currentStatValue *= 1.0f + multiplyAmount;
        GetStatInCurrentList(type).UpdateStat(currentStatValue);
    }


    // Temporary, work just fine but I really do not like this
    public int AddBuff(StatBuff newBuff)
    {
        int currentBuffID = nextBuffID;
        newBuff.SetBuffID(currentBuffID);
        
        buffDictionary.Add(currentBuffID, newBuff);

        RecalculateStatAfterBuff(newBuff.GetStatType());
        
        nextBuffID++;

        return currentBuffID;
    }


    // Temporary, work just fine but I really do not like this
    public bool RemoveBuff(int buffIDToRemove)
    {
        if (buffDictionary.ContainsKey(buffIDToRemove))
        {
            StatType buffType = buffDictionary[buffIDToRemove].GetStatType();
            buffDictionary.Remove(buffIDToRemove);
            RecalculateStatAfterBuff(buffType);
            return true;
        }

        return false;
    }


    public Stat GetStat(StatType type)
    {
        Stat stat = GetStatInCurrentList(type);
        if (stat == null)
        {
            Debug.LogWarning("Trying to access non-existing stat!");
            return null;
        }

        return stat;
    }


    public void SubscribeToStat(StatType type, Action<float, float, float> listener)
    {
        Stat stat = GetStat(type);
        stat.StatChanged += listener;
    }


    public void UnSubscribeToStat(StatType type, Action<float, float, float> listener)
    {
        Stat stat = GetStat(type);
        stat.StatChanged -= listener;
    }
}
