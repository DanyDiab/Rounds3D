using UnityEngine;

public class ProjSize : MonoBehaviour{

    PlayerStats stats;
    float damageToSize(){
        return stats.Damage / 10;
        
    }

    public void init(PlayerStats stats){
        this.stats = stats;
    }

    void Update(){
        transform.localScale = Vector3.one * damageToSize();
    }
}