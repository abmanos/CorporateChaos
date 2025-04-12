using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public int speed = 1;
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            cam.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y + (speed * Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.S)){
            cam.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y - (speed * Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.D)){
            cam.transform.position = new Vector2(cam.transform.position.x + (speed * Time.deltaTime), cam.transform.position.y);
        }
        if(Input.GetKey(KeyCode.A)){
            cam.transform.position = new Vector2(cam.transform.position.x - (speed * Time.deltaTime), cam.transform.position.y);
        }
    }
}
