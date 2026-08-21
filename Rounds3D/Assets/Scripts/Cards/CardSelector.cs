using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardSelector : MonoBehaviour{
    [SerializeField] GameObject cardParent;

    [SerializeField] float selectedScale;
    List<GameObject> cards;

    int selectedIndex = 0;

    bool hasKeyboard;

    InputAction leftAction;
    InputAction rightAction;
    [SerializeField] private InputActionAsset inputActions;

    List<GameObject> grabCards() {
        if (cardParent == null) {
            return new List<GameObject>();
        }

        Transform parentTransform = cardParent.transform;
        List<GameObject> cards = new List<GameObject>(parentTransform.childCount);

        for (int i = 0; i < parentTransform.childCount; i++) {
            cards.Add(parentTransform.GetChild(i).gameObject);
        }

        return cards;
    }

// returns if a change occured
    bool changeSelectedIndex(){
        bool change = false;

        if(ButtonPressUtil.Pressed(leftAction)){
            Debug.Log("Left");
            if(selectedIndex == cards.Count - 1) return false;
            selectedIndex++;
            change = true;
        }
        else if(ButtonPressUtil.Pressed(rightAction)){
            Debug.Log("Right");
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


        Debug.Log(selectedIndex);
        UpdateCardScales();
    }

    void Awake(){
        leftAction = inputActions.FindAction("Left");
        rightAction = inputActions.FindAction("Right");
    }

    void Start(){
        cards = grabCards();
        hasKeyboard = Keyboard.current != null;
        UpdateCardScales();
    }

}