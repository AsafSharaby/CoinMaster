using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopItem", menuName = "NewItem",order = 1)]
public class Item : ScriptableObject
{
	public int cost;
	public Sprite sprite;
	public GameObject objToShow;
}

