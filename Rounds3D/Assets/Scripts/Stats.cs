using System.Collections.Generic;
using UnityEngine;

struct StatInfo{
    public StatType statType;
    public float changeAmount;
};

enum StatType{
    HP,
    DMG,
    FIRERATE
};

class Stats{

    void applyStat(StatInfo statInfo){
        switch(statInfo.statType){
            case StatType.HP:{
                break;
            }
            case StatType.DMG:{
                break;
            }
            case StatType.FIRERATE:{
                break;
            }
            default:
                Debug.LogError("Unkown Stat Type!");
                break;
            
        }
    }
    public void applyStats(List<StatInfo> statsToApply){
        foreach(StatInfo statInfo in statsToApply){
            applyStat(statInfo);
        }
    }
}


