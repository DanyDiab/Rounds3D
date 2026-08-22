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
    List<CardInfo> cards;

    int selectedIndex = 0;

    InputAction leftAction;
    InputAction rightAction;
    InputAction selectAction;

    List<InputAction> actions;
    [SerializeField] private InputActionAsset inputActions;

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

    List<CardInfo> grabCards() {
        cards = new List<CardInfo>();
        if (cardParent == null) {
            return cards;
        }

        Transform parentTransform = cardParent.transform;

        for (int i = 0; i < parentTransform.childCount; i++) {
            CardInfo ci;

            GameObject child = parentTransform.GetChild(i).gameObject;
            ci.GO = child;
            ci.card = child.GetComponent<Card>();
            ci.cardState = CardState.HIDDEN;

            cards.Add(ci);
        }

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
        foreach(CardInfo card in cards){
            float newScale = idx == selectedIndex ? selectedScale : 1.0f;
            card.GO.transform.localScale = Vector3.one * newScale;
            idx++;
        }
    }

    void SelectCurrentCard(){
        if(!ButtonPressUtil.Pressed(selectAction)) return;

        GameObject currCardGO = cards[selectedIndex].GO;
        Card card = cards[selectedIndex].card;

        card.ApplyEffect();

        GOTransforms.TranslateToTarget translator = currCardGO.AddComponent<GOTransforms.TranslateToTarget>();
        
        translator.Init(currCardGO.transform, targetSelectPos, .5f, EasingType.EaseOutQuart);
        cards.RemoveAt(selectedIndex);
    }

    void FlipSelectedCard(){
        CardState selectedCardState = cards[selectedIndex].cardState;

        if(selectedCardState == CardState.SHOWN) return;

        CardInfo ci = cards[selectedIndex];
        ci.cardState = CardState.SHOWN;
        cards[selectedIndex] = ci;

        GameObject selectedCard = cards[selectedIndex].GO;

        GOTransforms.RotateToTarget rotator = selectedCard.AddComponent<GOTransforms.RotateToTarget>();

        Vector3 currRot = selectedCard.transform.rotation.eulerAngles;

// initlize the rotation, then make a lambda callback to show the shown face of the card
        rotator.Init(new Vector3(currRot.x,180.0f,currRot.z),
            selectedCard.transform, 
            .4f, 
            () => cards[selectedIndex].card.ShowFace(CardState.SHOWN), 
            EasingType.EaseInExpo
        );
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