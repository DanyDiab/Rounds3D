using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour{
    InputAction dashAction;
    [SerializeField] InputActionAsset inputActions;
    // this is probably the camera
    [SerializeField] Transform lookTransform;
    [SerializeField] PlayerStats playerStats;

    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        dashAction = inputActions.FindAction("Dash");
        dashAction.Enable();
    }

    void Update(){
        if(ButtonPressUtil.Pressed(dashAction)){
            Debug.Log("DASH");
            Vector3 lookDir = lookTransform.forward;
            rb.AddForce(lookDir * playerStats.dashForce);
        }
    }
}