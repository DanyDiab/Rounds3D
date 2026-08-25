using UnityEngine;

public class RunningShoes : Card{
    [SerializeField] float speedIncrease;
    [SerializeField] int staminaIncrease;


    public override void ApplyEffect(){
        playerStats.Stamina += staminaIncrease;
        playerStats.Speed += speedIncrease;
    }
}