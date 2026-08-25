using UnityEngine;

public class SpeedCard : Card{
    [SerializeField] float speedIncrease;
    public override void ApplyEffect(){
        playerStats.Speed += speedIncrease;
    }
}