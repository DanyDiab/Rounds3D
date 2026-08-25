using UnityEngine;
using UnityEngine.InputSystem;


public class ProjectileSpawner : MonoBehaviour{
    [SerializeField] InputActionAsset inputActions;

    InputAction shootAction;

    [SerializeField] GameObject projToSpawn;

    void OnEnable(){
        shootAction.Enable();
    }

    void OnDisable(){
        shootAction.Disable();
    }

    void Start(){
        inputActions.FindAction("Shoot");
    }


    void Update(){
        if(ButtonPressUtil.Pressed(shootAction, 1000.0f)){
            spawnProjectile();
        }
    }

    void spawnProjectile(){
        Instantiate(projToSpawn);
    }
}