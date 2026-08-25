using UnityEngine;

public class CombineBullets : Card{
    [SerializeField] float ammoMultiplier;
    [SerializeField] float DamageMultiplier;

    public override void ApplyEffect(){
        playerStats.Ammo *= ammoMultiplier;
        playerStats.Damage *= DamageMultiplier;
    }
}