using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public float time;
    public static int day;
    public static int month;
    public static int year;
    [SerializeField] TextMeshProUGUI dateText;
    public GameObject player;
    public GameObject WinEndGame;
    public GameObject LoseEndGame;
    public GameObject gameController;
    public GameObject auction;
    public PlayerAttributes attrib;
    public GameObject bank;
    public bool endless = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attrib = player.GetComponent<PlayerAttributes>();
        time = 0;
        day = 1;
        month = 1;
        year = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(attrib.money > 5000000 && !endless){
            endGame(true);
        }
        if(attrib.money < 0){
            endGame(false);
        }
        time += Time.deltaTime;
        if(time > 1.0f){
            dailyTrigger();
            if(day == 30){
                monthlyTrigger();
                day = 1;
                if(month == 12){
                    yearlyTrigger();
                    year++;
                    month = 1;
                } else {
                    month++;
                }
            } else {
                day++;
            }
            time = 0.0f;
        }
        dateText.text = "Year " + year + ", " + month + "/" + day;
        if(year == 6 && !endless){
            endGame(false);
        }
    }

    void dailyTrigger(){
        player.GetComponent<PlayerAttributes>().dailyExpenses();
        bank.GetComponent<Bank>().dailyUpdate();
    }

    void monthlyTrigger(){
        player.GetComponent<PlayerAttributes>().monthlyIncomes();

    }

    void yearlyTrigger(){

    }

    void endGame(bool won){
        if (won) {
            WinEndGame.SetActive(true);
        }
        else {
            LoseEndGame.SetActive(true);
        }
        
        auction.SetActive(false);
        gameController.SetActive(false);
        
    }

    public void endlessMode(){
        endless = true;
    }
}
