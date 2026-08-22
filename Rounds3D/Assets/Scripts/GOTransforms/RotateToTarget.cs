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
        EasingType easingType;

        public void Init(Vector3 end, Transform transform, float timeToTake = 1.0f, Action onComplete = null, EasingType easingType = EasingType.Linear){
            startingRot = transform.rotation.eulerAngles;

            endRot = end;
            this.timeToTake = timeToTake;
            transformToRot = transform;

            callBack = onComplete;
            this.easingType = easingType;
            
        }


        void Update(){
            if(t >= 1.0f) {
                if (callBack != null) {
                    callBack.DynamicInvoke(args);
                }
                Destroy(this);
                return;
            }

                        
            float delta = Time.deltaTime / timeToTake;

            t += delta;

            float easedT = EasingFunctions.Ease(t,easingType);

            Vector3 newRot = Vector3.Lerp(startingRot, endRot, easedT);

            transformToRot.rotation = Quaternion.Euler(newRot);

        }

    }
}