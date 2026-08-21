using UnityEngine;

public class TestCard :MonoBehaviour, Card {
    public GameObject GO {get; set;}

    void Card.ApplyEffect(){
        return;
    }

    void Start(){
        GO = gameObject;
    }


}