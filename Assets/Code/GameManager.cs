using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game Settings")]
    public float gameTimeScale = 1.0f; // 1 second = 1 day
    public int startingYear = 1;
    public int startingMonth = 1;
    public int startingMoney = 100000;
    public int winThreshold = 1000000000; // 1 billion to win
    
    [Header("Player Data")]
    public List<PlayerData> players = new List<PlayerData>();
    
    [Header("Time Data")]
    public int currentYear;
    public int currentMonth;
    public int currentDay;
    public float timeElapsed = 0f;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitializeGame();
    }
    
    void InitializeGame()
    {
        // Initialize time
        currentYear = startingYear;
        currentMonth = startingMonth;
        currentDay = 1;
        
        // Create players
        for (int i = 0; i < 4; i++)
        {
            PlayerData newPlayer = new PlayerData();
            newPlayer.playerID = i;
            newPlayer.playerName = "Player " + (i + 1);
            newPlayer.money = startingMoney;
            players.Add(newPlayer);
        }
        
        
        // Start game timer
        StartCoroutine(GameClock());
    }
    
    private IEnumerator GameClock()
    {
        while (true)
        {
            // Wait for 1 second (scaled by gameTimeScale)
            yield return new WaitForSeconds(1f / gameTimeScale);
            
            // Increment time (1 second = 1 day)
            timeElapsed += 1f / gameTimeScale;
            currentDay++;
            
            // Check for month end (30 days per month)
            if (currentDay > 30)
            {
                currentDay = 1;
                currentMonth++;
                
                // Check for year end
                if (currentMonth > 12)
                {
                    currentMonth = 1;
                    currentYear++;
                    
                    // Check game end condition (4 years)
                    if (currentYear >= startingYear + 4)
                    {
                        EndGame();
                        yield break;
                    }
                }
            }       
            // Check win condition
            CheckWinCondition();
        }
    }
    

    public void AddMoney(int playerID, int amount)
    {
        if (playerID >= 0 && playerID < players.Count)
        {
            players[playerID].money += amount;
        }
    }
    
    public void DeductMoney(int playerID, int amount)
    {
        if (playerID >= 0 && playerID < players.Count)
        {
            players[playerID].money -= amount;
            
            // Check for bankruptcy
            if (players[playerID].money < 0)
            {
                int totalAssetValue = CalculatePlayerAssets(playerID);
                if (players[playerID].money + totalAssetValue < 0)
                {
                    DeclareBankruptcy(playerID);
                }
            }
        }
    }
    
    public void AddDailyIncome(int playerID, int amount)
    {
        AddMoney(playerID, amount);
    }
    
    public void DeductMaintenanceFee(int playerID, int amount)
    {
        DeductMoney(playerID, amount);
    }
    
    private void ProcessMonthlyEvents()
    {
        // Process loan repayments
        foreach (var player in players)
        {
            if (player.activeLoan > 0)
            {
                // Calculate interest
                float interestRate = 0.05f - (player.reputation / 1000f); // 0-10% based on reputation
                int interestAmount = Mathf.RoundToInt(player.activeLoan * interestRate / 12);
                
                // Deduct payment + interest
                int monthlyPayment = player.loanMonthlyPayment + interestAmount;
                DeductMoney(player.playerID, monthlyPayment);
                
                // Reduce loan principal
                player.activeLoan -= player.loanMonthlyPayment;
                if (player.activeLoan <= 0)
                {
                    player.activeLoan = 0;
                    player.loanMonthlyPayment = 0;
                }
            }
        }
    }
    
    private void ProcessYearlyEvents()
    {
        // Update player reputations based on performance
        foreach (var player in players)
        {
            int propertyCount = CountPlayerProperties(player.playerID);
            float totalValueRatio = (float)CalculatePlayerNetWorth(player.playerID) / startingMoney;
            
            // Adjust reputation based on growth
            player.reputation = Mathf.Clamp(
                player.reputation + Mathf.RoundToInt((totalValueRatio - 1) * 10),
                0, 100);
        }
    }
    
    private int CountPlayerProperties(int playerID)
    {
        Building[] allBuildings = FindObjectsOfType<Building>();
        return allBuildings.Count(b => b.ownerID == playerID);
    }
    
    private int CalculatePlayerAssets(int playerID)
    {
        Building[] allBuildings = FindObjectsOfType<Building>();
        int totalValue = 0;
        
        foreach (Building building in allBuildings)
        {
            if (building.ownerID == playerID)
            {
                totalValue += building.base_price;
            }
        }
        
        return totalValue;
    }
    
    private int CalculatePlayerNetWorth(int playerID)
    {
        if (playerID >= 0 && playerID < players.Count)
        {
            int assetValue = CalculatePlayerAssets(playerID);
            int cash = players[playerID].money;
            int loans = players[playerID].activeLoan;
            
            return cash + assetValue - loans;
        }
        return 0;
    }
    
    private void TriggerSpecialEvent()
    {
        // List of possible events
        SpecialEvent[] events = new SpecialEvent[]
        {
            new SpecialEvent {
                eventName = "Celebrity Visit",
                description = "Saylor Twift is visiting the city! Hotels are in high demand.",
                affectedSector = "Hotel",
                marketMultiplier = 1.5f,
                duration = 5 // 5 days
            },
            new SpecialEvent {
                eventName = "Economic Recession",
                description = "Economic downturn has hit the market hard!",
                affectedSector = "All",
                marketMultiplier = 0.7f,
                duration = 15 // 15 days
            },
            new SpecialEvent {
                eventName = "Business Convention",
                description = "A major business convention increases demand for office space.",
                affectedSector = "Office",
                marketMultiplier = 1.3f,
                duration = 7 // 7 days
            }
        };
        
        // Select random event
        SpecialEvent selectedEvent = events[UnityEngine.Random.Range(0, events.Length)];
        
        // Trigger UI notification
        // NotificationManager.instance.ShowNotification(selectedEvent.eventName, selectedEvent.description);
        
    }
    
    
    private void DeclareBankruptcy(int playerID)
    {
        // Mark player as bankrupt
        players[playerID].isBankrupt = true;
        
        // Sell all player properties to bank
        Building[] allBuildings = FindObjectsOfType<Building>();
        foreach (Building building in allBuildings)
        {
            if (building.ownerID == playerID)
            {
                building.ownerID = -1; // Bank-owned

            }
        }
        
        // Check if game should end (only one player left)
        int activePlayers = players.Count(p => !p.isBankrupt);
        if (activePlayers <= 1)
        {
            EndGame();
        }
    }
    
    private void CheckWinCondition()
    {
        foreach (var player in players)
        {
            if (!player.isBankrupt)
            {
                int netWorth = CalculatePlayerNetWorth(player.playerID);
                if (netWorth >= winThreshold)
                {
                    // Player has won by reaching 1 billion
                    DeclareWinner(player.playerID);
                    EndGame();
                    return;
                }
            }
        }
    }
    
    private void DeclareWinner(int playerID)
    {
        // Set the winner
        foreach (var player in players)
        {
            player.isWinner = (player.playerID == playerID);
        }
        
        // Show victory screen
        // UIManager.instance.ShowVictoryScreen(playerID);
    }
    
    private void EndGame()
    {
        // If no winner was declared already, find the richest player
        bool hasWinner = players.Any(p => p.isWinner);
        if (!hasWinner)
        {
            int richestPlayerID = -1;
            int highestNetWorth = 0;
            
            foreach (var player in players)
            {
                if (!player.isBankrupt)
                {
                    int netWorth = CalculatePlayerNetWorth(player.playerID);
                    if (netWorth > highestNetWorth)
                    {
                        highestNetWorth = netWorth;
                        richestPlayerID = player.playerID;
                    }
                }
            }
            
            if (richestPlayerID >= 0)
            {
                DeclareWinner(richestPlayerID);
            }
        }
        
        // Stop all coroutines
        StopAllCoroutines();
        
        // Show end game screen
        // UIManager.instance.ShowEndGameScreen();
    }
}

[System.Serializable]
public class PlayerData
{
    public int playerID;
    public string playerName;
    public int money;
    public int reputation; // 0-100, affects loan interest rates
    public int activeLoan;
    public int loanMonthlyPayment;
    public bool isBankrupt;
    public bool isWinner;
}

[System.Serializable]
public class SpecialEvent
{
    public string eventName;
    public string description;
    public string affectedSector; // "All" or specific sector name
    public float marketMultiplier;
    public int duration; // In days
}