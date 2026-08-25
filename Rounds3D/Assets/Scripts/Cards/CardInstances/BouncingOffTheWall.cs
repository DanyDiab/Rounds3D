using UnityEngine;

public class BouncingOffTheWall : Card {
    [SerializeField] float increasePerBounce;
    [SerializeField] float numBouceIncrease;

    public override void ApplyEffect(){
        playerStats.BulletBounces += numBouceIncrease;
        playerStats.speedIncreasePerBounce += increasePerBounce;
    }
}