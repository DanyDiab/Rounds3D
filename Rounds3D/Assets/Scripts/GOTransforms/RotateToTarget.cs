using UnityEngine;

namespace GOTransforms{
    public class RotateToTarget : MonoBehaviour{
        Vector3 startingRot;
        Vector3 endRot;

        float t;

        float timeToTake;
        Transform transformToRot;

        public void Init(Vector3 end, Transform transform, float timeToTake = 1.0f){
            startingRot = transform.rotation.eulerAngles;

            endRot = end;
            this.timeToTake = timeToTake;   
            transformToRot = transform;
        }


        void Update(){
            if(t >= 1.0f) Destroy(this);

            Vector3 newRot = Vector3.Lerp(startingRot, endRot, t);

            transformToRot.rotation = Quaternion.Euler(newRot);
            
            float delta = Time.deltaTime / timeToTake;

            t += delta;
        }

    }
}