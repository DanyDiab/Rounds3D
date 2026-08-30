using UnityEngine;
using UnityEngine.InputSystem;

enum DashState{
    READY,
    COOLDOWN
}

public class Dash : MonoBehaviour{
    InputAction dashAction;
    [SerializeField] InputActionAsset inputActions;
    // this is probably the camera
    [SerializeField] Transform lookTransform;
    [SerializeField] PlayerStats playerStats;


    float timer;

    Rigidbody rb;

    DashState currState;

    void Start(){
        currState = DashState.READY;
        rb = GetComponent<Rigidbody>();
        dashAction = inputActions.FindAction("Dash");
        dashAction.Enable();
    }

    void Update(){
        switch(currState){
            case DashState.COOLDOWN:{
                timer += Time.deltaTime;
                if(timer >= playerStats.dashCooldownMS){
                    currState = DashState.READY;
                    timer = 0f;
                }
                break;
            }
            case DashState.READY:{
                if(ButtonPressUtil.Pressed(dashAction)){
                    Vector3 lookDir = lookTransform.forward;
                    rb.AddForce(lookDir * playerStats.dashForce);
                    currState = DashState.COOLDOWN;
                }
                break;
            }
        }


    }
}