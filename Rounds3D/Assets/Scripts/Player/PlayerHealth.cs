using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] PlayerStats playerStats;
    float currHealth;

    public delegate void DamageEvent(float percent, float percentLeft);

    public static event DamageEvent onDamage;
    void Start(){
        currHealth = playerStats.Health;
    }
    void OnEnable(){
        Projectile.OnProjectileCollide += takeDamage;
    }

    void OnDisable(){
        Projectile.OnProjectileCollide -= takeDamage;
    }
    public void takeDamage(float amount){
        currHealth -= amount;

        if(currHealth <= 0){
            Debug.Log("YOU ARE DEAD MF");
        }
        Debug.Log(currHealth);

        onDamage?.Invoke(amount / playerStats.Health, currHealth / playerStats.Health);
    }



}