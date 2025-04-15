using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.ComponentModel;

public class Bank : MonoBehaviour
{
  public GameObject player; //db
  public PlayerAttributes playerAttributes1;
  int loanAmount = 0;
  public bool activeDeadline;
  public bool loaning = false;
  bool bankScreenOn = false;
  bool repayScreenOn = false;

  public int safeOffer;
  public int mediumOffer;
  public int riskyOffer;
  public int deadlineDays;
  [SerializeField] TextMeshProUGUI safeOfferText;
  [SerializeField] TextMeshProUGUI mediumOfferText;
  [SerializeField] TextMeshProUGUI riskyOfferText;
  [SerializeField] TextMeshProUGUI loanAmountText;
  [SerializeField] TextMeshProUGUI deadlineText;

  public GameObject loanReminder;
  public GameObject bankOffers;
  public GameObject bankRepay;
  public GameObject offerClose;
  public GameObject repayClose;
  public Button offersButton;
  public Button repayButton;

  void Start()
  {
    loanAmount = 0;
    activeDeadline = false;
    playerAttributes1 = player.GetComponent<PlayerAttributes>();
    loanReminder.SetActive(false);
    bankOffers.SetActive(false);
    bankRepay.SetActive(false);
    offersButton = offerClose.GetComponent<Button>();
    repayButton = repayClose.GetComponent<Button>();
    offersButton.onClick.AddListener(delegate {closeWindow(1);});
    offersButton.onClick.AddListener(delegate {closeWindow(2);});

  }

  // Update is called once per frame
  void Update()
  {
    var openThisFrame = false;
    if (Input.GetKeyDown(KeyCode.B) && !bankScreenOn)
    {
      if (!loaning)
      {
        requestLoanAmounts();
        bankOffers.SetActive(true);
        bankScreenOn = true;
        openThisFrame = true;
      }
    }

    if (Input.GetKeyDown(KeyCode.B) && !repayScreenOn){
      if (loaning)
      {
        bankRepay.SetActive(true);
        repayScreenOn = true;
        openThisFrame = true;
      }
    }

    if (bankScreenOn)
    {

      if (Input.GetKeyDown(KeyCode.Z))
      {
        // Player choose safe
        deadlineDays = 360;
        requestLoan(safeOffer);
      }
      if (Input.GetKeyDown(KeyCode.X))
      {
        // choose med
        deadlineDays = 180;
        requestLoan(mediumOffer);
      }
      if (Input.GetKeyDown(KeyCode.C))
      {
        // choose risky
        deadlineDays = 90;
        requestLoan(riskyOffer);
      }
      if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.B)) && !openThisFrame)
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
      else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.B)) && !openThisFrame)
      {
        bankRepay.SetActive(false);
        repayScreenOn = false;
      }
    }
  }

  public void requestLoanAmounts()
  {
    int monthly = playerAttributes1.monthlyProfit;

    // For minimum loan amounts calculations, player have less than 1k per month income
    if (monthly < 1000)
    {
      monthly = 1000;
    }

    safeOffer = monthly * 5;
    mediumOffer = monthly * 10;
    riskyOffer = monthly * 20;
    safeOfferText.text = "(Z) $" + safeOffer.ToString() + " 360 Day Loan";
    mediumOfferText.text = "(X) $" + mediumOffer.ToString() + " 180 Day Loan";
    riskyOfferText.text = "(C) $" + riskyOffer.ToString() + " 90 Day Loan";
  }

  // On click number 1, 2, or 3
  void requestLoan(int requestedAmount)
  {
    playerAttributes1.money += requestedAmount;
    loanAmount = (int)(requestedAmount * 1.15);
    loanAmountText.text = "$" + loanAmount.ToString();
    activeDeadline = true;

    loaning = true;
    deadlineText.text = "$" + loanAmount + " Due in " +  deadlineDays + " days";

    

    bankOffers.SetActive(false);
    bankScreenOn = false;
    loanReminder.SetActive(true);

  }


  void playerPays()
  {
    if (playerAttributes1.money >= loanAmount)
    {
      playerAttributes1.money -= loanAmount;
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


  public void dailyUpdate(){
    if(loaning){
      if(deadlineDays == 0){
        playerAttributes1.money -= loanAmount;
        activeDeadline = false;
        loaning = false;
        loanReminder.SetActive(false);
        bankRepay.SetActive(false);
      }
      deadlineDays -= 1;
      deadlineText.text = "$" + loanAmount + " Due in " +  deadlineDays + " days";
    }
  }

  public void closeWindow(int window){
    if(window == 1){
      bankOffers.SetActive(false);
      bankScreenOn = false;
    }
    if(window == 2){
      bankRepay.SetActive(false);
      repayScreenOn = false;
    }
  }
}