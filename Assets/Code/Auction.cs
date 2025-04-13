using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Auction : MonoBehaviour
{
    public GameObject player;
    public PlayerAttributes attrib;
    public List<(GameObject, int)> unownedBuildings;
    public GameObject currentBuilding;
    public Property prop;
    public Camera playerCam;
    public int currentPrice;
    public float time;
    public bool swap;
    public int rng;
    [SerializeField] TextMeshProUGUI auctionText;
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI statText;
    [SerializeField] TextMeshProUGUI incomeText;
    [SerializeField] TextMeshProUGUI dailyCostText;

    public GameObject auctionbg;
    // simply for testing
    public GameObject hotel1;
    public int hotel1Price;
    public GameObject hotel2;
    public int hotel2Price;
    public GameObject hotel3;
    public int hotel3Price;

    // sheds

    public GameObject shed1;
    public int shed1Price;
    public GameObject shed2;
    public int shed2Price;
    public GameObject shed3;
    public int shed3Price;
    public GameObject shed4;
    public int shed4Price;
    public GameObject shed5;
    public int shed5Price;
    public GameObject shed6;
    public int shed6Price;
    public GameObject shed7;
    public int shed7Price;
    public GameObject shed8;
    public int shed8Price;
    public GameObject shed9;
    public int shed9Price;
    public GameObject shed10;
    public int shed10Price;
    public GameObject shed11;
    public int shed11Price;
    public GameObject shed12;
    public int shed12Price;
    public GameObject shed13;
    public int shed13Price;
    public GameObject shed14;
    public int shed14Price;
    public GameObject house1;
    public int house1Price;
    public GameObject house2;
    public int house2Price;
    public GameObject house3;
    public int house3Price;
    public GameObject house4;
    public int house4Price;
    public GameObject house5;
    public int house5Price;
    public GameObject house6;
    public int house6Price;
    public GameObject house7;
    public int house7Price;
    public GameObject house8;
    public int house8Price;

    public GameObject mansion1;
    public int mansion1Price;
    public GameObject mansion2;
    public int mansion2Price;
    public GameObject mansion3;
    public int mansion3Price;
    public GameObject apartment1;
    public int apartment1Price;
    public GameObject apartment2;
    public int apartment2Price;
    public GameObject apartment3;
    public int apartment3Price;
    public GameObject apartment4;
    public int apartment4Price;
    public GameObject apartment5;
    public int apartment5Price;
    public GameObject apartment6;
    public int apartment6Price;
    public GameObject apartment7;
    public int apartment7Price;
    public GameObject shop1;
    public int shop1Price;
    public GameObject shop2;
    public int shop2Price;
    public GameObject shop3;
    public int shop3Price;
    public GameObject shop4;
    public int shop4Price;
    public GameObject shop5;
    public int shop5Price;
    public GameObject shop6;
    public int shop6Price;
    public GameObject stadium;
    public int stadiumPrice;
    public GameObject casino;
    public int casinoPrice;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = Time.time;
        swap = true;
        unownedBuildings = new List<(GameObject, int)>();
        attrib = player.GetComponent<PlayerAttributes>();
        // hotels
        unownedBuildings.Add((hotel1, hotel1Price));
        unownedBuildings.Add((hotel2, hotel2Price));
        unownedBuildings.Add((hotel3, hotel3Price));
        // sheds
        unownedBuildings.Add((shed1, shed1Price));
        unownedBuildings.Add((shed2, shed2Price));
        unownedBuildings.Add((shed3, shed3Price));
        unownedBuildings.Add((shed4, shed4Price));
        unownedBuildings.Add((shed5, shed5Price));
        unownedBuildings.Add((shed6, shed6Price));
        unownedBuildings.Add((shed7, shed7Price));
        unownedBuildings.Add((shed8, shed8Price));
        unownedBuildings.Add((shed9, shed9Price));
        unownedBuildings.Add((shed10, shed10Price));
        unownedBuildings.Add((shed11, shed11Price));
        unownedBuildings.Add((shed12, shed12Price));
        unownedBuildings.Add((shed13, shed13Price));
        unownedBuildings.Add((shed14, shed14Price));
        // houses
        unownedBuildings.Add((house1, house1Price));
        unownedBuildings.Add((house2, house2Price));
        unownedBuildings.Add((house3, house3Price));
        unownedBuildings.Add((house4, house4Price));
        unownedBuildings.Add((house5, house5Price));
        unownedBuildings.Add((house6, house6Price));
        unownedBuildings.Add((house7, house7Price));
        unownedBuildings.Add((house8, house8Price));
        // mansions
        unownedBuildings.Add((mansion1, mansion1Price));
        unownedBuildings.Add((mansion2, mansion2Price));
        unownedBuildings.Add((mansion3, mansion3Price));
        // apartments
        unownedBuildings.Add((apartment1, apartment1Price));
        unownedBuildings.Add((apartment2, apartment2Price));
        unownedBuildings.Add((apartment3, apartment3Price));
        unownedBuildings.Add((apartment4, apartment4Price));
        unownedBuildings.Add((apartment5, apartment5Price));
        unownedBuildings.Add((apartment6, apartment6Price));
        unownedBuildings.Add((apartment7, apartment7Price));
        //shops
        unownedBuildings.Add((shop1, shop1Price));
        unownedBuildings.Add((shop2, shop2Price));
        unownedBuildings.Add((shop3, shop3Price));
        unownedBuildings.Add((shop4, shop4Price));
        unownedBuildings.Add((shop5, shop5Price));
        unownedBuildings.Add((shop6, shop6Price));
        // casino
        unownedBuildings.Add((casino, casinoPrice));
        // stadium
        unownedBuildings.Add((stadium, stadiumPrice));
        
    }

    void startAuction()
    {
        rng = Random.Range(0, unownedBuildings.Count);
        currentBuilding = unownedBuildings[rng].Item1;
        currentPrice = unownedBuildings[rng].Item2;
        prop = currentBuilding.GetComponent<Property>();
        playerCam.transform.position = currentBuilding.transform.position;
        statText.text = currentBuilding.name + " Stats";
        incomeText.text = "Income/mo: $" + prop.monthlyIncome;
        dailyCostText.text = "Daily Expenses: $" + prop.dailyExpenses;
        auctionText.text = currentBuilding.name + " is now on Auction for $" + currentPrice;
        purchaseText.text = "Do you want to purchase? Yes (Y) No (N)";
        auctionbg.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        // auction every month
        if(time > 15.0f && swap){
            time = 0.0f;
            startAuction();
            swap = false;
        }
        if(!swap){
            timerText.text = "" + System.Math.Round(15.0f-time,0);
            if(Input.GetKeyDown(KeyCode.Y) && attrib.money >= currentPrice){
                player.GetComponent<PlayerAttributes>().buyProperty(currentBuilding, currentPrice);
                unownedBuildings.RemoveAt(rng);
                time = 15.0f;
            }
            if(Input.GetKeyDown(KeyCode.N)){
                time = 15.0f;
            }
        }
        // no auction for 30 seconds
        if(time > 15.0f && !swap){
            time = 0.0f;
            swap = true;
            timerText.text = "";
            auctionText.text = "";
            purchaseText.text = "";
            dailyCostText.text = "";
            incomeText.text = "";
            statText.text = "";
            auctionbg.SetActive(false);
        }
        time += Time.deltaTime;
        
    }
}
