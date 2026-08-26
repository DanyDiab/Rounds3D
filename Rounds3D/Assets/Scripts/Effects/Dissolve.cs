using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum EffectState{
    STOPPED,
    RUNNING
}

public class Dissolve : MonoBehaviour{
    [SerializeField] Material dissolveMat;
    [SerializeField] [Range(0.0f, 1.0f)] float scrubProgress;
    List<Graphic> graphicsToDissolve;

    EffectState currState;

    static readonly int ProgressProperty = Shader.PropertyToID("_Progress");

    void Start(){
        currState = EffectState.STOPPED;
    }
    
    public void StartEffect(GameObject parent){
        graphicsToDissolve = parent.GetComponentsInChildren<Graphic>().ToList();
        foreach(Graphic graphic in graphicsToDissolve){
            graphic.material = dissolveMat;
        }

    }

    void Update() {
        switch(currState){
            case EffectState.STOPPED:{
                break;
            }
            case EffectState.RUNNING:{
                if(scrubProgress >= 1.0f){
                    currState = EffectState.STOPPED;
                    scrubProgress = 0.0f;
                    return;
                }

                dissolveMat.SetFloat(ProgressProperty, Mathf.Clamp01(scrubProgress));
                scrubProgress += Time.deltaTime;        
                break;
            }
        }


        
    }

}


