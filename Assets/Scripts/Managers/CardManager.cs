using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CardManager : MonoBehaviour
{
    //GERE LES CARTES PIOCHEES, LE DECK ET LE CIMETIERE.


    //card = le scriptableObject
    //cardIG = la class

    //[SerializeField] private BattleManager battleManager; //Inutile pour l'instant ?

    [SerializeField] private Transform startPosition; //A link avec StartPositionCard dans l'inspector d'Unity



    [SerializeField] private Text textDeck;
    [SerializeField] private Text textGrav;

    [SerializeField] private InterfaceText interf;
    private List<CardIngame> deck = new List<CardIngame>();
    [SerializeField] private List<CardIngame> hand = new List<CardIngame>();
    private List<CardIngame> graveyard = new List<CardIngame>();

    private float offset = 1.6F;


    private int IDcount = 1; //a ne pas reset. Yann : probablement a mettre en static


    //Decks :
    [SerializeField] private List<DeckTypeSO> deckList = new List<DeckTypeSO>();

    //----------------------------------------------------------//
    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    void Update()
    {

    }
    //----------------------------------------------------------//

    public void DrawCard(int amount) //Ajoute les X premieres cartes du deck dans la main, et les enleve du deck
    {

        if(deck.Count() < amount)
        {
            ShuffleGravInDeck();
        }

        if(deck.Count() < amount) {
            amount = deck.Count();
        }

        for(int i = 0; i < amount; i++)
        {
            hand.Add(deck[0]);
            InstantiateCard(deck[0]);

            deck.RemoveAt(0);
        }

        ReorganizeHand();

        //UI : 
        interf.UpdateDeckText(deck.Count);
        interf.UpdateGravText(graveyard.Count);
    }


    private void PrintCardList(List<CardIngame> cardList) //Pour track les cartes d'une liste
    {
        foreach (CardIngame cardIG in cardList)
        {
            print(cardIG.cardSO.prefab.GetComponent<Spell>().spellName);
        }
    }

    //Rajoute au deck principal le sous deck en argument. Ancienne version.
    public void InitiateHeroDeck(DeckTypeSO initDeck, int IDunit)
    {
        foreach (CardTypeSO card in initDeck.decklist)
        {
            deck.Add(new CardIngame(card, null, null, IDunit));
        }
    }

    //Rajoute au deck principal le sous deck en argument. Avec une liste de CardTypeSO.
    public void InitiateHeroDecklist(List<CardTypeSO> initDeck, int IDunit)
    {
        foreach (CardTypeSO card in initDeck)
        {
            deck.Add(new CardIngame(card, null, null, IDunit));
        }
    }


    public void FinishInitDeck() //Pour valider le deck
    {
       
        foreach (CardIngame cardIG in deck)
        {
            cardIG.ID = IDcount;
            IDcount++;
        }

        FonctionsUtiles.Fisher_Yates_CardDeck_Shuffle(deck);
    }


    /*private void InitiateDeck() //INIT, Legacy
    {
        foreach(DeckTypeSO deckOrigin in deckList)//Pour tout les decks de chaque unite.
        {
            //Copie la decklist du scriptableObject :
            foreach (CardTypeSO card in deckOrigin.decklist)
            {
                deck.Add(new CardIngame(card, null));
            }    
        }

        //Donne un ID a chaque carte du deck :
        foreach (CardIngame cardIG in deck)
        {
            cardIG.ID = IDcount;
            IDcount++;
        }

        //Shuffle
        FonctionsUtiles.Fisher_Yates_CardDeck_Shuffle(deck);
    }*/


    private void InstantiateCard(CardIngame cardIG) //Instancie une carte
    {
        cardIG.cardInstance = Instantiate(cardIG.cardSO.prefab, startPosition);

        //INFOS A DONNER A LA CARTES 
        Spell spell = cardIG.cardInstance.GetComponent<Spell>();
        spell.SetID(cardIG.ID);
        spell.SetUnitID(cardIG.IDunit);

        spell.SetManaCost(cardIG.manaCost);
        spell.UpdateManaCost();


        IDcount++;
    }


    public void PlayCard(int ID) //Joue une carte avec un ID precis
    {
        CardIngame playedCardIG;

        playedCardIG = hand.FirstOrDefault(s => s.ID == ID); //recupere la carte avec l'ID donne

        Destroy(playedCardIG.cardInstance.gameObject);

        hand.RemoveAll(s => s.ID == ID); //Supprime la carte de la main
        graveyard.Add(playedCardIG);

        ReorganizeHand();


        //UI : 
        interf.UpdateDeckText(deck.Count);
        interf.UpdateGravText(graveyard.Count);
    }


    public void ShuffleGravInDeck() //Melange le cimetiere au deck
    {
        //On passe toutes les cartes du cimetière au deck
        foreach(CardIngame c in graveyard)
        {
            deck.Add(c);
        }
        //On nettoie le cimetière
        graveyard.Clear();

        //On melange les cartes
        deck = FonctionsUtiles.Fisher_Yates_CardDeck_Shuffle(deck);

        //UI : 
        interf.UpdateDeckText(deck.Count);
        interf.UpdateGravText(graveyard.Count);
    }


    public void ShuffleHandInGraveyard() //Defausse la main dans le cimetière
    {
        List<CardIngame> listCardCopy = new List<CardIngame>();

        foreach(CardIngame c in hand)
        {
            graveyard.Add(c);

            Destroy(c.cardInstance.transform.gameObject);
        }


        hand.Clear();
        ReorganizeHand();

        //UI : 
        interf.UpdateDeckText(deck.Count);
        interf.UpdateGravText(graveyard.Count);
    }


    public void ReorganizeHand() //Replace les cartes dans la main
    {
        int i = 0;
        foreach(CardIngame cardIG in hand)
        {
            cardIG.cardInstance.position = new Vector2(startPosition.position.x + (i * offset), startPosition.position.y);
            i++;
        }

    }
}
