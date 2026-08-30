using UnityEngine;
public class Abundance : Card {
    [SerializeField] float increaseCardSize;
    public override void ApplyEffect()
    {
        playerStats.CardPoolSize += increaseCardSize;
    }
}