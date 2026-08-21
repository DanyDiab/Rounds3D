using UnityEngine;

namespace GOTransforms{
    public class TranslateToTarget : MonoBehaviour{
        
        Vector3 startingPos;
        Transform trans;
        Vector3 targetPos;
        float timeToTake;

        float t;

        public void Init(Transform trans, Transform target, float timeToTake = 1.0f){
            this.trans = trans;
            this.targetPos = target.position;
            this.timeToTake = timeToTake;
            t = 0.0f;

            startingPos = trans.position;
        }

        float easeOutQuart(float t){
            return 1 - Mathf.Pow(1 - t, 4);
        }

        void Update(){
            if(t > 1.0f) Destroy(this);

            Vector3 newPos = Vector3.Lerp(startingPos, targetPos, t);

            trans.position = newPos;

            float delta = Time.deltaTime / timeToTake;
            t += delta;
        }

    }
}