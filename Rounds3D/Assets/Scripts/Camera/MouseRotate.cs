using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotate : MonoBehaviour {
[SerializeField] GameObject toRotate;

    Vector3 delta;
    Mouse currMouse;

    Camera cam;

    [SerializeField] float sensitivity;

    Quaternion camRotationQuat;
    [SerializeField] CameraRotation camRotate;


    float yaw;
    float pitch;

    QuatRot camRotQuat;

    void Awake(){
        currMouse = Mouse.current;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        camRotationQuat = Quaternion.identity;
        camRotQuat = new QuatRot(camRotationQuat);
    }
    void OnEnable(){
        camRotate.AddRotation(camRotQuat);
    }

    void OnDisable(){
        camRotate.RemoveRotation(camRotQuat);
    }

    void Update(){
        delta = currMouse.delta.ReadValue() * sensitivity;

        yaw += delta.x;
        pitch -= delta.y;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
        toRotate.transform.rotation = Quaternion.Euler(0.0f, yaw, 0f);
        camRotQuat.quat = Quaternion.Euler(pitch, yaw, 0.0f);
    }
}