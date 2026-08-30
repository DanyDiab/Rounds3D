

using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour{
    public List<Card> currCards;

    public List<Card> allOptions;

    PlayerCards Instance;

    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}