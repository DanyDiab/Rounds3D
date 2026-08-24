using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotate : MonoBehaviour {
    [SerializeField] GameObject toRotate;

    Vector3 delta;
    Mouse currMouse;

    Camera cam;

    [SerializeField] float sensitivity;


    float yaw;
    float pitch;

    void Start(){
        currMouse = Mouse.current;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update(){
        delta = currMouse.delta.ReadValue() * sensitivity;

        yaw += delta.x;
        pitch -= delta.y;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        toRotate.transform.rotation = Quaternion.Euler(0.0f, yaw, 0f);
        cam.transform.rotation = Quaternion.Euler(pitch, yaw, 0.0f);
    }
}