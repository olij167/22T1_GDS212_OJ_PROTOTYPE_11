using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;


public class OnlineShop : MonoBehaviour
{
    public DucklingStats stats;

    public RigidbodyFirstPersonController playerController;

    public float currentBalance, foodPrice, toyPrice;

    public List<GameObject> foodList, toyList;

    public TextMeshProUGUI currentBalanceText, purchaseStatusText, toyPriceText, foodPriceText, inputText;
    public GameObject shopPanel, frontDoorPos, inputPanel;




    void Start()
    {
        currentBalance = stats.playerMoney;

        toyPriceText.text = "Buy Toy - $" + toyPrice.ToString();
        foodPriceText.text = "Buy Food - $" + foodPrice.ToString();

        shopPanel.SetActive(false);
    }

    void Update()
    {
        if (shopPanel.activeSelf)
        {
            currentBalanceText.text = "Current Balance: " + currentBalance.ToString(".00");

            inputPanel.SetActive(true);

            inputText.text = "Press Spacebar to Close";

            if (Input.GetButtonDown("Jump"))
            {
                shopPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                playerController.enabled = true;
            }
        }
    }

    public void BuyToy()
    {
        if (currentBalance > toyPrice)
        {
            purchaseStatusText.text = "Toy Purchase!";

            GameObject newToy = Instantiate(toyList[Random.Range(0, toyList.Count)], frontDoorPos.transform);
            newToy.transform.parent = null;

            // play doorbell sound effect

            currentBalance -= toyPrice;
        }
        else purchaseStatusText.text = "Insufficent Funds";
    }

    public void BuyFood()
    {
        if (currentBalance > foodPrice)
        {
            purchaseStatusText.text = "Food Purchased!";

            GameObject newFood = Instantiate(foodList[Random.Range(0, foodList.Count)], frontDoorPos.transform);
            newFood.transform.parent = null;

            // play doorbell sound effect

            currentBalance -= foodPrice;
        }
        else purchaseStatusText.text = "Insufficent Funds";
    }
}
