using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVelocity : MonoBehaviour{

    Vector3 velocity;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float accelerationForce;
    [SerializeField] float decelerationForce;


    [SerializeField] GameObject footStart;

    InputAction leftAction;
    InputAction rightAction;
    InputAction upAction;
    InputAction downAction;
    InputAction jumpAction;

    [SerializeField] GameObject player;
    [SerializeField] LayerMask groundLayer;

    List<InputAction> inputs;
    [SerializeField] InputActionAsset inputActions;

    Rigidbody rb;



    bool grounded;

    void Start(){
        velocity = Vector3.zero;
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
        grounded = Physics.Raycast(footStart.transform.position,Vector3.down, 5.0f, groundLayer, QueryTriggerInteraction.Ignore);
    }

    Vector3 getJumpForce(){
        if(!jumpAction.triggered || !grounded) return Vector3.zero;

        Vector3 jumpvec = new Vector3(0.0f, jumpForce, 0.0f);

        return jumpvec;
    }


    void FixedUpdate(){
        rayCastCollideGround();
    }

    void Update(){
        Vector3 moveDir = readInputDir();
        
        Vector3 targetHorizontal = moveDir * speed;
        Vector3 currentHorizontal = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        float maxDelta = moveDir == Vector3.zero ? decelerationForce : accelerationForce;

        if(Vector3.Dot(targetHorizontal, currentHorizontal) < 0){
            currentHorizontal = Vector3.zero;
        }
        Vector3 horizontal = Vector3.MoveTowards(currentHorizontal, targetHorizontal, maxDelta * Time.deltaTime);

        rb.velocity = new Vector3(horizontal.x, rb.velocity.y, horizontal.z);
    }


    Vector3 readInputDir(){
        Vector3 input = Vector3.zero;

        if(!grounded) return input;

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