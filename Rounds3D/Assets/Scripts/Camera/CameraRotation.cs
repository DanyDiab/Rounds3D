using System.Collections.Generic;
using UnityEngine;

public class QuatRot{
    public Quaternion quat;

    public QuatRot(Quaternion quat){
        this.quat = quat;
    }
}
public class CameraRotation : MonoBehaviour{
    List<QuatRot> rotations;
    Camera camera;

    void Start(){
        camera = Camera.main;
    }

    void Awake(){
        rotations = new List<QuatRot>();
    }
    public void AddRotation(QuatRot quat){
        rotations.Add(quat);
    }

    public void RemoveRotation(QuatRot quat){
        rotations.Remove(quat);
    }

    void Update(){
        Quaternion masterQuat = Quaternion.identity;

        foreach(QuatRot quat in rotations){
            masterQuat *= quat.quat;
        }
        
        camera.transform.rotation = masterQuat;
    }

    public void SetRotation(Quaternion rotation){
        camera.transform.rotation = rotation;
    }
}