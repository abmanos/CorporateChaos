using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;

public class PlayerAttributes : MonoBehaviour
{
    public int playerID = 0;
    public int money = 100000;
    public List<GameObject> buildings;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI incomeText;
    
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
        moneyText.text = "$" + money;     
        if(buildings.Count != 0){
            income = 0;
            foreach(GameObject building in buildings){
                Property prop = building.GetComponent<Property>();
                income += prop.monthlyIncome - (prop.dailyExpenses * 30);
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

    }

    public void buyProperty(GameObject building, int price){
        buildings.Add(building);
        money = money - price;
    }

    public void dailyExpenses(){
        foreach(GameObject bld in buildings){
            Property prop = bld.GetComponent<Property>();
            money -= prop.dailyExpenses;
        }
    }

    public void monthlyIncome(){
        foreach(GameObject bld in buildings){
            Property prop = bld.GetComponent<Property>();
            money += prop.monthlyIncome;
        }
    }
}
