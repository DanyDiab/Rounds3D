using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotate : MonoBehaviour {
    [SerializeField] GameObject toRotate;

    Vector3 delta;

    Mouse currMouse;


    float yaw;
    float pitch;

    void Start(){
        currMouse = Mouse.current;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update(){

        delta = currMouse.delta.ReadValue();


        yaw += delta.x;
        pitch -= delta.y;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

}