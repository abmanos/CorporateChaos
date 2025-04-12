using System;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Money;
    public Properties[] PropertyList;
    void Start()
    {
        Money = 0;
        PropertyList = new Properties[] {};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
