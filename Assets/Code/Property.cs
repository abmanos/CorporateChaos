using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Property : MonoBehaviour
{
    public int monthlyIncome;
    public int dailyExpenses;
    public int rng;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rng = Random.Range((int)(monthlyIncome / 1.8), (int)(monthlyIncome * 1.15));
        dailyExpenses = rng/30;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
