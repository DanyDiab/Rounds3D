using UnityEngine;

public abstract class Card : MonoBehaviour{
    public abstract void ApplyEffect();

    [SerializeField] protected PlayerStats playerStats;

    
}