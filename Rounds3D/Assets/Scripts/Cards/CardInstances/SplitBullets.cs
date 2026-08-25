using UnityEngine;

public class SplitBullets : Card{
    [SerializeField] float ammoMultiplier;
    [SerializeField] float DamageMultiplier;

    public override void ApplyEffect(){
        playerStats.Ammo *= ammoMultiplier;
        playerStats.Damage *= DamageMultiplier;
    }
}