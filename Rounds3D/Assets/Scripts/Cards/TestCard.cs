using UnityEngine;

public class TestCard :MonoBehaviour, Card {
    public GameObject GO {get; set;}

    [SerializeField]
    public GameObject frontFace {get; set;}
    [SerializeField]
    public GameObject backFace {get; set;}

    void Card.ApplyEffect(){
        return;
    }

    void Start(){
        GO = gameObject;
    }


}