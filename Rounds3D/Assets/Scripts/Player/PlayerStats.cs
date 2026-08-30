using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject{
    [Header("DPS")]
    public float Damage;
    public float RateOfFire; // this assumes Bullets Per Minute


    [Header("HP")]
    public float Health;
    public float Regen;

    [Header("Movement")]
    public float Speed;
    public float JumpForce;
    public float dashForce;
    public float Stamina;
    public float StaminaRegen;
    
    [Header("Misc")]
    public float CardsToPick;
    public float CardPoolSize;

    [Header("Reload")]
    public float Ammo;
    public float ReloadTimeMS;
    [Header("Bullet")]
    public float BulletSpeed;
    public float BulletBounces;
    public float SpeedIncreasePerBounce;
    public bool BulletExplosive;


}