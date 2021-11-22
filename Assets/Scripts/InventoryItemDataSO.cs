using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new Inventory Item", menuName = "ScriptableObjects/Inventory Item")]
public class InventoryItemDataSO : ScriptableObject
{
    public string IconName;
    public string Name;
    public string Description;
    public int Stat;
}