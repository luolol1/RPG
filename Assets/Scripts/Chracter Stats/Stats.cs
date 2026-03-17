using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats 
{
    [SerializeField] private int baseValue;

    [SerializeField] private List<int> modifiers = new List<int>();
    public int GetValue()
    {
        int finalValue= baseValue;
        for(int i=0;i<modifiers.Count;i++)
        {
            finalValue+=modifiers[i];
        }
        return finalValue;
    }

    public void SetValue(int value)
    {
        baseValue = value;
    }
    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }
    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
    }
}
