using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    //SCRIPT QUI GERE : 
    //- LE SELECTEUR
    //- CONNAIT L'ID DE L'UNITE POUR LE PARTY MANAGER !
    //- PERMET D'UPDATE LE PARTY MANAGER EN LUI DISANT CE QU'IL SE PASSE SUR CETTE UNITE

    //- LA COROUTINE D'IA DES MONSTRES



    private Transform selector;
    private Transform selectorAlpha;
    private Transform selectorCard; //Selecteur qui s'affiche quand on survole les cartes, pour montrer qui est le héros qui l'utilise.
    protected PartyManager partyManager;

    public HealthSystem healthSystem;
    public StatusSystem statusSystem;

    protected UnitAnimation animeUnit; //Graphique
    protected float timerCoroutine;

    private bool isActive;

    protected int selectorNumber;
    private int IDunit; //Pas d'utilite, mais au cas ou l'ordre d'ID change.

    [SerializeField] private bool isPlayer;




    private void Awake()
    {
        

        selector = transform.Find("Selector");
        selector.gameObject.SetActive(false);
        isActive = false;

        selectorAlpha = transform.Find("SelectorAlpha");
        selectorAlpha.gameObject.SetActive(false);

        selectorCard = transform.Find("SelectorCard");
        selectorCard.gameObject.SetActive(false);




        partyManager = PartyManager.Instance;

        healthSystem = transform.Find("Unit").GetComponent<HealthSystem>();
        animeUnit = healthSystem.GetComponent<UnitAnimation>();

    }

    public void Start()
    {
        healthSystem.onDamageEvent += HealthSystem_onDamageEvent; //event quand prend degats
        healthSystem.onDiedEvent += HealthSystem_onDiedEvent;
    }

    //EVENTS
    private void HealthSystem_onDiedEvent(object sender, System.EventArgs e)
    {
        print("L'unit num" + selectorNumber.ToString() + " est morte");
        partyManager.SetStateUnit(selectorNumber, false, isPlayer);

        animeUnit.Die();
    }

    private void HealthSystem_onDamageEvent(object sender, System.EventArgs e)
    {
        //print("L'unit num" + selectorNumber.ToString() + " a pris des degats.");

    }

    //Discussion avec le party Manager :
    public void ReviveUnit()
    {
        partyManager.SetStateUnit(IDunit, true, isPlayer);

        animeUnit.Revive();
    }

    //INITIALISATION : 
    public void SetNumber(int i) //ID du selector
    {
        selectorNumber = i;
    }

    public void SetID(int i)
    {
        IDunit = i;
    }

    public int GetIT()
    {
        return IDunit;
    }

    //UNIT :
    public bool IsPlayer()
    {
        return isPlayer;
    }

    //EVENT SOURIS :
    private void OnMouseEnter() //Affiche le selecteur transparent quand la souris passe dessus
    {
        partyManager.SetUnitSelected(selectorNumber, isPlayer);
    }

    private void OnMouseExit() //Cache le selecteur transparent quand la souris passe dessus
    {
        partyManager.SetUnitSelected(-1, isPlayer);
        //-1 = aucune unite selectionne
    }

    private void OnMouseDown()
    {
        //selectorManager.SetUnitSelected(selectorNumber);
    }


    //AFFICHAGE : 
    //SELECTEUR SOURIS :
    public void SetActive(bool x) //Active ou desactive le selecteur
    {
        selector.gameObject.SetActive(x);
        isActive = x;
    }

    public void ToggleSelector() //Desactive si actif et viceversa (le selecteur)
    {
        if (isActive)
        {
            selector.gameObject.SetActive(false);
            isActive = false;
        }
        else
        {
            selector.gameObject.SetActive(true);
            isActive = true;
        }
    }

    public void ShowSelectorCard(bool show)
    {
        selectorCard.gameObject.SetActive(show);
    }

    public bool IsActive()
    {
        return isActive;
    }

    //SELECTEUR CARTE :


    //IA MONSTRE, A VOIR DANS LA CLASS
    public virtual void IAMonster(HealthSystem healthSystem)
    {

        print("Aucun monstre selectionne.");

    }

    public float GetTimerCoroutine()
    {
        return timerCoroutine;
    }
}
