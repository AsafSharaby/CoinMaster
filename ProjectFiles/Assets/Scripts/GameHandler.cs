using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
	public static GameHandler instance;

	public static event Action HandlePulled = delegate { };

	[Header("SlotMachine")]
	[SerializeField] private Row[] rows;
	[SerializeField] private Transform handle;
	[SerializeField] private GameObject[] shileds;

	[Header("Text")]
	public Text coinsText;
	[SerializeField] private Text starText;
	[SerializeField] private Text villegeSliderText;
	[SerializeField] private Text amoutSliderText;

	[Header("Slider")]
	public Slider villegeSlider;
	public Slider amountSlider;

	[Header("Slider")]
	[SerializeField] private Image prizeImage;
	[SerializeField] private Sprite[] prizeImages;


	private int stars = 0;
	private int amout = 50;
	private int shiledAmout  = 0;
	public int coins = 0;
	private Color tempColor;

	private bool resultsChacked = false;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		amountSlider.value = amout;
		coinsText.text = "$ " + coins.ToString();

	}

	private void Update()
	{
		if(!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
			resultsChacked = false;

		if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChacked)
		{
			ChackResult();
			FindObjectOfType<SwipePanels>().enabled = true;
			coins += UnityEngine.Random.Range(200, 1000);
			coinsText.text = "$ " + coins.ToString();
		}
	}

	private void OnMouseDown()
	{
		if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
			StartCoroutine(PullHandle());
	}

	private IEnumerator PullHandle()
	{
		FindObjectOfType<SwipePanels>().enabled = false;

		ChekImageAlpha(0);

		for (int i = 0; i < 15; i += 5)
		{
			handle.Rotate(0, 0, i);
			yield return new WaitForSeconds(0.1f);
		}

		HandlePulled();
		ChackAmout();

		for (int i = 0; i < 15; i += 5)
		{
			handle.Rotate(0, 0, -i);
			yield return new WaitForSeconds(0.1f);
		}
	}

	private void ChackResult()
	{
		if (rows[0].stoppedSlot == "Coin" && rows[1].stoppedSlot == "Coin" && rows[2].stoppedSlot == "Coin")
			CheckPrizeImage(200, 0);
		else if (rows[0].stoppedSlot == "Sword" && rows[1].stoppedSlot == "Sword" && rows[2].stoppedSlot == "Sword")
			CheckPrizeImage(300, 1);
		else if (rows[0].stoppedSlot == "Exle" && rows[1].stoppedSlot == "Exle" && rows[2].stoppedSlot == "Exle")
		{
			CheckPrizeImage(600, 2);
			amout += 5;
		}
		else if (rows[0].stoppedSlot == "Sheild" && rows[1].stoppedSlot == "Sheild" && rows[2].stoppedSlot == "Sheild")
		{
			CheckPrizeImage(1500, 3);
			CheckShield();
		}
		else if (rows[0].stoppedSlot == "Skull" && rows[1].stoppedSlot == "Skull" && rows[2].stoppedSlot == "Skull")
			CheckPrizeImage(3000, 4);
		else if (rows[0].stoppedSlot == "Chest" && rows[1].stoppedSlot == "Chest" && rows[2].stoppedSlot == "Chest")
			CheckPrizeImage(5000, 5);



		resultsChacked = true;
	}

	public void PurchaseSuccsed()
	{
		stars++;
		starText.text = stars.ToString();
		villegeSlider.value = stars;
		villegeSliderText.text = stars + " / " + 20;
	}

	public void ChackAmout()
	{
		amout--;
		amountSlider.value = amout;
		amoutSliderText.text = amout + " / " + 50;
	}

	public void CheckShield()
	{
		shiledAmout += 1;

		for (int i = 0; i < shileds.Length; i++)
		{
			if(i<shiledAmout)
			shileds[i].SetActive(true);
		}
	}

	private void CheckPrizeImage(int amout,int i)
	{
		coins += amout;

		ChekImageAlpha(1);
		prizeImage.sprite = prizeImages[i];
	}

	private void ChekImageAlpha(float cupacity)
	{
		tempColor = prizeImage.color;
		tempColor.a = cupacity;
		prizeImage.color = tempColor;
	}
}
