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

        Vector2 centerScreen = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

        Vector2 spacingBetween = new Vector2(30.0f, 0.0f);

        Vector2 cardSize = new Vector2(200.0f, 300.0f);

        float xWidth = cardSize.x + spacingBetween.x;

        Vector2 spacing = new Vector2(xWidth, 0);

        Vector2 startingPos = centerScreen - (spacing * (cards.Count / 2));
 
        int idx = 0;
        foreach(GameObject card in cards){

            Vector2 pos = startingPos + (spacing * idx);
            Instantiate(card, pos, Quaternion.identity,parent.transform);
            idx++;
        }
    }
}