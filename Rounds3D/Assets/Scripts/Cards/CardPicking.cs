using System.Collections.Generic;
using UnityEngine;

public class CardPicking : MonoBehaviour{
    [SerializeField] int numCards;

    List<GameObject> cards;

    [SerializeField] List<GameObject> cardOptions;
    [SerializeField] GameObject parent;


    void Update(){
        
    }

    void Awake(){
        cards = new List<GameObject>();
        populateDisplay();
    }

    [ContextMenu("Populate Display")]
    void populateDisplay(){
        clearAllCards();
        populateCards();
        displayCards();
    }

    void populateCards(){
        for(int i = 0; i < numCards; i++){
            int randChoice = Random.Range(0, cardOptions.Count);
            cards.Add(cardOptions[randChoice]);
        }
    }

    void clearAllCards(){
        cards.Clear();
        for(int i = parent.transform.childCount - 1; i >= 0; i--){
            Transform child = parent.transform.GetChild(i);
            if(child == null){
                continue;
            }
            Destroy(child.gameObject);
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