using UnityEngine;
using UnityEngine.InputSystem;


public class ProjectileSpawner : MonoBehaviour{
    [SerializeField] InputActionAsset inputActions;

    InputAction shootAction;

    [SerializeField] GameObject projToSpawn;
    [Tooltip("This is who/where the spawning comes from (normally the player)")]
    [SerializeField] GameObject spawning;
    [SerializeField] GameObject projectileParent;
    [Tooltip("Rotation of the projectile")]
    [SerializeField] Transform shootRotation;
    [SerializeField] PlayerStats stats;

    void OnEnable(){
        shootAction.Enable();
    }

    void OnDisable(){
        shootAction.Disable();
    }

    void Awake(){
        shootAction = inputActions.FindAction("Shoot");
    }


    void Update(){
        if(ButtonPressUtil.Pressed(shootAction, 1000.0f)){
            spawnProjectile();
        }
    }

    void spawnProjectile(){
        GameObject projGO = Instantiate(projToSpawn, spawning.transform.position, spawning.transform.rotation, projectileParent.transform);
        Projectile proj = projGO.GetComponent<Projectile>();
        proj.init(stats,spawning, shootRotation.forward);
    }
}