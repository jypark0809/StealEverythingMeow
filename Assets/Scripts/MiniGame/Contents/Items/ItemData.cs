using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int gold;
    public int Gold { get { return gold; } }
    [SerializeField]
    private int exp;
    public int Exp { get { return exp; } }
}
