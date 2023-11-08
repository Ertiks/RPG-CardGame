using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private static DeckManager instance;

    [SerializeField] private int[] idHeros;

    // Liste des cartes du deck courant
    [SerializeField] private List<CardTypeSO> Decklist0;
    [SerializeField] private List<CardTypeSO> Decklist1;
    //[SerializeField] private List<CardTypeSO> Decklist2;
    [SerializeField] private List<CardTypeSO>[] deckLists;


    private void Awake ()
    {
        deckLists = new List<CardTypeSO>[] {Decklist0, Decklist1 };
    }

    public static DeckManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DeckManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<DeckManager>();
                    singletonObject.name = "DeckManagerSingleton";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    // Ajoutez ici les méthodes pour accéder et modifier les données
    public List<CardTypeSO>[] DeckLists
    {
        get { return deckLists; }
        set { deckLists = value; }
    }

    public List<CardTypeSO> DeckList0
    {
        get { return DeckList0; }
        set { DeckList0 = value; }
    }

    public List<CardTypeSO> DeckList1
    {
        get { return DeckList1; }
        set { DeckList1 = value; }
    }

    public List<CardTypeSO> DeckList2
    {
        get { return DeckList2; }
        set { DeckList2 = value; }
    }


    // Ajoutez d'autres méthodes de gestion de données ici
}