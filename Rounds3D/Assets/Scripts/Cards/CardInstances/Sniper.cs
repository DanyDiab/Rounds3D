using UnityEngine;

public class Sniper : Card {
    [SerializeField] float bulletSpeedIncrease;
    [SerializeField] int numAmmo;
    [SerializeField] float bulletDamageIncrease;
    public override void ApplyEffect(){
        playerStats.Ammo = numAmmo;
        playerStats.BulletSpeed += bulletSpeedIncrease;
        playerStats.Damage += bulletDamageIncrease;
    }
}