using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            cam.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y + 0.002f);
        }
        if(Input.GetKey(KeyCode.S)){
            cam.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y - 0.002f);
        }
        if(Input.GetKey(KeyCode.D)){
            cam.transform.position = new Vector2(cam.transform.position.x + 0.002f, cam.transform.position.y);
        }
        if(Input.GetKey(KeyCode.A)){
            cam.transform.position = new Vector2(cam.transform.position.x - 0.002f, cam.transform.position.y);
        }
    }
}
