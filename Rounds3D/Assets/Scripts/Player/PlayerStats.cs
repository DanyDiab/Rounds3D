using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject{
    [Header("DPS")]
    public float Damage;
    public float RateOfFire; // this assumes Bullets Per Minute
    public float BulletSpeed;
    public float BulletBounces;
    public float SpeedIncreasePerBounce;

    [Header("HP")]
    public float Health;
    public float Regen;

    [Header("Movement")]
    public float Speed;
    public float JumpForce;
    public float Stamina;
    public float StaminaRegen;
    
    [Header("Misc")]
    public float CardsToPick;

    [Header("Reload")]
    public float Ammo;
    public float ReloadTimeMS;



}