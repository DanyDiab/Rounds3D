using UnityEngine;

namespace GOTransforms{
    public class WiggleRotate : MonoBehaviour{
        [SerializeField] float frequency;
        [SerializeField] float amplitude;

        float startOffset;

        Vector3 eulerAngles;

        void Start(){
            eulerAngles = new Vector3();
            startOffset = Random.value * 100;
        }


        void Update(){
            // assume z roatation for now
            eulerAngles = transform.rotation.eulerAngles;
            float rotation = Mathf.Sin((Time.time * frequency) + startOffset) * amplitude;
            eulerAngles.z = rotation;
            transform.rotation = Quaternion.Euler(eulerAngles);
        }

    }
}