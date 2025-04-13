using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Bank : MonoBehaviour
{
  public GameObject player; //db
  public PlayerAttributes playerAttributes1;
  int loanAmount = 0;
  public bool activeDeadline;
  public int deadlineYear;
  public int deadlineMonth;
  public int deadlineDay;
  public bool loaning = false;
  bool bankScreenOn = false;
  bool repayScreenOn = false;

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
    bankRepay.SetActive(false);

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.B))
    {
      if (!loaning)
      {
        requestLoanAmounts();
        bankOffers.SetActive(true);
        bankScreenOn = true;
      }
      else
      {
        if (activeDeadline && (GameController.day == deadlineDay)
          && (GameController.month == deadlineMonth) && (GameController.year == deadlineYear))
        {
          Debug.Log("You are too late to pay. You have lost all your money. You lose.");
          playerAttributes1.money = -1;
        }

        Debug.Log("You are already actively loaning. You have the option to pay your debts now.");
        bankRepay.SetActive(true);
        repayScreenOn = true;
      }
    }

    if (bankScreenOn)
    {
      if (Input.GetKeyDown(KeyCode.Z))
      {
        // Player choose safe
        requestLoan(safeOffer);
      }
      else if (Input.GetKeyDown(KeyCode.X))
      {
        // choose med
        requestLoan(mediumOffer);
      }
      else if (Input.GetKeyDown(KeyCode.C))
      {
        // choose risky
        requestLoan(riskyOffer);
      }
      else if (Input.GetKeyDown(KeyCode.Escape))
      {
        bankOffers.SetActive(false);
        bankScreenOn = false;
      }
    }

    if (repayScreenOn)
    {
      if (Input.GetKeyDown(KeyCode.P))
      {
        playerPays();
      }
      else if (Input.GetKeyDown(KeyCode.Escape))
      {
        bankRepay.SetActive(false);
        repayScreenOn = false;
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
    safeOfferText.text = "(Z) " + safeOffer.ToString();
    mediumOfferText.text = "(X) " + mediumOffer.ToString();
    riskyOfferText.text = "(C) " + riskyOffer.ToString();
  }

  // On click number 1, 2, or 3
  void requestLoan(int requestedAmount)
  {
    playerAttributes1.money += requestedAmount;
    loanAmount = (int)(requestedAmount * 1.15);
    loanAmountText.text = "$" + loanAmount.ToString();
    activeDeadline = true;

    deadlineYear = GameController.year + 1;
    deadlineMonth = GameController.month;
    deadlineDay = GameController.day;
    loaning = true;
    deadlineText.text = "$" + loanAmount + " Due at " +  deadlineMonth + "/" + deadlineDay + "/" + deadlineYear + ".";

    

    bankOffers.SetActive(false);
    bankScreenOn = false;
    loanReminder.SetActive(true);

    //Show active deadline and loan amount
    Debug.Log("Loan of $" + requestedAmount + "and deadline of " + deadlineDay + "/" + deadlineMonth + "/" + deadlineYear);
  }


  void playerPays()
  {
    if (playerAttributes1.money >= loanAmount * 1.15)
    {
      playerAttributes1.money -= (int)(loanAmount * 1.15);
      Debug.Log("Player has successfully paid $" + loanAmount + ". Remaining balance: $" + playerAttributes1.money);
      activeDeadline = false;
      loaning = false;
      loanReminder.SetActive(false);
      bankRepay.SetActive(false);

    }
    else
    {
      Debug.Log("Player does not have enough money to pay $" + loanAmount + ". Current balance: $" + playerAttributes1.money);
    }
  }


}