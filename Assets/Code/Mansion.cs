using UnityEngine;

public class Mansion : MonoBehaviour
{
    public GameObject owner;
    public int customers;
    public double maintenanceFee;
    public double rent;
    public double sellingPrice;
    public double priceIncrease;
    public int condition;
    public int conditionChange;
    public bool maintenancePaid;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        customers = 20;
        maintenanceFee = 10000;
        rent = 20000;
        sellingPrice = 1000000;
        maintenancePaid = false;
        priceIncrease = (sellingPrice * 0.03);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Purchase(GameObject buyer, bool payingMaintenance) {
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