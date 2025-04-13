using UnityEngine;

public class startGame : MonoBehaviour
{
    public GameObject gameController;
    public GameObject auction;
    public GameObject startScreen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            startScreen.SetActive(false);
            gameController.SetActive(true);
            auction.SetActive(true);
        }
    }
}
