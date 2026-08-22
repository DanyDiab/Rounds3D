using UnityEngine;

namespace GOTransforms{
    public class TranslateToTarget : MonoBehaviour{
        
        Vector3 startingPos;
        Transform trans;
        Vector3 targetPos;
        float timeToTake;
        EasingType easingType;

        float t;

        public void Init(Transform trans, Transform target, float timeToTake = 1.0f, EasingType easingType = EasingType.Linear){
            this.trans = trans;
            this.targetPos = target.position;
            this.timeToTake = timeToTake;
            this.easingType = easingType;
            t = 0.0f;

            startingPos = trans.position;
        }



        void Update(){
            if(t > 1.0f) Destroy(this);

            float delta = Time.deltaTime / timeToTake;
            t += delta;

            float easedT = EasingFunctions.Ease(t, easingType);

            Vector3 newPos = Vector3.Lerp(startingPos, targetPos, easedT);

            trans.position = newPos;
        }

    }
}