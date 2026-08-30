using UnityEngine;

public class CardUI : MonoBehaviour{
    [SerializeField] GameObject frontFace;

    [SerializeField] GameObject backFace;

    CardState cardState;

    public Card card;

    void Update(){
        if(cardState != CardState.FLIPPING) return;

        FlipCardSprite();
    }

    void Start(){
        card = GetComponent<Card>();
        ShowFace(CardState.HIDDEN);
    }

    public void FlipCard(){
        if(cardState == CardState.SHOWN) return;

        cardState = CardState.FLIPPING;

        GOTransforms.RotateToTarget rotator = gameObject.AddComponent<GOTransforms.RotateToTarget>();

        Vector3 currRot = transform.rotation.eulerAngles;

        // initlize the rotation, then make a lambda callback to show the shown face of the card
        rotator.Init(new Vector3(currRot.x,180.0f,currRot.z),
            transform, 
            .75f, 
            easingType: EasingType.Linear
        );
    }

    public void ShowFace(CardState state){
        frontFace.SetActive(false);
        backFace.SetActive(false);

        switch(state){
            case CardState.HIDDEN:
                backFace.SetActive(true);
                break;
            case CardState.SHOWN:
                frontFace.SetActive(true);
                break;
        }
    }

    protected void FlipCardSprite() {
        Vector3 toCamera = (Camera.main.transform.position - transform.position).normalized;

        Vector3 projectedToCamera = Vector3.ProjectOnPlane(toCamera, transform.up).normalized;
        Vector3 projectedForward  = Vector3.ProjectOnPlane(backFace.transform.forward, transform.up).normalized;

        float dotted = Vector3.Dot(projectedToCamera, projectedForward);

        if (dotted  < -.99f) {
            cardState = CardState.SHOWN;
            ShowFace(cardState);
        }
    }
}