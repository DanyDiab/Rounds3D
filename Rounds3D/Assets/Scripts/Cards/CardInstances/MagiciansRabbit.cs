using UnityEngine;
public class MagiciansRabbit : Card {
    [SerializeField] float increaseCardSize;
    public override void ApplyEffect()
    {
        playerStats.CardPoolSize += increaseCardSize;
    }
}