using UnityEngine;


public class Projectile : MonoBehaviour{

    int numBounced;
    PlayerStats stats;
    GameObject shooter;

    Rigidbody rb;
    [SerializeField] Collider projCollider;
    Collider shooterCollider;
    Vector3 lastVelocity;


    public void init(PlayerStats playerStats, GameObject shooter, Vector3 shootDirection){
        numBounced = 0;
        stats = playerStats;

        this.shooter = shooter;
        rb = GetComponent<Rigidbody>();
        shooterCollider = shooter.GetComponentInChildren<Collider>();
        
        rb.AddForce(shootDirection.normalized * 5000);

        Physics.IgnoreCollision(projCollider, shooterCollider, true);
    }

    void Update(){
        lastVelocity = rb.velocity;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(numBounced == 0 && collision.gameObject == shooter){
            Physics.IgnoreCollision(projCollider, shooterCollider, false);
        }

        numBounced++;

        if(numBounced > stats.BulletBounces){
            Destroy(gameObject);
        }
        
        ContactPoint contact = collision.GetContact(0);
        Vector3 incomingDirection = lastVelocity.normalized;
        float speed = lastVelocity.magnitude;

        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, contact.normal);

        rb.velocity = reflectedDirection * speed * stats.speedIncreasePerBounce;
        rb.angularVelocity = Vector3.zero;
        transform.forward = reflectedDirection;
    }
}