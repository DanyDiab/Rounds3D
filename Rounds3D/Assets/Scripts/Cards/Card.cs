using UnityEngine;

interface Card{
    void ApplyEffect();

    void ShowFace(CardState state);

    GameObject GO {get; set;}
    GameObject frontFace {get; }
    GameObject backFace { get; }
}