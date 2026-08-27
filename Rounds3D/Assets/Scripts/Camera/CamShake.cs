using UnityEngine;

enum ShakeState {
    SHAKING,
    WAITING
}

public class CamShake : MonoBehaviour {
    Camera camera;

    float shakeTime;
    float magnitude;

    Quaternion originalRotation;
    float time;

    ShakeState currState;

    void Start(){
        camera = Camera.main;
    }
    public void startShake(float shakeTime, float magnitude){
        this.shakeTime = shakeTime;
        this.magnitude = magnitude;
        originalRotation = camera.transform.rotation;
        currState = ShakeState.SHAKING;
    }



// rotatoins are overiding each other. 
    void Update(){
        switch(currState){
            case ShakeState.SHAKING:{
                Vector3 randRot = Random.onUnitSphere;
                Vector3 euler = randRot * magnitude + camera.transform.forward;
                camera.transform.rotation = Quaternion.Euler(euler);

                time += Time.deltaTime;
                Debug.Log("SHAEK");
                if(time >= shakeTime){
                    currState = ShakeState.WAITING;
                    time = 0.0f;
                    transform.rotation = originalRotation;
                }

                break;
            }
            case ShakeState.WAITING:{
                break;
            }
        }
    }
}