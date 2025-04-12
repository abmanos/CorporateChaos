using UnityEngine;

public class Stadium : Building
{
    private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingName = "Capital Won Stadium";
        buildingType = "Stadium";
        condition = Random.Range(1, 101);
        size = Random.Range(1, 4);
        base_price = 5000000;
        maintenanceFee = 5000;
        attractiveness = 5;
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
