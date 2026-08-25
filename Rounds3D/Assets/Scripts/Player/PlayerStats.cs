using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject{
    [Header("DPS")]
    public float Damage;
    public float RateOfFire;
    public float BulletSpeed;
    public float BulletBounces;

    [Header("HP")]
    public float Health;
    public float Regen;

    [Header("Speed")]
    public float Speed;
    
    [Header("Misc")]
    public float CardsToPick;

    [Header("Reload")]
    public float Ammo;
    public float ReloadSpeed;


}