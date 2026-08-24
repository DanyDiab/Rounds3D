using UnityEngine;

public class CamFollowTarget : MonoBehaviour{
    [SerializeField] Camera cam;

    Transform camTrans;

    [SerializeField] Transform target;

    [SerializeField] float followSpeed;

    void Start(){
        camTrans = cam.transform;
        camTrans.position = target.position;
    }

    void translateCamera(){
        camTrans.position = target.position;
    }

    void Update(){
        translateCamera();
    } 
}