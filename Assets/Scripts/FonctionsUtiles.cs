using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionsUtiles : MonoBehaviour
{
    //Fonctions/Methodes Utiles a utiliser un peu partout !

    //Shuffle de liste :






    //================================================================//
    //===================Fisher_Yates_CardDeck_Shuffle====================//
    //================================================================//

    /// With the Fisher-Yates shuffle, first implemented on computers by Durstenfeld in 1964, 
    ///   we randomly sort elements. This is an accurate, effective shuffling method for all array types.

    public static List<CardIngame> Fisher_Yates_CardDeck_Shuffle(List<CardIngame> aList)
    {

        System.Random _random = new System.Random();

        CardIngame myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }


}
