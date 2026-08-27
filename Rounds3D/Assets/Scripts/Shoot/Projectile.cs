using UnityEngine;


public class Projectile : MonoBehaviour{

    int numBounced;
    PlayerStats stats;
    GameObject shooter;

    Rigidbody rb;
    [SerializeField] Collider projCollider;
    Collider shooterCollider;
    Vector3 lastVelocity;
    ExplosionManager explosionManager;



    public void init(PlayerStats playerStats, GameObject shooter, Vector3 shootDirection, ExplosionManager explosionManager){
        numBounced = 0;
        stats = playerStats;

        this.shooter = shooter;
        this.explosionManager = explosionManager;
        rb = GetComponent<Rigidbody>();
        shooterCollider = shooter.GetComponentInChildren<Collider>();
        
        rb.AddForce(shootDirection.normalized * stats.BulletSpeed);

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
        
        // Instantiate(explosionParent);
        if(stats.BulletExplosive) explosionManager.spawnExplosion(transform.position, 1.0f);
        ContactPoint contact = collision.GetContact(0);
        Vector3 incomingDirection = lastVelocity.normalized;
        float speed = lastVelocity.magnitude;

        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, contact.normal);

        rb.velocity = reflectedDirection * speed * stats.SpeedIncreasePerBounce;
        rb.angularVelocity = Vector3.zero;
        transform.forward = reflectedDirection;
    }
}