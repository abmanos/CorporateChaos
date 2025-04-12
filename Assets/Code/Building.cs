using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Building : MonoBehaviour
{
    [Header("Basic Properties")]
    public string buildingName;
    public string buildingType; // Residential, Commercial, Hotel, etc.
    public int purchasePrice;
    public int currentValue;
    
    [Header("Building Stats")]
    [Range(0, 100)]
    public int condition = 50; // 0-100, affects value and tenant satisfaction
    [Range(1, 4)]
    public int size = 1; // 1-10, affects capacity and maintenance
    public int maintenanceFee = 100; // Daily maintenance cost
    [Range(0, 100)]
    public int attractiveness = 50;
    [Range(0f, 1.0f)]    
    public float condition_drop = 0.5f;
    
    [Header("Financial Data")]
    public int dailyIncome = 0;
    public int occupancyRate = 0; // Percentage of building that is occupied
    public float reputationMultiplier = 1.0f; // Affects income and value
    
    [Header("Owner Information")]
    public int ownerID = -1; // -1 means no owner (NPC owned)
    public bool isForSale = false;
    public int askingPrice = 0;
    
    [Header("Property Development")]
    public List<PropertyUpgrade> availableUpgrades = new List<PropertyUpgrade>();
    public List<PropertyUpgrade> installedUpgrades = new List<PropertyUpgrade>();
    
    // Methods for property management
    public void PerformMaintenance()
    {
        // Increase condition based on maintenance investment
        condition = Mathf.Min(100, condition + 10);
        // Deduct maintenance fee from player's account
        if (ownerID >= 0)
        {
            GameManager.instance.DeductMaintenanceFee(ownerID, maintenanceFee);
        }
    }
    
    public void AdjustMaintenanceFee(int newFee)
    {
        maintenanceFee = Mathf.Max(0, newFee);
        // Higher maintenance improves condition over time, lower reduces it
        StartCoroutine(AdjustConditionOverTime());
    }
    
    private IEnumerator AdjustConditionOverTime()
    {
        while (true)
        {
            // Calculate how much condition changes based on maintenance fee
            float maintenanceRatio = (float)maintenanceFee / (size * 100);
            float conditionChange = (maintenanceRatio - 1) * 5;
            
            condition = Mathf.Clamp(condition + Mathf.RoundToInt(conditionChange), 0, 100);
            
            // Wait for a day (1 minute in real time)
            yield return new WaitForSeconds(60f);
        }
    }
    
    public void UpgradeProperty(PropertyUpgrade upgrade)
    {
        if (availableUpgrades.Contains(upgrade))
        {
            // Apply upgrade effects
            currentValue += upgrade.valueIncrease;
            condition = Mathf.Min(100, condition + upgrade.conditionBoost);
            dailyIncome += upgrade.incomeBoost;
            reputationMultiplier += upgrade.reputationBoost;
            
            // Move from available to installed
            availableUpgrades.Remove(upgrade);
            installedUpgrades.Add(upgrade);
            
            // Deduct cost from player
            if (ownerID >= 0)
            {
                GameManager.instance.DeductMoney(ownerID, upgrade.cost);
            }
        }
    }
    
    public void CalculateDailyIncome()
    {
        // Base income depends on property type, size, and positioning
        int baseIncome = 1000;
        
        // Adjust based on condition and occupancy
        float conditionFactor = condition / 100f;
        float occupancyFactor = occupancyRate / 100f;
        
        // Calculate final income
        dailyIncome = Mathf.RoundToInt(baseIncome * conditionFactor * occupancyFactor * reputationMultiplier);
        
        // Add income to player's account
        if (ownerID >= 0)
        {
            GameManager.instance.AddDailyIncome(ownerID, dailyIncome);
        }
    }
    
    public void SetForSale(bool forSale, int price = 0)
    {
        isForSale = forSale;
        if (forSale)
        {
            askingPrice = price > 0 ? price : currentValue;
        }
    }
    
    // Called when a player buys this property
    public void PurchaseProperty(int newOwnerID, int transactionAmount)
    {
        // Transfer ownership
        int previousOwner = ownerID;
        ownerID = newOwnerID;
        isForSale = false;
        
        // Transfer money
        if (previousOwner >= 0)
        {
            GameManager.instance.AddMoney(previousOwner, transactionAmount);
        }
        GameManager.instance.DeductMoney(newOwnerID, transactionAmount);
        
        // Reset some stats on purchase
        StartCoroutine(AdjustConditionOverTime());
    }
    
}

// Class for property upgrades
[System.Serializable]
public class PropertyUpgrade
{
    public string upgradeName;
    public string description;
    public int cost;
    public int valueIncrease;
    public int conditionBoost;
    public int incomeBoost;
    public float reputationBoost;
    public string buildingTypeRequirement; // Which type of building can use this upgrade
}