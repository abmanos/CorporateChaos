using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public int playerID = 0;
    public int money = 100000;
    public int debt = 0;
    public int dailyExpense = 0;
    public int monthlyIncome = 0;
    public int index = 0;

    

    public List<GameObject> buildings;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI incomeText;

    [SerializeField] TextMeshProUGUI invcount;

    public GameObject invslot1;
    [SerializeField] TextMeshProUGUI invslot1text;
    public GameObject invslot1img;
    public GameObject invslot2;
    [SerializeField] TextMeshProUGUI invslot2text;
    public GameObject invslot2img;
    public GameObject invslot3;
    [SerializeField] TextMeshProUGUI invslot3text;
    public GameObject invslot3img;
    public GameObject invslot4;
    [SerializeField] TextMeshProUGUI invslot4text;
    public GameObject invslot4img;
    public GameObject invslot5;
    [SerializeField] TextMeshProUGUI invslot5text;
    public GameObject invslot5img;
    
    void Start()
    {
        buildings = new List<GameObject>();
        Debug.Log("Player " + playerID + "properties");
        Debug.Log(buildings);
    }

    // Update is called once per frame
    void Update()
    {

        int income = 0;
        int leftover = 0;
        moneyText.text = "$" + money;
        invcount.text = buildings.Count + "/5 properties";  
        if(Input.GetKeyDown(KeyCode.Alpha1) && buildings.Count >= 1){
            money += (int)(buildings[0].GetComponent<Property>().price * 0.8);
            buildings.RemoveAt(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && buildings.Count >= 2){
            money += (int)(buildings[1].GetComponent<Property>().price * 0.8);
            buildings.RemoveAt(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && buildings.Count >= 3){
            money += (int)(buildings[2].GetComponent<Property>().price * 0.8);
            buildings.RemoveAt(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && buildings.Count >= 4){
            money += (int)(buildings[3].GetComponent<Property>().price * 0.8);
            buildings.RemoveAt(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5) && buildings.Count == 5){
            money += (int)(buildings[4].GetComponent<Property>().price * 0.8);
            buildings.RemoveAt(4);
        }
        if(Input.GetKeyDown(KeyCode.L)) {
            money -= 100000;
        }
        if(Input.GetKeyDown(KeyCode.K)) {
            money += 100000;
        }
        if(buildings.Count != 0){
            index = 0;
            income = 0;
            leftover = 5 - buildings.Count;
            foreach(GameObject building in buildings){
                Property prop = building.GetComponent<Property>();
                income += prop.monthlyIncome - (prop.dailyExpenses * 30);
                if(index == 0){
                    invslot1.SetActive(true);
                    invslot1text.text = "$" + (int)(prop.price * 0.8);
                    invslot1img.GetComponent<UnityEngine.UI.Image>().sprite = building.GetComponent<SpriteRenderer>().sprite; 
                }
                if(index == 1){
                    invslot2.SetActive(true);
                    invslot2text.text = "$" + (int)(prop.price * 0.8);
                    invslot2img.GetComponent<UnityEngine.UI.Image>().sprite = building.GetComponent<SpriteRenderer>().sprite; 
                }
                if(index == 2){
                    invslot3.SetActive(true);
                    invslot3text.text = "$" + (int)(prop.price * 0.8);
                    invslot3img.GetComponent<UnityEngine.UI.Image>().sprite = building.GetComponent<SpriteRenderer>().sprite; 
                }
                if(index == 3){
                    invslot4.SetActive(true);
                    invslot4text.text = "$" + (int)(prop.price * 0.8);
                    invslot4img.GetComponent<UnityEngine.UI.Image>().sprite = building.GetComponent<SpriteRenderer>().sprite; 
                }
                if(index == 4){
                    invslot5.SetActive(true);
                    invslot5text.text = "$" + (int)(prop.price * 0.8);
                    invslot5img.GetComponent<UnityEngine.UI.Image>().sprite = building.GetComponent<SpriteRenderer>().sprite; 
                
                }
                index++;
            }
        }
        if(income > 0){
            incomeText.color = Color.green;
        } else if(income == 0){
            incomeText.color = Color.gray;
        } else {
            incomeText.color = Color.red;
        }
        incomeText.text = "+$" + income + "/mo";
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
