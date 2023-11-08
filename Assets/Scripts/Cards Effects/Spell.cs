using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spell : MonoBehaviour
{
    private int ID;
    private int IDunit;


    //INFO A CONNAITRE :
    public string spellName;
    protected int manaCost;

    //SELECTOR
    private Transform spriteSelector;

    //MANAGERS
    protected CardManager cardManager;
    protected BattleManager battleManager;
    protected PartyManager partyManager;
    protected SpellEffect spellEffect;
    protected ResourcesManager resourcesManager;


    //UNITS ET HEALTHSYSTEM
    protected HealthSystem healthSystemSelected;
    protected Transform transformUnitSelected;
    protected Unit unitSelected;


    //PARAMETERS :
    [SerializeField] protected bool needTarget;
    [SerializeField] protected bool needAlive;
    [SerializeField] protected bool needDead;

    //AFFICHAGE :
    TextMeshPro manaCostText;



    public void SetManaCost(int i) //Donne le coup en mana
    {
        manaCost = i;
    }

    public void SetID(int i) //Fonction pour set l'ID, necessaire pour reconnaitre la carte dans CardManager
    {
        ID = i;
    }

    public int GetID() //Renvoie l'ID de la carte
    {
        return(ID);
    }

    public void SetUnitID(int i) //ID unité associé
    {
        IDunit = i;
    }

    public int GetUnitID()
    {
        return IDunit;
    }
    

    private void Awake()
    {

        //On recupere tout les managers :
        cardManager = GameObject.FindWithTag("Manager").GetComponent<CardManager>();
        battleManager = cardManager.GetComponent<BattleManager>();
        spellEffect = cardManager.GetComponent<SpellEffect>();
        partyManager = cardManager.GetComponent<PartyManager>();
        resourcesManager = cardManager.GetComponent<ResourcesManager>();


        //On recupere le selector visuel :
        spriteSelector = transform.Find("CardSelector");
        ShowSelector(false);

        //boite texte pour manacost :
        manaCostText = transform.Find("ManaCostText").GetComponent<TextMeshPro>();
    }

    public virtual void Start() 
    {
        partyManager.OnUnitSelected += SelectorManager_OnEnemySelected; //Event de selection du SelectorManager

        needTarget = true;


    }

    private void SelectorManager_OnEnemySelected(object sender, System.EventArgs e)
    {
        transformUnitSelected = partyManager.GetEnemySelected(); //Quand une nouvelle unite est selectionne on met a jour ici
        unitSelected = partyManager.GetUnitSelected();
    }

    protected virtual void OnMouseDown()
    {
        if(battleManager.turnState == BattleManager.State.Menu) 
        {
            InfoPopup.Create(Vector3.zero, "Partie Terminée");   
        }
        else if (battleManager.turnState == BattleManager.State.PlayerTurn) 
        {
            ShowSelector(true);

            partyManager.UPL[IDunit].ShowSelectorCard(true);

        }
        
    }

    protected virtual void OnMouseUp()
    {

        if (CanCastSpell())
        {
            CastSpell(); //Lance l'effet du sort
            cardManager.PlayCard(ID); //Envoie la carte au cimetiere

            //MANA :
            resourcesManager.UseMana(manaCost);
        }

        ShowSelector(false);
        partyManager.UPL[IDunit].ShowSelectorCard(false);
    }

    protected virtual void CastSpell() //Fonction a override pour appeller le spell associe dans SpellEffect
    {
        EventManager.Instance.EventOnPlayedCard();
    }

    //TEST SPELL :
    protected virtual bool CanCastSpell()
    {
        if(transformUnitSelected == null)
        {
            return false;
        }else if(battleManager.turnState != BattleManager.State.PlayerTurn)
        {
            InfoPopup.Create(Vector3.zero, "Partie terminée.");
            return false;
        } else if(unitSelected.healthSystem.IsAlive() == false && needAlive == true)
        {
            InfoPopup.Create(Vector3.zero, "La cible est morte.");
            return false;
        }else if(unitSelected.healthSystem.IsAlive() == true && needDead == true)
        {
            InfoPopup.Create(Vector3.zero, "La cible est vivante.");
            return false;
        }
        else if(partyManager.GetStateUnit(IDunit, true) == false)
        {
            InfoPopup.Create(Vector3.zero, "Le héros est mort.");
            return false;
        }else if(resourcesManager.CanUseMana(manaCost) == false)
        {
            InfoPopup.Create(Vector3.zero, "Pas assez de Mana.");
            return false;
        }

        return true;
    }

    

    //AFFICHAGE :
    private void ShowSelector(bool x)
    {
        spriteSelector.gameObject.SetActive(x);
    }

    public void UpdateManaCost()
    {
        manaCostText.SetText(manaCost.ToString());
    }


    //VERSION LEGACY DES TESTS POUR LANCER UNE CARTE
    /*if(transformUnitSelected != null) // LEGACY :
        {
            if (battleManager.turnState == BattleManager.State.Menu) 
            {
                InfoPopup.Create(Vector3.zero, "Partie terminée");
            }
            else if (battleManager.turnState == BattleManager.State.PlayerTurn) 
            {

                if ( unitSelected.healthSystem.isAlive() || partyManager.GetStateUnit(IDunit, true) == true)
                {

                    if (resourcesManager.CanUseMana(manaCost))
                    {
                        CastSpell(); //Lance l'effet du sort
                        cardManager.PlayCard(ID); //Envoie la carte au cimetiere

                        //MANA :
                        resourcesManager.UseMana(manaCost);

                        print(partyManager.GetStateUnit(IDunit, true));

                        CanCastSpell();
                    }
                   
                }
                else
                {
                    if (!unitSelected.healthSystem.isAlive())
                    {
                        InfoPopup.Create(Vector3.zero, "Cible invalide");
                    }
                    else if(!resourcesManager.CanUseMana(manaCost)){
                        InfoPopup.Create(Vector3.zero, "Mana insuffisant");
                    }
                    else
                    {
                        InfoPopup.Create(Vector3.zero, "Erreur générique");
                    } if(partyManager.GetStateUnit(IDunit, true) == false)
                    {
                        InfoPopup.Create(Vector3.zero, "L'Unite est morte.");
                    }
                    
                }

            }

        }*/
}
