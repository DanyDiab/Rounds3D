using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

enum SpawnState{
    READY,
    STARTRELOAD,
    RELOADING,
}

public class ProjectileSpawner : MonoBehaviour{
    [SerializeField] InputActionAsset inputActions;

    InputAction shootAction;

// you might be wondering why we have rotation and position seperated here
// we do not rotate the player about the X axis, as to not topple them over.
// we only fully rotate the camera, sowe use its rotation to spawn the projetiles

    [SerializeField] GameObject projToSpawn;
    [Tooltip("This is who/where the spawning comes from (normally the player)")]
    [SerializeField] GameObject spawning;
    [SerializeField] GameObject projectileParent;
    [Tooltip("Rotation of the projectile")]
    [SerializeField] Transform shootRotation;
    [SerializeField] PlayerStats stats;
    
    int shotNumber;
    [SerializeField] InputAction reloadAction;
    [SerializeField] RotateForSeconds rotateForSeconds;
    [SerializeField] GameObject gun;
    [SerializeField] ExplosionManager explosionManager;
    [SerializeField] int playerLayer;
    SpawnState currState;


    void OnEnable(){
        shootAction.Enable();
    }

    void OnDisable(){
        shootAction.Disable();
    }

    void Awake(){
        shootAction = inputActions.FindAction("Shoot");
        reloadAction = inputActions.FindAction("Reload");
    }

    void Start(){
        currState = SpawnState.STARTRELOAD;
        shotNumber = 0;
    }

    float ROFToMS(){
        float bulletsPerSecond = stats.RateOfFire / 60.0f;
        return 1000.0f / bulletsPerSecond;
    }

    IEnumerator Reload(){
        currState = SpawnState.RELOADING;
        yield return new WaitForSeconds(stats.ReloadTimeMS / 1000.0f);
        shotNumber = 0;
        currState = SpawnState.READY;
    }

    void Update(){
        switch(currState){
            case SpawnState.READY:{
               bool reloadPressed = ButtonPressUtil.Pressed(reloadAction) && shotNumber != 0;
                if(shotNumber >= stats.Ammo || reloadPressed){
                    currState = SpawnState.STARTRELOAD;
                }
                
                if(ButtonPressUtil.Pressed(shootAction, ROFToMS())){
                   spawnProjectile();
                    shotNumber++;
                }

                break;
            }
            case SpawnState.STARTRELOAD:{
                float timeS = stats.ReloadTimeMS / 1000.0f;
                float rotationSpeed = 360.0f / timeS;
                rotateForSeconds.startRotate(gun,Vector3.right, stats.ReloadTimeMS / 1000.0f, rotationSpeed);
                StartCoroutine(Reload());
                break;
            }
            case SpawnState.RELOADING:{
                // do nothing here :p
                break;
            }

        }
    }

    void spawnProjectile(){
        GameObject projGO = Instantiate(projToSpawn, spawning.transform.position, spawning.transform.rotation, projectileParent.transform);
        Projectile proj = projGO.GetComponent<Projectile>();
        proj.init(stats,spawning, shootRotation.forward, explosionManager, playerLayer);

        // shake.startShake(1.0f, 200.0f);
    }
}