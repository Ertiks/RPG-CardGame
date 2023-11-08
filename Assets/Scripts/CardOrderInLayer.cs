using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOrderInLayer : MonoBehaviour
{
    //Met en avant les cartes quand on passe la souris dessus.

    private SpriteRenderer spriteR;
    private SpriteRenderer cardSelector;

    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
        cardSelector = transform.Find("CardSelector").GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        spriteR.sortingOrder += 2;
        cardSelector.sortingOrder += 2;
    }

    private void OnMouseExit()
    {
        spriteR.sortingOrder -= 2;
        cardSelector.sortingOrder -= 2;
    }
}
