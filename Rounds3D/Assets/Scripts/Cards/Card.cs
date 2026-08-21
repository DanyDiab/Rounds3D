using UnityEngine;

interface Card{
    void ApplyEffect();

    GameObject GO {get; set;}
    GameObject frontFace { get; }
    GameObject backFace { get; }
}