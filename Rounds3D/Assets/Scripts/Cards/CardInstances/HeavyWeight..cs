using UnityEngine;

public class HeavyWeight : Card{
    [SerializeField] float healthIncrease;
    [SerializeField] float speedDecrease;

    public override void ApplyEffect(){
        playerStats.Health += healthIncrease;
        playerStats.Speed -= speedDecrease;
    }
}