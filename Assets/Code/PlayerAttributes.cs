using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;

public class PlayerAttributes : MonoBehaviour
{
    public int playerID = 0;
    public int money = 100000;
    public List<GameObject> buildings;

    // this is just to test, remove later
    public GameObject testbuilding;
    [SerializeField] TextMeshProUGUI moneyText;
    
    void Start()
    {
        buildings = new List<GameObject>();
        Debug.Log("Player " + playerID + "properties");
        Debug.Log(buildings);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            buyProperty(testbuilding, 200000);
        }
        moneyText.text = "$" + money;
        
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
