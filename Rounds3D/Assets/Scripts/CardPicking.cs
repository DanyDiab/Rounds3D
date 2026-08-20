using System.Collections.Generic;
using UnityEngine;

public class CardPicking : MonoBehaviour{
    int numCards;

    List<GameObject> cards;

    GameObject testCard;
    [SerializeField] GameObject parent;


    void Update(){
        
    }

    void Start(){

    }

    void populateCards(){
        for(int i = 0; i < numCards; i++){
            cards.Add(testCard);
        }
    }

    void displayCards(){
        foreach(GameObject card in cards){
            Instantiate(card, Vector3.zero, Quaternion.identity,parent.transform);
        }
    }
}