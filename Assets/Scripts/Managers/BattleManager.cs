using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleManager : MonoBehaviour
{


    //STATE :
    public enum State
    {
        PlayerTurn,
        EnnemyTurn,
        Menu

    }
    public State turnState;

    //DIVERS :
    private CardManager cardManager;
    private PartyManager partyManager;
    private ResourcesManager resourcesManager;
    [SerializeField] private HealthSystem healthPlayer; //A virer a terme

    [SerializeField] private int numberEnemy;
    [SerializeField] private int numberPlayer;

    //STATIC :  
    public static List<bool> enemyParty = new List<bool>();
    public static List<bool> playerParty = new List<bool>();

    //UI :
    [SerializeField] private Button endTurnButton;
    [SerializeField] private Transform endgame;
    [SerializeField] private Transform victoryMessage;
    [SerializeField] private Transform defeatMessage;


    private void Awake()
    {
        cardManager = GetComponent<CardManager>();
        partyManager = GetComponent<PartyManager>();
        resourcesManager = GetComponent<ResourcesManager>();

        defeatMessage.gameObject.SetActive(false);
        victoryMessage.gameObject.SetActive(false);
        endgame.gameObject.SetActive(false);

    }

    private void Start()
    {


        //Event du bouton "fin de tour"
        endTurnButton.onClick.AddListener(() =>
        {
            if(turnState == BattleManager.State.PlayerTurn)
            {
                ToEnnemyTurn();
            }

        });





        EventManager.Instance.onFightEnd += OnFightEnd;
    }

    public void InitBattle()
    {
        ToPlayerTurn();
    }

    private void OnFightEnd(object sender, System.EventArgs e) //FIN DE COMBAT
    {
        turnState = State.Menu;
        bool victory = partyManager.GetVictory();
        if (victory)
        {
            print("GGGGG POUR TA VICTOIRE BROOOOOOOOOOOOOOOOOOO CLIQUE ICI POUR TON IPHONE >>>>>>>>__ ICI __<<<<<<<<");
        }
        else
        {
            print("bravo t'as perdu (sale merde)");
        }
    }

    public void ToEnnemyTurn() //Passer au tour de l'ennemi
    {

        EventManager.Instance.EventOnEndTurn();
        turnState = State.EnnemyTurn;

        //On defausse la main :
        cardManager.ShuffleHandInGraveyard();

        //IA DU MONSTRE .....

        partyManager.StartAttackEnnemy();
        //StartCoroutine(IAMonster());
    }

    public void ToPlayerTurn() //Passer au tour du joueur
    {
        turnState = State.PlayerTurn;

        resourcesManager.SetMana(5);
        cardManager.DrawCard(5);
    }

    public void EndFight(bool boolVictory)
    {
        turnState = BattleManager.State.Menu;
        endgame.gameObject.SetActive(true); 

        if (boolVictory)
        {
            victoryMessage.gameObject.SetActive(true);
        }
        else
        {
            defeatMessage.gameObject.SetActive(true);
        }
    }


    IEnumerator IAMonster() //Coroutine pour faire une "IA" pas a pas
    {
        yield return new WaitForSeconds(1);
        print("L'ennemi inflige 30 degats");
        healthPlayer.Damage(30);

        yield return new WaitForSeconds(1);
        ToPlayerTurn();
    }

    //GESTIONNAIRE DE TOURS DE JEU :
    
    

}
