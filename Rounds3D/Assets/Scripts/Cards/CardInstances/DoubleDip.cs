using UnityEngine;

public class DoubleDip : Card{

    public override void ApplyEffect()
    {
        playerStats.CardsToPick *= 2;
    }
}