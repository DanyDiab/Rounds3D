using UnityEngine;

public class LightWeight : Card{
    [SerializeField] float healthDecrease;
    [SerializeField] float speedIncrease;

    public override void ApplyEffect(){
        playerStats.Health -= healthDecrease;
        playerStats.Speed += speedIncrease;
    }
}