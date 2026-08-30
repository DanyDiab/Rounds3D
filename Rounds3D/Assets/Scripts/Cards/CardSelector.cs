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
    List<CardUI> cards;

    int selectedIndex = 0;

    InputAction leftAction;
    InputAction rightAction;
    InputAction selectAction;

    List<InputAction> actions;
    [SerializeField] InputActionAsset inputActions;

    [Header("Player Info")]
    [SerializeField] PlayerStats stats;
    [SerializeField] PlayerCards playerCards;


    int numPicked;

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

    List<CardUI> grabCards() {
        cards = cardParent.GetComponentsInChildren<CardUI>().ToList();
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
        foreach(CardUI card in cards){
            float newScale = idx == selectedIndex ? selectedScale : 1.0f;
            card.gameObject.transform.localScale = Vector3.one * newScale;
            idx++;
        }
    }

    bool SelectCurrentCard(){
        if(!ButtonPressUtil.Pressed(selectAction)) return false;

        CardUI card = cards[selectedIndex];

        playerCards.currCards.Add(cards[selectedIndex].card);
        card.card.ApplyEffect();

        GOTransforms.TranslateToTarget translator = card.gameObject.AddComponent<GOTransforms.TranslateToTarget>();
        
        translator.Init(card.transform, targetSelectPos.position, .5f, EasingType.EaseOutQuart);
        cards.RemoveAt(selectedIndex);

        selectedIndex = 0;
        numPicked++;
        return true;
    }

    void FlipSelectedCard(){
        cards[selectedIndex].FlipCard();
    }


    void yeetRemainingCards(){
        float distance = 1000.0f;
        float time = 3.0f;
        foreach(CardUI card in cards){
            Vector3 randomDir = Random.onUnitSphere;
            Vector3 randomRot = Random.onUnitSphere;

            GOTransforms.TranslateToTarget translator = card.gameObject.AddComponent<GOTransforms.TranslateToTarget>();
            translator.Init(card.transform,(distance * randomDir) + card.transform.position,timeToTake: time, easingType: EasingType.EaseOutQuart);

            GOTransforms.RotateToTarget rotator = card.gameObject.AddComponent<GOTransforms.RotateToTarget>();
            rotator.Init(randomDir * distance,card.transform, timeToTake: time);
        }
    }


    void Update(){
        if((cards?.Count ?? 0) == 0) return;

        if(numPicked >= stats.CardsToPick){
            yeetRemainingCards();
            Destroy(this);
            return;
        };

        bool selected = SelectCurrentCard();

        bool change = changeSelectedIndex();
        // might need another empty check here, since if we select the last card 
        if(!change && !selected) return;

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
        numPicked = 0;
        cards = grabCards();

        UpdateCardScales();
        FlipSelectedCard();
    }

}