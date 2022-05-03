using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;


public class OnlineShop : MonoBehaviour
{
    public DucklingStats stats;

    public FirstPersonController playerController;

    public float currentBalance, foodPrice, toyPrice;

    public List<GameObject> foodList, toyList;

    public TextMeshProUGUI currentBalanceText, purchaseStatusText, toyPriceText, foodPriceText, inputText;
    public GameObject shopPanel, frontDoorPos, inputPanel;

    //public ObjectPositions objectPositions;

    public Transform loadObjectsPosition;

    public AudioSource audioSource;
    public AudioClip doorbellAudio, declinedAudio;

    void Start()
    {
        currentBalance = stats.playerMoney;

        toyPriceText.text = "Buy Toy - $" + toyPrice.ToString();
        foodPriceText.text = "Buy Food - $" + foodPrice.ToString();

        shopPanel.SetActive(false);

        //if (objectPositions.isLoaded)
        //{
        //    foreach (Vector3 position in objectPositions.foodPositions)
        //    {
        //        GameObject newFood = Instantiate(foodList[Random.Range(0, foodList.Count)], loadObjectsPosition.transform);
        //        newFood.transform.parent = null;
        //        newFood.transform.position = position;
        //    }

        //    foreach (Vector3 position in objectPositions.toyPositions)
        //    {
        //        GameObject newToy = Instantiate(toyList[Random.Range(0, toyList.Count)], loadObjectsPosition.transform);
        //        newToy.transform.parent = null;
        //        newToy.transform.position = position;
        //    }
        //}
    }

    void Update()
    {
        if (shopPanel.activeSelf)
        {
            currentBalance = stats.playerMoney;
            currentBalanceText.text = "Current Balance: " + currentBalance.ToString(".00");

            inputPanel.SetActive(true);

            inputText.text = "Press Spacebar to Close";

            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Cancel"))
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

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(doorbellAudio, audioSource.volume);
            }

            stats.playerMoney -= toyPrice;
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(declinedAudio, audioSource.volume);
            }
            purchaseStatusText.text = "Insufficent Funds";
        }
    }

    public void BuyFood()
    {
        if (currentBalance > foodPrice)
        {
            purchaseStatusText.text = "Food Purchased!";

            GameObject newFood = Instantiate(foodList[Random.Range(0, foodList.Count)], frontDoorPos.transform);
            newFood.transform.parent = null;

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(doorbellAudio, audioSource.volume);
            }

            stats.playerMoney -= foodPrice;
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(declinedAudio, audioSource.volume);
            }
            purchaseStatusText.text = "Insufficent Funds";
        }
    }
}
