using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum EffectState{
    STOPPED,
    RUNNING
}

public class Dissolve : MonoBehaviour{
    Material targetMaterial;
    [SerializeField] [Range(0.0f, 1.0f)] float scrubProgress;
    List<Graphic> graphicsToDissolve;

    static readonly int ProgressProperty = Shader.PropertyToID("_Progress");
    
    void Awake(){
        targetMaterial = GetComponentInChildren<Material>();
    }

    public void StartEffect(GameObject parent){
        
    }

    void Update() {
        if(scrubProgress >= 1.0f){
        }

        if (targetMaterial == null) {
            return;
        }

        targetMaterial.SetFloat(ProgressProperty, Mathf.Clamp01(scrubProgress));
        scrubProgress += Time.deltaTime;
    }

}


