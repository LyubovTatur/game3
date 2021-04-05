using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class database : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public Sprite image;
    public GameObject model;
    public int price;
    public int sellPrice;
}
