using UnityEngine;

public class MachineGun : Card{
    [SerializeField] float speedMultiplier;
    [SerializeField] float fireRateIncrease;
    [SerializeField] float ammoIncrease;

    public override void ApplyEffect(){
        playerStats.Speed *= speedMultiplier;
        playerStats.RateOfFire += fireRateIncrease;
        playerStats.Ammo += ammoIncrease;
    }
}