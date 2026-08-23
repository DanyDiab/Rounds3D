using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVelocity : MonoBehaviour{

    Vector3 velocity;

    InputAction leftAction;
    InputAction rightAction;
    InputAction upAction;
    InputAction downAction;

    [SerializeField] GameObject player;

    List<InputAction> inputs;
    [SerializeField] InputActionAsset inputActions;

    void Start(){
        velocity = Vector3.zero;
    }

    void Awake()
    {
        leftAction = inputActions.FindAction("Left");
        rightAction = inputActions.FindAction("Right");
        upAction = inputActions.FindAction("Up");
        downAction = inputActions.FindAction("Down");

        inputs = new List<InputAction>{
            leftAction,
            rightAction,
            upAction,
            downAction
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



    void Update(){
        player.transform.position += readInputDir() * 10f;
    }


    Vector3 readInputDir(){
        Vector3 input = Vector3.zero;

        if(leftAction.IsPressed()){
            input.x += 1.0f;
        }
        if(rightAction.IsPressed()){
            input.x -= 1.0f;
        }
        if(downAction.IsPressed()){
            input.z -= 1.0f;
        }
        if(upAction.IsPressed()){
            input.z += 1.0f;
        }

        return input.normalized;
    }
}