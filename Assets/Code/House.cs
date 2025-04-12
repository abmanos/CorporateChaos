using UnityEngine;

public class House : Building
{
    public static int houseID = 1;
    public int base_price;
    private GameManager manager;
    
    private int lastCheckedDay = -7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingName = "House " + houseID;
        shedID = houseID + 1;
        buildingType = "House";
        condition = Random.Range(1, 101);
        size = Random.Range(1, 4);
        base_price = 200000;
        rent = 12500;
        maintenanceFee = 50;
        attractiveness = 60;
        condition_drop = 0.8f;
        maintenancePaid = false;
        priceIncrease = (sellingPrice * 0.03)
        manager = GameManager.instance;
    }

    // Update is called once per frame
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