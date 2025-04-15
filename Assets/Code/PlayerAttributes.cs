using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System;

public class PlayerAttributes : MonoBehaviour
{
    public int playerID = 0;
    public int money = 100000;
    public int debt = 0;
    public int dailyExpense = 0;
    public int monthlyIncome = 0;
    public int index = 0;
    public bool invOpen = false;
    public int monthlyProfit = 0;
    public GameObject auction;

    public List<GameObject> buildings;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI incomeText;

    [SerializeField] TextMeshProUGUI invcount;

    public GameObject inventory;
    public GameObject invslot1;
    [SerializeField] TextMeshProUGUI invslot1nametext;
    [SerializeField] TextMeshProUGUI invslot1incometext;
    [SerializeField] TextMeshProUGUI invslot1expensestext;
    [SerializeField] TextMeshProUGUI invslot1selltext;
    public GameObject invslot1img;
    public GameObject invslot2;
    [SerializeField] TextMeshProUGUI invslot2nametext;
    [SerializeField] TextMeshProUGUI invslot2incometext;
    [SerializeField] TextMeshProUGUI invslot2expensestext;
    [SerializeField] TextMeshProUGUI invslot2selltext;
    public GameObject invslot2img;
    public GameObject invslot3;
    [SerializeField] TextMeshProUGUI invslot3nametext;
    [SerializeField] TextMeshProUGUI invslot3incometext;
    [SerializeField] TextMeshProUGUI invslot3expensestext;
    [SerializeField] TextMeshProUGUI invslot3selltext;
    public GameObject invslot3img;
    public GameObject invslot4;
    [SerializeField] TextMeshProUGUI invslot4nametext;
    [SerializeField] TextMeshProUGUI invslot4incometext;
    [SerializeField] TextMeshProUGUI invslot4expensestext;
    [SerializeField] TextMeshProUGUI invslot4selltext;
    public GameObject invslot4img;
    public GameObject invslot5;
    [SerializeField] TextMeshProUGUI invslot5nametext;
    [SerializeField] TextMeshProUGUI invslot5incometext;
    [SerializeField] TextMeshProUGUI invslot5expensestext;
    [SerializeField] TextMeshProUGUI invslot5selltext;
    public GameObject invslot5img;

    static Color green = new Color32(95,255,131,255);
    static Color red = new Color32(255,112,112,255);
    
    void Start()
    {
        buildings = new List<GameObject>();
        Debug.Log("Player " + playerID + "properties");
        Debug.Log(buildings);
    }

    // Update is called once per frame
    void Update()
    {

        monthlyProfit = 0;
        int leftover = 0;
        if(money < 0){
            moneyText.color = red;
        }
        moneyText.text = "$" + money;
        invcount.text = buildings.Count + "/5 Properties (I)";  
        if(Input.GetKeyDown(KeyCode.I) && buildings.Count > 0){
            invOpen = !invOpen;
            inventory.SetActive(invOpen);
        }
        if(invOpen){
            if(Input.GetKeyDown(KeyCode.Alpha1) && buildings.Count >= 1){
                money += (int)(buildings[0].GetComponent<Property>().price * 0.8);
                auction.GetComponent<Auction>().readdBuilding((buildings[0], buildings[0].GetComponent<Property>().price));
                buildings.RemoveAt(0);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && buildings.Count >= 2){
                money += (int)(buildings[1].GetComponent<Property>().price * 0.8);
                auction.GetComponent<Auction>().readdBuilding((buildings[1], buildings[1].GetComponent<Property>().price));
                buildings.RemoveAt(1);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3) && buildings.Count >= 3){
                money += (int)(buildings[2].GetComponent<Property>().price * 0.8);
                auction.GetComponent<Auction>().readdBuilding((buildings[2], buildings[2].GetComponent<Property>().price));
                buildings.RemoveAt(2);
            }
            if(Input.GetKeyDown(KeyCode.Alpha4) && buildings.Count >= 4){
                money += (int)(buildings[3].GetComponent<Property>().price * 0.8);
                auction.GetComponent<Auction>().readdBuilding((buildings[3], buildings[3].GetComponent<Property>().price));
                buildings.RemoveAt(3);
            }
            if(Input.GetKeyDown(KeyCode.Alpha5) && buildings.Count == 5){
                money += (int)(buildings[4].GetComponent<Property>().price * 0.8);
                auction.GetComponent<Auction>().readdBuilding((buildings[4], buildings[4].GetComponent<Property>().price));
                buildings.RemoveAt(4);
            }
        }
        if(Input.GetKeyDown(KeyCode.L)) {
            money -= 100000;
        }
        if(Input.GetKeyDown(KeyCode.K)) {
            money += 100000;
        }
        if(buildings.Count != 0){
            index = 0;
            monthlyProfit = 0;
            leftover = 5 - buildings.Count;
            foreach(GameObject building in buildings){
                Property prop = building.GetComponent<Property>();
                monthlyProfit += prop.monthlyIncome - (prop.dailyExpenses * 30);
                if(index == 0){
                    invslot1.SetActive(true);
                    invslot1nametext.text = prop.name;
                    invslot1incometext.text = "Monthly Income: $" + prop.monthlyIncome;
                    invslot1expensestext.text = "Daily Expenses: $" + prop.dailyExpenses;
                    invslot1selltext.text = "Sell: $" + (int)(prop.price * 0.8);
                    invslot1img.GetComponent<UnityEngine.UI.Image>().sprite = prop.cardpic; 
                    invslot1.GetComponent<UnityEngine.UI.Image>().sprite = prop.card; 
                }
                if(index == 1){
                    invslot2.SetActive(true);
                    invslot2nametext.text = prop.name;
                    invslot2incometext.text = "Monthly Income: $" + prop.monthlyIncome;
                    invslot2expensestext.text = "Daily Expenses: $" + prop.dailyExpenses;
                    invslot2selltext.text = "Sell: $" + (int)(prop.price * 0.8);
                    invslot2img.GetComponent<UnityEngine.UI.Image>().sprite = prop.cardpic;
                    invslot2.GetComponent<UnityEngine.UI.Image>().sprite = prop.card; 
                }
                if(index == 2){
                    invslot3.SetActive(true);
                    invslot3nametext.text = prop.name;
                    invslot3incometext.text = "Monthly Income: $" + prop.monthlyIncome;
                    invslot3expensestext.text = "Daily Expenses: $" + prop.dailyExpenses;
                    invslot3selltext.text = "Sell: $" + (int)(prop.price * 0.8);
                    invslot3img.GetComponent<UnityEngine.UI.Image>().sprite = prop.cardpic; 
                    invslot3.GetComponent<UnityEngine.UI.Image>().sprite = prop.card; 
                }
                if(index == 3){
                    invslot4.SetActive(true);
                    invslot4nametext.text = prop.name;
                    invslot4incometext.text = "Monthly Income: $" + prop.monthlyIncome;
                    invslot4expensestext.text = "Daily Expenses: $" + prop.dailyExpenses;
                    invslot4selltext.text = "Sell: $" + (int)(prop.price * 0.8);
                    invslot4img.GetComponent<UnityEngine.UI.Image>().sprite = prop.cardpic; 
                    invslot4.GetComponent<UnityEngine.UI.Image>().sprite = prop.card; 
                }
                if(index == 4){
                    invslot5.SetActive(true);
                    invslot5nametext.text = prop.name;
                    invslot5incometext.text = "Monthly Income: $" + prop.monthlyIncome;
                    invslot5expensestext.text = "Daily Expenses: $" + prop.dailyExpenses;
                    invslot5selltext.text = "Sell: $" + (int)(prop.price * 0.8);
                    invslot5img.GetComponent<UnityEngine.UI.Image>().sprite = prop.cardpic; 
                    invslot5.GetComponent<UnityEngine.UI.Image>().sprite = prop.card; 
                
                }
                index++;
            }
        }
        incomeText.text = "";
        if(monthlyProfit > 0){
            incomeText.text += "+$";
            incomeText.color = green;
        } else if(monthlyProfit == 0){
            incomeText.color = Color.white;
        } else {
            incomeText.text += "-$";
            incomeText.color = red;
        }
        incomeText.text += (int)(Math.Abs(monthlyProfit)) + "/mo";
        if(buildings.Count == 0){
            invslot1.SetActive(false);
            invslot2.SetActive(false);
            invslot3.SetActive(false);
            invslot4.SetActive(false);
            invslot5.SetActive(false);
        }
        if(buildings.Count == 1){
            invslot2.SetActive(false);
            invslot3.SetActive(false);
            invslot4.SetActive(false);
            invslot5.SetActive(false);
        }
        if(buildings.Count == 2){
            invslot3.SetActive(false);
            invslot4.SetActive(false);
            invslot5.SetActive(false);
        }
        if(buildings.Count == 3){
            invslot4.SetActive(false);
            invslot5.SetActive(false);
        }
        if(buildings.Count == 4){
            invslot5.SetActive(false);
        }


    }

    // Integrate later
    public void requestLoan(int amount){
        requestLoan(amount);
    }

    public void buyProperty(GameObject building, int price){
        buildings.Add(building);
        money = money - price;
    }

    public void dailyExpenses(){
        foreach(GameObject bld in buildings){
            Property prop = bld.GetComponent<Property>();
            dailyExpense = prop.dailyExpenses;
            money -= dailyExpense;
            monthlyIncome = prop.monthlyIncome;
        }
    }

    public void monthlyIncomes(){
        foreach(GameObject bld in buildings){
            Property prop = bld.GetComponent<Property>();
            monthlyIncome = prop.monthlyIncome;
            money += monthlyIncome;
        }
    }
}
