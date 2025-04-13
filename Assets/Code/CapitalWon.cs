using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Bank
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

  public GameObject debtbg;
  public GameObject bankLoanfg;
  public GameObject bankPayfg;

  // 2 screens aka 2 parents 
  void Start()
  {
    loanAmount = 0;
    activeDeadline = false;
    playerAttributes1 = player.GetComponent<PlayerAttributes>();

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.B))
    {
      if (!loaning)
        requestLoanAmounts();
      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
        // Player choose safe
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

    }
    else
    {
      // ALready loaned
      // Show pay button and exit button
      // Check if deadline date is reached
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
  playerAttributes1.money += requestedAmount;
  loanAmount = requestedAmount;
  activeDeadline = true;
  //deadlineYear = currentYear + 1;
  //deadlineMonth = currentMonth;
  //deadlineDay = currentDay;

  // Show active deadline and loan amount
  Debug.Log("Loan of $" + requestedAmount);
}


void playerPays()
{
  if (playerAttributes1.money >= loanAmount)
  {
    playerAttributes1.money -= loanAmount;
    Debug.Log("Player has successfully paid $" + loanAmount + ". Remaining balance: $" + playerAttributes1.money);
    activeDeadline = false;
  }
  else
  {
    Debug.Log("Player does not have enough money to pay $" + loanAmount + ". Current balance: $" + playerAttributes1.money);
  }
}


}