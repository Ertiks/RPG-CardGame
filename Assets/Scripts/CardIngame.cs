using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIngame //Class contenant le type de la carte, et son transform.
{

    //Le transform vaut NULL si la carte n'a jamais été instantié 
    //Utiliser le transform pour acceder directement au gameObject de la carte instantie (transform.gameObject)
    //L'ID pour l'unicite de chaque carte.

    public CardTypeSO cardSO;
    public int ID;
    public Transform cardInstance;

    //Pour trouver l'unite associee a la carte :
    public Transform unitInstance;
    public int IDunit;

    //Details de carte
    public bool shuffleEndTurn;
    public int manaCost;



    public CardIngame(CardTypeSO __cardSO, Transform __cardTransform, Transform __unitInstance, int __IDunit)
    {
        cardSO = __cardSO;
        cardInstance = __cardTransform;
        unitInstance = __unitInstance;
        IDunit = __IDunit;



        //Bonus :
        shuffleEndTurn = true;
        manaCost = cardSO.manaCost;
            
    }
}
