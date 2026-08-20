using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelector : MonoBehaviour{
    [SerializeField] GameObject cardParent;

    [SerializeField] float selectedScale;
    List<GameObject> cards;

    int selectedIndex = 0;

    bool hasKeyboard;



    List<GameObject> grabCards(){
        return cardParent.GetComponentsInChildren<GameObject>().ToList();
    }

// returns if a change occured
    bool changeSelectedIndex(){
        bool change = false;

        if(Input.GetKeyDown(KeyCode.D)){
            if(selectedIndex == cards.Count - 1) return false;
            selectedIndex++;
            change = true;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            if(selectedIndex == 0) return false;
            selectedIndex--;
            change = true;
        }
        
        return change;
    }


    void UpdateCardScales(){
        int idx = 0;
        foreach(GameObject card in cards){
            float newScale = idx == selectedIndex ? selectedScale : 1.0f;
            card.transform.localScale = Vector3.one * newScale; 
        }
    }
    void Update(){
        bool change = changeSelectedIndex();
        if(!change) return;

        UpdateCardScales();
    }

    void Start(){
        cards = grabCards();
    }

}