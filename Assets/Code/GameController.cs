using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public float time;
    public int day;
    public int month;
    public int year;
    [SerializeField] TextMeshProUGUI dateText;
    public GameObject player;
    public PlayerAttributes attrib;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attrib = player.GetComponent<PlayerAttributes>();
        time = Time.time;
        day = 1;
        month = 1;
        year = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(attrib.money > 5000000){
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
        dateText.text = "Year" + year + ", " + month + "/" + day;
        if(year == 6){
            endGame(false);
        }
    }

    void dailyTrigger(){
        player.GetComponent<PlayerAttributes>().dailyExpenses();
    }

    void monthlyTrigger(){
        player.GetComponent<PlayerAttributes>().monthlyIncomes();

    }

    void yearlyTrigger(){

    }

    void endGame(bool won){
        string result = won ? "Won!" : "Lost :(";  
        dateText.text = "";
        Debug.Log("Game finished, you " + result);
    }
}
