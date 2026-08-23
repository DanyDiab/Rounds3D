using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardSelector : MonoBehaviour{
    [SerializeField] GameObject cardParent;

    [SerializeField] float selectedScale;
    [SerializeField] Transform targetSelectPos;
    List<Card> cards;

    int selectedIndex = 0;

    InputAction leftAction;
    InputAction rightAction;
    InputAction selectAction;

    List<InputAction> actions;
    [SerializeField] InputActionAsset inputActions;

    void OnEnable(){
        foreach(InputAction action in actions){
            action.Enable();
        }

    }

    void OnDisable(){
        foreach(InputAction action in actions){
            action.Disable();
        }
    }

    List<Card> grabCards() {
        cards = cardParent.GetComponentsInChildren<Card>().ToList();
        return cards;
    }

// returns if a change occured
    bool changeSelectedIndex(){
        bool change = false;

        if(ButtonPressUtil.Pressed(rightAction)){
            if(selectedIndex == cards.Count - 1) return false;
            selectedIndex++;
            change = true;
        }
        else if(ButtonPressUtil.Pressed(leftAction)){
            if(selectedIndex == 0) return false;
            selectedIndex--;
            change = true;
        }
        
        return change;
    }


    void UpdateCardScales(){
        int idx = 0;
        foreach(Card card in cards){
            float newScale = idx == selectedIndex ? selectedScale : 1.0f;
            card.gameObject.transform.localScale = Vector3.one * newScale;
            idx++;
        }
    }

    void SelectCurrentCard(){
        if(!ButtonPressUtil.Pressed(selectAction)) return;

        Card card = cards[selectedIndex];

        card.ApplyEffect();

        GOTransforms.TranslateToTarget translator = card.gameObject.AddComponent<GOTransforms.TranslateToTarget>();
        
        translator.Init(card.transform, targetSelectPos, .5f, EasingType.EaseOutQuart);
        cards.RemoveAt(selectedIndex);
    }

    void FlipSelectedCard(){
        cards[selectedIndex].FlipCard();
    }



    void Update(){
        SelectCurrentCard();

        bool change = changeSelectedIndex();
        if(!change) return;

        FlipSelectedCard();
        UpdateCardScales();

    }

    void Awake(){
        leftAction = inputActions.FindAction("Left");
        rightAction = inputActions.FindAction("Right");
        selectAction = inputActions.FindAction("Select");

        actions = new List<InputAction>{
            leftAction,
            rightAction,
            selectAction
        };
    }

    void Start(){
        cards = grabCards();

        UpdateCardScales();
        FlipSelectedCard();
    }

}