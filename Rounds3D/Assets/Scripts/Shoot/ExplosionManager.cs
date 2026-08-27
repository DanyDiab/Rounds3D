using UnityEngine;

public class ExplosionManager : MonoBehaviour{
    [SerializeField] GameObject explosionEffect;
    
    [SerializeField] GameObject effectParent;


    public delegate void explosionEvent(Vector3 position);
    public static event explosionEvent onExplode;
    public void spawnExplosion(Vector3 position, float size){
        GameObject newExplosion = Instantiate(explosionEffect, position, Quaternion.identity, effectParent.transform);

        onExplode?.Invoke(position);
    }
}