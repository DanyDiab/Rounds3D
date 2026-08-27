using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum GroundState{
    GROUNDED,
    AIR
}

public class PlayerVelocity : MonoBehaviour{

    [SerializeField] PlayerStats stats;
    [SerializeField] float accelerationForce;
    [SerializeField] float decelerationForce;
    [SerializeField] float airAccelForce;
    [SerializeField] float groundRayCastSize;
    [SerializeField] float airControlFactor;


    [SerializeField] GameObject footStart;

    InputAction leftAction;
    InputAction rightAction;
    InputAction upAction;
    InputAction downAction;
    InputAction jumpAction;
    InputAction crouchAction;

    [SerializeField] GameObject player;
    [SerializeField] LayerMask groundLayer;

    List<InputAction> inputs;
    [SerializeField] InputActionAsset inputActions;

    Rigidbody rb;

    Vector3 lastKnownGroundedVelocity;
    GroundState currState;





    void Start(){
        lastKnownGroundedVelocity = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        leftAction = inputActions.FindAction("Left");
        rightAction = inputActions.FindAction("Right");
        upAction = inputActions.FindAction("Up");
        downAction = inputActions.FindAction("Down");
        jumpAction = inputActions.FindAction("Jump");

        inputs = new List<InputAction>{
            leftAction,
            rightAction,
            upAction,
            downAction,
            jumpAction
        };
    }

    void OnEnable(){
        foreach(InputAction action in inputs){
            action.Enable();
        }
    }

    void OnDisable(){
        foreach(InputAction action in inputs){
            action.Disable();
        }
    }

    void rayCastCollideGround(){
        bool groundCollide = Physics.Raycast(footStart.transform.position,Vector3.down, groundRayCastSize, groundLayer, QueryTriggerInteraction.Ignore);

        currState = groundCollide ? GroundState.GROUNDED : GroundState.AIR;
    }

    Vector3 getJumpForce(){
        if(!jumpAction.triggered) return Vector3.zero;

        Vector3 jumpvec = new Vector3(0.0f, stats.JumpForce, 0.0f);

        return jumpvec;
    }


    void FixedUpdate(){
        rayCastCollideGround();
    }

    float determineAccelForce(Vector3 moveDir){
        float maxDelta = moveDir == Vector3.zero ? decelerationForce : accelerationForce;

        return maxDelta;
    }

    void DetermineVelocity(){
        Vector3 moveDir = readInputDir();

        switch(currState){
            case GroundState.GROUNDED:{
                Vector3 targetHorizontal = moveDir * stats.Speed;

                Vector3 currentHorizontal = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

                if(Vector3.Dot(targetHorizontal, currentHorizontal) < 0){
                   currentHorizontal = Vector3.zero;
                }
        
                float maxDelta = determineAccelForce(moveDir);

                Vector3 horizontalTowardsTarget = Vector3.MoveTowards(currentHorizontal, targetHorizontal, maxDelta * Time.deltaTime);
                rb.velocity = new Vector3(horizontalTowardsTarget.x, rb.velocity.y, horizontalTowardsTarget.z);

                rb.AddForce(getJumpForce());

                lastKnownGroundedVelocity = horizontalTowardsTarget;
                break;
            }
            case GroundState.AIR:{
                Vector3 targetHorizontal = ((moveDir * airControlFactor) * stats.Speed) +  ((1.0f - airControlFactor) * lastKnownGroundedVelocity);
                Vector3 currentHorizontal = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                
                Vector3 horizontalTowardsTarget = Vector3.MoveTowards(currentHorizontal, targetHorizontal, airAccelForce * Time.deltaTime);
                rb.velocity = new Vector3(horizontalTowardsTarget.x, rb.velocity.y, horizontalTowardsTarget.z);

                break;
            }
        }
    }

    void Update(){
        
        DetermineVelocity();



        



    }


    Vector3 readInputDir(){
        Vector3 input = Vector3.zero;

        if(leftAction.IsPressed()){
            input -= player.transform.right;
        }
        if(rightAction.IsPressed()){
            input += player.transform.right;

        }
        if(downAction.IsPressed()){
            input -= player.transform.forward;
        }
        if(upAction.IsPressed()){
            input += player.transform.forward;
        }

        return input.normalized;
    }
}