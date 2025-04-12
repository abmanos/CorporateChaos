using UnityEngine;

public class Apartment : Building
{
    public static int apartmentID = 1;
    private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingName = "Apartment " + apartmentID;
        apartmentID = apartmentID + 1;
        buildingType = "Apartment";
        condition = Random.Range(1, 101);
        size = Random.Range(1, 4);
        base_price = 200000;
        maintenanceFee = 20;
        attractiveness = 60;
        condition_drop = 1;
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

