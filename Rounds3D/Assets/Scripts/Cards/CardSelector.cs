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
    List<GameObject> cards;
    List<CardState> cardStates;

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
        foreach(GameObject card in cards){
            float newScale = idx == selectedIndex ? selectedScale : 1.0f;
            card.transform.localScale = Vector3.one * newScale;
            idx++;
        }
    }

    void SelectCurrentCard(){
        if(!ButtonPressUtil.Pressed(selectAction)) return;

        GameObject currCardGO = cards[selectedIndex];
        Card card = currCardGO.GetComponent<Card>();

        card.ApplyEffect();

        GOTransforms.TranslateToTarget translator = currCardGO.AddComponent<GOTransforms.TranslateToTarget>();
        
        translator.Init(currCardGO.transform, targetSelectPos, .5f);
    }

    void FlipSelectedCard(){
        CardState selectedCardState = cardStates[selectedIndex];

        if(selectedCardState == CardState.SHOWN) return;

        cardStates[selectedIndex] = CardState.SHOWN;
        GameObject selectedCard = cards[selectedIndex];

        GOTransforms.RotateToTarget rotator = selectedCard.AddComponent<GOTransforms.RotateToTarget>();

        Vector3 currRot = selectedCard.transform.rotation.eulerAngles;
        rotator.Init(new Vector3(currRot.x,180.0f,currRot.z),selectedCard.transform, .2f);
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

        cardStates = Enumerable.Repeat(CardState.HIDDEN, cards.Count).ToList();
        

        UpdateCardScales();
        FlipSelectedCard();
    }

}