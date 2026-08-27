using UnityEngine;

public class StopParticles : MonoBehaviour {
    [SerializeField] float timeAfterMS;
    float totalSeconds;

    [SerializeField] ParticleSystem system;


    float currTime;

    void Start(){
        totalSeconds = timeAfterMS / 1000.0f;
    }

    void Update(){
        
        currTime += Time.deltaTime;
        
        if(currTime >= totalSeconds){
            system.Stop();
            currTime = 0.0f;
        }
    }
}