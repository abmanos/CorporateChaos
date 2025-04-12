using UnityEngine;
using System.Collections.Generic;

public class PlayerAttributes : MonoBehaviour
{
    public int playerID = 0;
    public int money = 100000;
    public List<GameObject> buildings;

    // this is just to test, remove later
    public GameObject testbuilding;
    
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
        
    }

    void buyProperty(GameObject building, int price){
        buildings.Add(building);
        money = money - price;
    }
}
