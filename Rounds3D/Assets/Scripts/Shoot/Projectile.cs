using UnityEngine;


public class Projectile : MonoBehaviour{

    int numBounced;
    PlayerStats stats;
    GameObject shooter;

    Rigidbody rb;
    [SerializeField] Collider projCollider;
    [SerializeField] ProjSize size;
    Collider shooterCollider;
    Vector3 lastVelocity;
    ExplosionManager explosionManager;
    int playerLayer;

    public delegate void ProjectileCollide(float damage);
    public static event ProjectileCollide OnProjectileCollide;



    public void init(PlayerStats playerStats, GameObject shooter, Vector3 shootDirection, ExplosionManager explosionManager, int playerLayer){
        numBounced = 0;
        stats = playerStats;

        this.shooter = shooter;
        this.explosionManager = explosionManager;
        this.playerLayer = playerLayer;
        rb = GetComponent<Rigidbody>();
        shooterCollider = shooter.GetComponentInChildren<Collider>();
        
        rb.AddForce(shootDirection.normalized * stats.BulletSpeed);
        size.init(playerStats);
    }

    void Update(){
        lastVelocity = rb.velocity;
    }


    void BounceProjectile(Collision collision){
        ContactPoint contact = collision.GetContact(0);
        Vector3 incomingDirection = lastVelocity.normalized;
        float speed = lastVelocity.magnitude;

        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, contact.normal);

        rb.velocity = reflectedDirection * speed * stats.SpeedIncreasePerBounce;
        rb.angularVelocity = Vector3.zero;
        transform.forward = reflectedDirection;
    }
    void OnCollisionEnter(Collision collision){
        if(stats.BulletExplosive) explosionManager.spawnExplosion(transform.position, 5.0f);

        // might not work for multiple players? well see :p
        Debug.Log(collision.gameObject.layer);

        if(collision.gameObject.layer == playerLayer){
            Debug.Log("player collide");
            OnProjectileCollide?.Invoke(stats.Damage);   
            Destroy(gameObject);
        }

        numBounced++;

        if(numBounced > stats.BulletBounces){
            Destroy(gameObject);
        }
        
        // Instantiate(explosionParent);
        BounceProjectile(collision);
    }

// either enter or exit 
// becareful with this, if we scale the player up, this requires that the projectile spawns in the player
    void OnTriggerExit(){
        projCollider.isTrigger = false;
    }
}