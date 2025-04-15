using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Property : MonoBehaviour
{
    public int price;
    public int monthlyIncome;
    public int dailyExpenses;
    public int rng;
    public Sprite cardpic;
    public Sprite card;

    public void randomizeExpenses(){
        rng = Random.Range((int)(monthlyIncome / 2.5), (int)(monthlyIncome * 1.2));
        dailyExpenses = rng/30;
    }
}
