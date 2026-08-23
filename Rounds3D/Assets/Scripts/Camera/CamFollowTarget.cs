using UnityEngine;

public class CamFollowTarget : MonoBehaviour{
    [SerializeField] Camera cam;

    Transform camTrans;

    [SerializeField] Transform target;

    [SerializeField] float followSpeed;

    void Start(){
        camTrans = cam.transform;
    }

    void translateCamera(){
        camTrans.position = target.position;
    }

    void rotateCamera(){
        camTrans.rotation = target.rotation;
    }
    void Update(){
        translateCamera();
        rotateCamera();
    } 
}