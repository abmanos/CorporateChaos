using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Bank : MonoBehaviour
{
  public GameObject player; //db
  public PlayerAttributes playerAttributes1;
  int loanAmount;
  public bool activeDeadline;
  public int deadlineYear;
  public int deadlineMonth;
  public int deadlineDay;
  public bool loaning = false;

  public int safeOffer;
  public int mediumOffer;
  public int riskyOffer;
  [SerializeField] TextMeshProUGUI safeOfferText;
  [SerializeField] TextMeshProUGUI mediumOfferText;
  [SerializeField] TextMeshProUGUI riskyOfferText;
  [SerializeField] TextMeshProUGUI loanAmountText;
  [SerializeField] TextMeshProUGUI deadlineText;

  public GameObject loanReminder;
  public GameObject bankOffers;
  public GameObject bankRepay;

  // 2 screens aka 2 parents 
  void Start()
  {
    loanAmount = 0;
    activeDeadline = false;
    playerAttributes1 = player.GetComponent<PlayerAttributes>();
    loanReminder.SetActive(false);
    bankOffers.SetActive(false);

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.B))
    {
      if (!loaning) {
        requestLoanAmounts();
        bankOffers.SetActive(true);
      }
      if (Input.GetKeyDown(KeyCode.Alpha1))      {
        // Player choose safe
              Debug.Log("ABC");

        requestLoan(safeOffer);
      }
      else if (Input.GetKeyDown(KeyCode.Alpha2))
      {
        // choose med
        requestLoan(mediumOffer);
      }
      else if (Input.GetKeyDown(KeyCode.Alpha3))
      {
        // choose risky
        requestLoan(riskyOffer);
      }   
    else
    {
      if (activeDeadline && (GameController.day == deadlineDay) 
        && (GameController.month == deadlineMonth) && (GameController.year == deadlineYear)) {
            Debug.Log("You are too late to pay. You have lost all your money. You lose.");
            playerAttributes1.money = 0;
            // Make sure to destroy the game later
        } 

      Debug.Log("You are already actively loaning. You have the option to pay your debts now.");
      // Make pay and exit button

    }
  }
  }

public void requestLoanAmounts()
{
  int monthly = playerAttributes1.monthlyIncome;

  // For minimum loan amounts calculations, player have less than 1k per month income
  if (monthly < 1000)
  {
    monthly = 1000;
  }

  safeOffer = monthly * 5;
  mediumOffer = monthly * 12;
  riskyOffer = monthly * 25;
  safeOfferText.text = safeOffer.ToString();
  mediumOfferText.text = mediumOffer.ToString();
  riskyOfferText.text = riskyOffer.ToString();
}

// On click number 1, 2, or 3
void requestLoan(int requestedAmount)
{
        Debug.Log("ABCfff");

  playerAttributes1.money += requestedAmount;
  loanAmount = requestedAmount;
  activeDeadline = true;
  
  deadlineYear = GameController.year + 1;
  deadlineMonth = GameController.month;
  deadlineDay = GameController.day;
  loaning = true;

  bankOffers.SetActive(false);
  loanReminder.SetActive(true);
  
  //Show active deadline and loan amount
  Debug.Log("Loan of $" + requestedAmount + "and deadline of " + deadlineDay + "/" + deadlineMonth + "/" + deadlineYear);
}


void playerPays()
{
  if (playerAttributes1.money >= loanAmount)
  {
    playerAttributes1.money -= loanAmount;
    Debug.Log("Player has successfully paid $" + loanAmount + ". Remaining balance: $" + playerAttributes1.money);
    activeDeadline = false;
    loaning = false;
    loanReminder.SetActive(false);
    bankOffers.SetActive(true);
  }
  else
  {
    Debug.Log("Player does not have enough money to pay $" + loanAmount + ". Current balance: $" + playerAttributes1.money);
  }
}


}