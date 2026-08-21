using System.Collections.Generic;
using UnityEngine;

public class CardPicking : MonoBehaviour{
    [SerializeField] int numCards;

    List<GameObject> cards;

    [SerializeField] GameObject testCard;
    [SerializeField] GameObject parent;


    void Update(){
        
    }

    void Awake(){
        cards = new List<GameObject>();
        populateDisplay();
    }

    [ContextMenu("Populate Display")]
    void populateDisplay(){
        populateCards();
        displayCards();
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