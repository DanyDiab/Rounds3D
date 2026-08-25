using UnityEngine;

public class HealthIncreaseCard : Card{
    [SerializeField] float healthIncrease;
    public override void ApplyEffect(){
        playerStats.Health += healthIncrease;
    }
}