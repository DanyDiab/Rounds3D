using UnityEngine;

public class ReloadManiac : Card{

    [SerializeField] float multPerReload;
    [SerializeField] float reloadSpeedDecrease;
    public override void ApplyEffect(){
        playerStats.ReloadTimeMS -= reloadSpeedDecrease;
    }

    void increaseMult(){
        playerStats.Damage *= multPerReload;
    }

    void OnEnable(){
        ProjectileSpawner.OnReload += increaseMult;
    }

    void OnDisable(){
        ProjectileSpawner.OnReload -= increaseMult;
    }
}