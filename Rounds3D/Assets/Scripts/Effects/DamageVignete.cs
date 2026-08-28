using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

enum VignetteState{
    STABLE,
    FLASHING
}
public class DamageVignete : MonoBehaviour{
    [SerializeField] Volume volume;
    Vignette vignette;

    VignetteState currState;

    [SerializeField] float maxIntensity;

    [SerializeField] float minFlashIntensity;
    [SerializeField] float maxFlashIntensity;

    [SerializeField] float flashTimeSecond;
    float flashIntensity;
    float stableIntensity;

    float minIntensity;
    float time;


    void Start(){
        volume.profile.TryGet(out vignette);
        minIntensity = 0;
        currState = VignetteState.STABLE;
        vignette.intensity.value = minIntensity;
    }

    void OnEnable(){
        PlayerHealth.onDamage += setVigneteStrength;
    }

    void Update(){
        switch(currState){
            case VignetteState.STABLE:{
                break;
            }
            case VignetteState.FLASHING:{
                
                float val = Mathf.Sin((time / flashTimeSecond) * Mathf.PI) * flashIntensity;

                time += Time.deltaTime;

                vignette.intensity.value = stableIntensity + val;
                if(time >= flashTimeSecond){
                    currState = VignetteState.STABLE;
                    vignette.intensity.value = stableIntensity;
                    time = 0.0f;
                }
                break;
            }
        }
    }

    void setVigneteStrength(float percent, float percentLeft){
        // percentLeft will set the global min
        // percent will set a falsh

        float newIntensity = Mathf.Lerp(maxIntensity, minIntensity, percentLeft);
        
        vignette.intensity.value = newIntensity;
        currState = VignetteState.FLASHING;

        flashIntensity = Mathf.Lerp(minFlashIntensity, maxFlashIntensity, percent);
        stableIntensity = newIntensity;
    }
}