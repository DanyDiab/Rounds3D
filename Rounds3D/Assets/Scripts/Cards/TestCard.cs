using UnityEngine;

public class TestCard :MonoBehaviour, Card {
    public GameObject GO {get; set;}

    [SerializeField] private GameObject FrontFace;
    [SerializeField] private GameObject BackFace;

    public GameObject frontFace => FrontFace;
    public GameObject backFace => BackFace;

    void Card.ApplyEffect(){
        return;
    }

    void Card.ShowFace(CardState state){

        FrontFace.SetActive(false);
        BackFace.SetActive(false);

        bool isShown = state == CardState.SHOWN;
        FrontFace.SetActive(isShown);
        BackFace.SetActive(!isShown);
    }

    void Start(){
        GO = gameObject;
    }


}