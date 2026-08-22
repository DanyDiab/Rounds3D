using System;
using UnityEngine;

namespace GOTransforms{
    public class RotateToTarget : MonoBehaviour{
        Vector3 startingRot;
        Vector3 endRot;

        float t;

        float timeToTake;
        Transform transformToRot;
        Action callBack;
        object[] args;

        public void Init(Vector3 end, Transform transform, float timeToTake = 1.0f, Action onComplete = null){
            startingRot = transform.rotation.eulerAngles;

            endRot = end;
            this.timeToTake = timeToTake;
            transformToRot = transform;

            callBack = onComplete;
            
        }


        void Update(){
            if(t >= 1.0f) {
                if (callBack != null) {
                    callBack.DynamicInvoke(args);
                }
                Destroy(this);
                return;
            }

            Vector3 newRot = Vector3.Lerp(startingRot, endRot, t);

            transformToRot.rotation = Quaternion.Euler(newRot);
            
            float delta = Time.deltaTime / timeToTake;

            t += delta;
        }

    }
}