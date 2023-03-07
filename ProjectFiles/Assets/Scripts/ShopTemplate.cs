using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
	private GameHandler gameHandler;

	private Text costText;
	private Button buyButton;

	public Item[] items;

	public Image currentImage;
	public GameObject curPosition;

	public int indexnum = 0;
	private int rating;
	public Image[] starsImages;


	private void Start()
	{
		buyButton = GetComponentInChildren<Button>();
		buyButton.onClick.AddListener(PurchaseItem);

		costText = GetComponentInChildren<Text>();

		gameHandler = GameHandler.instance;

		UpdateCostUI();
		//CheckPurchaseable();
		currentImage.sprite = items[indexnum].sprite;
	}

	private void Update()
	{
		UpdateCostUI();
		if (indexnum >= 4)
		{
			costText.text = "Complited";
			buyButton.interactable = false;
		}
	}

	private void PurchaseItem()
	{
		if (gameHandler.coins >= items[indexnum].cost)
		{
			gameHandler.coins = gameHandler.coins - items[indexnum].cost;
			gameHandler.coinsText.text = gameHandler.coins.ToString();

			//CheckPurchaseable();
			UpdateCostUI();

			UpdateCurrentImage();
			gameHandler.PurchaseSuccsed();
			indexnum++;
		}
	}

	private void CheckPurchaseable()
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (gameHandler.coins >= items[i].cost)
				buyButton.interactable = true;
			else
				buyButton.interactable = false;

			if (indexnum >= 4)
				buyButton.interactable = false;
		}
	}

	private void UpdateCostUI()
	{
		costText.text = items[indexnum].cost.ToString();
	}

	private void UpdateCurrentImage()
	{
		
		currentImage.sprite = items[indexnum+1].sprite;

		GameObject temp = Instantiate(items[indexnum].objToShow, curPosition.transform.position, Quaternion.identity);
		temp.transform.SetParent(curPosition.transform);

		if (curPosition.transform.childCount ==2)
			Destroy(curPosition.transform.GetChild(0).gameObject);

		SetStarRating(indexnum);
	}

	public void SetStarRating(int index)
	{
		rating = index + 1;
		for (int i = 0; i < starsImages.Length; i++)
		{
			if (i <= index)
				starsImages[i].gameObject.SetActive(true);
			else
				starsImages[i].gameObject.SetActive(false);
		}
	}
}
