using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public int speed = 2;
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            cam.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y + (speed * Time.deltaTime));
        }
        if(cam.transform.position.y > 5.5f){
            cam.transform.position = new Vector2(cam.transform.position.x, 5.5f);
        }
    
        if(Input.GetKey(KeyCode.S)){
            cam.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y - (speed * Time.deltaTime));
        }
        if(cam.transform.position.y < -4.0f){
            cam.transform.position = new Vector2(cam.transform.position.x, -4.0f);
        }
        if(Input.GetKey(KeyCode.D)){
            cam.transform.position = new Vector2(cam.transform.position.x + (speed * Time.deltaTime), cam.transform.position.y);
        }
        if(cam.transform.position.x > 5.5f){
            cam.transform.position = new Vector2(5.5f, cam.transform.position.y);
        }
        if(Input.GetKey(KeyCode.A)){
            cam.transform.position = new Vector2(cam.transform.position.x - (speed * Time.deltaTime), cam.transform.position.y);
        }
        if(cam.transform.position.x < -5.5f){
            cam.transform.position = new Vector2(-5.5f, cam.transform.position.y);
        }


        if(Input.GetKey(KeyCode.Q)){
            cam.orthographicSize = cam.orthographicSize + (speed * Time.deltaTime);
        }
        if(cam.orthographicSize < 0.3f){
            cam.orthographicSize = 0.3f;
        }
        if(Input.GetKey(KeyCode.E)){
            cam.orthographicSize = cam.orthographicSize - (speed * Time.deltaTime);
        }
        if(cam.orthographicSize > 3.0f){
            cam.orthographicSize = 3.0f;
        }

    }
}
