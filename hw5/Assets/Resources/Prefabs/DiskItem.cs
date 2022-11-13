using System;
using UnityEngine;

[System.Serializable]
public class Attributes{
    [Tooltip("大小")]
    public int size;
    [Tooltip("速度")]
    public int speed;
    [Tooltip("得分")]
    public int score;
}

[CreateAssetMenu(fileName = "DiskItem", menuName = "(ScritableObject)DiskItem")]
public class DiskItem : ScriptableObject
{
    public string Name;
    public string Desc;
    [Tooltip("飞碟属性")]
    public Attributes attributes;
}
