using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class Building : MonoBehaviour
{
    [Header("Basic Properties")]
    public string buildingName;
    public string buildingType; // Residential, Commercial, Hotel, etc.
    public int base_price;

    
    
    [Header("Building Stats")]
    [Range(0, 100)]
    public int condition = 50; // 0-100, affects value and tenant satisfaction
    [Range(1, 4)]
    public int size = 1; // 1-10, affects capacity and maintenance
    public int maintenanceFee = 100; // Daily maintenance cost
    [Range(0, 100)]
    public int attractiveness = 50;
    [Range(0f, 1.0f)]    
    public int condition_drop = 1;
    Queue<Offer> offers = new Queue<Offer>();
    
    
    [Header("Owner Information")]
    public int ownerID = -1; // -1 means no owner (NPC owned)
    public bool isForSale = false;
    public bool isForRent = false;
    public int askingPrice = 0;
    public bool isMaintained = true;
    
    
    // Methods for property management
    public int PerformMaintenance(bool maintain)
    {
        if (maintain) {
            isMaintained = true;
            return maintenanceFee;
        } else {
            isMaintained = false;
            return 0;
        }
    }
    
    private IEnumerator AdjustConditionOverTime()
    {
        while (true)
        {
            if (isMaintained == false) {
                condition = condition - condition_drop;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator BuyersOverTime(int speculatedPrice) {
        while (true)
        {
            if (isForRent == true) {
                int chance = UnityEngine.Random.Range(1, 101);
                bool offerAvailable = false;
                if (chance <= attractiveness) {
                    offerAvailable = true;
                }
                if (offerAvailable) {
                    int rentOffer = CalculateSpeculatedPrice() / 100 * 3;
                    int variance = UnityEngine.Random.Range(1, rentOffer);
                    rentOffer = rentOffer - variance;
                    variance = UnityEngine.Random.Range(1, rentOffer);
                    rentOffer = rentOffer + variance;
                    offers.Append(new Offer("Rent", rentOffer));
                }
            }
            if (isForSale == false) {
                int chance = UnityEngine.Random.Range(1, 101);
                bool offerAvailable = false;
                if (chance <= attractiveness) {
                    offerAvailable = true;
                }
                if (offerAvailable) {
                    int buyOffer = CalculateSpeculatedPrice();
                    int variance = UnityEngine.Random.Range(1, buyOffer) / 2;
                    buyOffer = buyOffer - variance;
                    variance = UnityEngine.Random.Range(1, buyOffer) / 2;
                    buyOffer = buyOffer + variance;
                    offers.Append(new Offer("Buy", buyOffer));
                }
            }
            yield return new WaitForSeconds(10f);
        }
    }
    
    public int RenovateProperty()
    {
        int renovatePrice = (100 - condition)/100 * base_price * (1/2);
        return renovatePrice;
    }
    
    public int CalculateSpeculatedPrice() {
        int conditionMultiplier = condition/100*2;
        int sizeMultiplier = 1 + size/10;
        int speculatedPrice = base_price * conditionMultiplier * sizeMultiplier;
        speculatedPrice = speculatedPrice + 2 * maintenanceFee;
        return speculatedPrice;
    }
    
}

[System.Serializable]
public class Offer
{
    public string type;
    public int money;

    public Offer(string offerType, int offerMoney)
    {
        type = offerType;
        money = offerMoney;
    }
}

