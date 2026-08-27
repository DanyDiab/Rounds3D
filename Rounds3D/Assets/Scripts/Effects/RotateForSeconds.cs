using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateForSeconds : MonoBehaviour{
    List<GameObject> objsToRot;
    List<Quaternion> originalQuats;

    Vector3 axis;
    float totalTime;
    float rotateSpeed;
    float currTime;


    void Start(){
        objsToRot = new List<GameObject>();
        originalQuats = new List<Quaternion>();
    }

    public void startRotate(GameObject parent, Vector3 axis, float timeToRotate = 1.0f, float rotateSpeed = 1.0f){
        this.axis = axis;
        this.totalTime = timeToRotate;
        this.rotateSpeed = rotateSpeed;

        for(int i = 0; i < parent.transform.childCount; i++){
            Transform child = parent.transform.GetChild(i);
            originalQuats.Add(child.localRotation);
            objsToRot.Add(child.gameObject);
        }

    }


    void Update(){  
        if(objsToRot.Count == 0) return;

        float step = rotateSpeed * Time.deltaTime;
        foreach(GameObject obj in objsToRot){
            obj.transform.Rotate(axis, step, Space.Self);
        }
        
        currTime += Time.deltaTime;

        if(currTime >= totalTime){
            for(int i = 0; i < originalQuats.Count; i++){
                GameObject obj = objsToRot[i];
                obj.transform.localRotation = originalQuats[i];
            }

            objsToRot.Clear();
            originalQuats.Clear();
            currTime = 0.0f;
        }

    }
}