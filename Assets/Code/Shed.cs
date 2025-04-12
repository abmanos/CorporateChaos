using UnityEngine;

public class Shed : Building
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
        buildingName = "Shed " + apartmentID;
        shedID = shedID + 1;
        buildingType = "Shed";
        condition = Random.Range(1, 101);
        size = Random.Range(1, 4);
        base_price = 50000;
        rent = 800;
        sellingPrice = 50000;
        maintenancePaid = false;
        priceIncrease = (sellingPrice * 0.03);
    }


    void Update()
    {
        if (owner == null) return;

        int currentDay = GameManager.instance.currentDay + (GameManager.instance.currentMonth * 30) + (GameManager.instance.currentYear * 360);
        if (currentDay - lastCheckedDay >= 7)
        {
            lastCheckedDay = currentDay;

            if (Random.value < 0.15f)
            {
                condition -= conditionChange;
                PlayerData player = GameManager.instance.players.Find(p => p.playerID == owner.GetComponent<Player>().playerID);
                if (player != null)
                {
                    GameManager.instance.DeductMoney(player.playerID, 100);
                    Debug.Log($"[Event] Shed condition worsened! Player {player.playerID} lost $100.");
                }
            }
        }
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