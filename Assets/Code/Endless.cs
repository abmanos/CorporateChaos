using UnityEngine;

public class Endless : MonoBehaviour
{
    public GameObject gameController;
    public GameObject auction;
    public GameObject youwon;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            gameController.GetComponent<GameController>().endlessMode();
            gameController.SetActive(true);
            auction.SetActive(true);
            youwon.SetActive(false);
        }
    }
}
