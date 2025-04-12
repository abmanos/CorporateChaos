using UnityEngine;

public class Hotel : Building
{
    public static int hotelID = 1;
    public int base_price;
    private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingName = "Hotel " + hotelID;
        hotelID = hotelID + 1;
        buildingType = "Business";
        condition = Random.Range(1, 101);
        size = Random.Range(1, 4);
        base_price = 200000;
        maintenanceFee = 20;
        attractiveness = 60;
        condition_drop = 0.8f;
        manager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(buildingName.ToString());
        Debug.Log(buildingType);
        Debug.Log(manager.currentDay);
    }
}
