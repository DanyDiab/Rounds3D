using UnityEngine;

enum ShakeState {
    SHAKING,
    WAITING
}
// dont forget to initlize currSTate
public class CamShake : MonoBehaviour {
    Camera camera;

    float shakeTime;
    float magnitude;

    Quaternion originalRotation;
    float time;

    ShakeState currState;
    [SerializeField] CameraRotation camRotate;
    QuatRot camRotateQuatRot;
    Quaternion camRotateQuat;

    [Header("Explosion Shake Info")]
    [SerializeField] float minExplosionShakeMag;
    [SerializeField] float maxExplosionShakeMag;
    [SerializeField] float explosionShakeTime;


    void Start(){
        camRotateQuat = Quaternion.identity;
        camRotateQuatRot = new QuatRot(camRotateQuat);

        camera = Camera.main;
        currState = ShakeState.WAITING;

        startShake(2.0f, 3.0f);
    }

    void OnEnable(){
        ExplosionManager.onExplode += handleExplosionShake;
    }
    void OnDisable(){
        ExplosionManager.onExplode -= handleExplosionShake;
    }

    public void startShake(float shakeTime, float magnitude){
        this.shakeTime = shakeTime;
        this.magnitude = magnitude;
        originalRotation = camera.transform.rotation;
        currState = ShakeState.SHAKING;
        camRotateQuat = Quaternion.identity;
        camRotate.AddRotation(camRotateQuatRot);
    }


// rotatoins are overiding each other. 
    void Update(){
        switch(currState){
            case ShakeState.SHAKING:{
                Vector3 randRot = Random.onUnitSphere;
                Vector3 euler = randRot * magnitude + camera.transform.forward;
                camRotateQuatRot.quat = Quaternion.Euler(euler);

                time += Time.deltaTime;
                if(time >= shakeTime){
                    currState = ShakeState.WAITING;
                    time = 0.0f;
                    camRotate.SetRotation(originalRotation);
                    camRotate.RemoveRotation(camRotateQuatRot);
                }

                break;
            }
            case ShakeState.WAITING:{
                break;
            }
        }
    }

    void handleExplosionShake(Vector3 position, float maxDamageDistance){
        float distanceFromExplosion = (transform.position - position).magnitude;

        if(distanceFromExplosion > maxDamageDistance) return;

        float t = distanceFromExplosion / maxDamageDistance;

        float shakeMagnitude = Mathf.Lerp(maxExplosionShakeMag, minExplosionShakeMag, t);
        startShake(explosionShakeTime, shakeMagnitude);
    }
}