using UnityEngine;

public class Tank : Card{
    [SerializeField] float speedDecrease;
    [SerializeField] float healthIncrease;

    public override void ApplyEffect(){
        playerStats.Speed -= speedDecrease;
        playerStats.Health += healthIncrease;

    }
}