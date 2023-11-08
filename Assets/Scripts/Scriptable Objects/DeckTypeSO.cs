using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Deck")]
public class DeckTypeSO : ScriptableObject
{
    public List<CardTypeSO> decklist;
    public int classe; //1 mage, 2 pretre, 3 guerrier.
}
