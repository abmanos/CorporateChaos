using UnityEngine;

public class Shed : MonoBehaviour
{
    public GameObject owner;
    public int customers;
    public double maintenanceFee;
    public double rent;
    public double sellingPrice;
    public double priceIncrease;
    public int condition;
    public int conditionChange;
    public boolean maintenancePaid;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        customers = 100;
        maintenanceFee = 500;
        rent = 800;
        sellingPrice = 50,000
        maintenancePaid = false;
        priceIncrease = (sellingPrice * 0.03)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Purchase(GameObject buyer, boolean payingMaintenance) {
        owner = buyer;
        maintenancePaid = payingMaintenance;
        // subtract player money later or maybe not here- maybe on player end instead
    }
    
    void MonthChange()
    {
        if (!maintenancePaid) {
            condition -= conditionChange;
        }
        sellingPrice += priceIncrease;
    }
}