using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartyManager : MonoBehaviour
{

    //GERE LES EQUIPE, LE NOMBRE D'UNITE, SI LES EQUIPES SONT ENCORE EN VIE, ETC

    public event EventHandler OnUnitSelected;
    public event EventHandler OnEnd; //jesaispacommentlenommer
    BattleManager battleManager;

    public List<Unit> UEL = new List<Unit>(); //Unit Enemy List 
    public List<Unit> UPL = new List<Unit>(); //Unit Player List

    [SerializeField] private List<int> positionEnemyList = new List<int>(); //0 pour frontlane, 1 pour backlane.

    private List<bool> stateEnemyList = new List<bool>();
    private List<bool> statePlayerList = new List<bool>();

    [SerializeField] private int unitSelected;
    [SerializeField] private bool isPlayerSelected;

    private int IDheroes; //A ne pas changer. ID locaux a la scene seulement.
    private int IDenemies;

    private bool victory;


    //Singleton : 
    private static PartyManager instance;
    public static PartyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PartyManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<PartyManager>();
                    singletonObject.name = "PartyManagerSingleton";
                }
            }
            return instance;
        }
    }




    private void Awake()
    {
        IDheroes = 0;
        IDenemies = 0;

        victory = false;

        unitSelected = -1;
        isPlayerSelected = false;

        battleManager = GetComponent<BattleManager>();
    }

    //ID :
    public void SetIDParty() //Legacy
    {
        int i = 0;
        foreach (Unit unit in UEL)
        {
            unit.SetNumber(i); //Donne l'ID a chaque selector ennemi
            stateEnemyList.Add(true);
            i++;
        }
        i = 0;
        foreach (Unit unit in UPL)
        {
            unit.SetNumber(i); //Donne l'ID a chaque selector heros
            statePlayerList.Add(true);
            i++;
        }
    }

    //GESTIONNAIRE DE PARTY :  

    //SET 
    public void SetStateUnit(int ID, bool state, bool isPlayer)
    {
        if (isPlayer)
        {
            statePlayerList[ID] = state;
            print("un allié viens de caner la en fait");
            
            if (TestStateParty(isPlayer) == false)
            {
                victory = false;
                EventManager.Instance.EventOnFightEnd(false);
            }
            
        }
        else
        {
            stateEnemyList[ID] = state;
            print("un ennemi viens de caner la en fait");
            if (TestStateParty(isPlayer) == false)
            {
                victory = true;
                EventManager.Instance.EventOnFightEnd(true);
            }
        }
    }

    public int AddUnit(Unit unit, bool isPlayer, int position = 0) //Ajoute un ennemi au groupe. SetID a chaque fois. //RENVOIE L'ID
    {
        if (isPlayer) { UPL.Add(unit); unit.SetNumber(IDheroes); statePlayerList.Add(true); unit.SetID(IDheroes);
            IDheroes++; return (IDheroes - 1); }

        else {

            UEL.Add(unit);
            positionEnemyList.Add(position);

            unit.SetNumber(IDenemies);
            stateEnemyList.Add(true);
            unit.SetID(IDenemies);



            IDenemies++;

            return (IDenemies - 1); 
        }        
    }

    public void AddUnitWithoutSetID(Unit unit, bool isPlayer) //Sans SET ID, pour l'init.
    {
        if (isPlayer) { UPL.Add(unit); }
        else { UEL.Add(unit); }
    }







    public bool TestStateParty(bool isPlayer) //Test soit l'equipe allie, soit l'equipe ennemi. Renvoie false si tout les membres de l'equipe specifie sont morts.
    {
        if (isPlayer)
        {
            foreach(bool state in statePlayerList)
            {
                if (state) { return true; }
            }
        }
        else
        {
            foreach (bool state in stateEnemyList)
            {
                if (state) { return true; }
            }
        }


        return false;
    }

    //GET 
    public List<Unit> GetListUnitAlive(bool isEnemy)
        //Renvoit list d'unité vivante dans la liste. true -> liste de players, fale -> list d'ennemis.
    {
        List<Unit> listReturn = new List<Unit>();

        if (isEnemy)
        {
            foreach (Unit unit in UEL)
            {
                if (unit.healthSystem.IsAlive())
                {
                    listReturn.Add(unit);
                }
            }
        }
        else
        {
            foreach (Unit unit in UPL)
            {
                if (unit.healthSystem.IsAlive())
                {
                    listReturn.Add(unit);
                }
            }
        }

        return listReturn;
    }

    public bool GetStateUnit(int IDunit, bool isPlayer)
    {
        if (isPlayer)
        {
            return statePlayerList[IDunit];
        }
        else
        {
            return stateEnemyList[IDunit];
        }
    }

    //DIVERS :

    public bool GetVictory()
    {
        return victory;
    }


    //SELECTION : 
    public void SetUnitSelected(int number, bool isPlayer) //Number = numero de l'unite selectionne, dans l'ordre de la liste initialise
    {
        foreach (Unit unit in UEL)
        {
            unit.SetActive(false); //Desactive tout le monde
        }
        foreach (Unit unit in UPL)
        {
            unit.SetActive(false);
        }

        if (number != -1)
        {
            if (isPlayer)
            {
                UPL[number].SetActive(true); //Active celui cible
                unitSelected = number;
            }
            else
            {
                UEL[number].SetActive(true); //Active celui cible
                unitSelected = number;
            }

        }
        else
        {
            unitSelected = -1;
        }

        isPlayerSelected = isPlayer;
        OnUnitSelected?.Invoke(this, EventArgs.Empty); //On crie tres fort qu'une unite vient d'etre selectionne    
    }

    public Transform GetEnemySelected() //Renvoie le transform de l'ennemi selectionne.
    {
        if (unitSelected != -1)
        {
            if (isPlayerSelected)
            {
                return UPL[unitSelected].transform.Find("Unit").GetComponent<Transform>(); ;
            }
            else
            {
                return UEL[unitSelected].transform.Find("Unit").GetComponent<Transform>(); ;
            }
        }
        else return null;
    }

    public Unit GetUnitSelected()
    {
        if (unitSelected != -1)
        {
            if (isPlayerSelected)
            {
                return UPL[unitSelected];
            }
            else
            {
                return UEL[unitSelected];
            }
        }
        else return null;
    }

    public bool IsSelectedEnemyFront()
    {

        if (unitSelected == -1 || isPlayerSelected)
        {
            Debug.Log("La cible n'est pas un ennemi.");
        }
        else
        {
            if (positionEnemyList[unitSelected] == 0)
            {
                Debug.Log("Cible front");
                return true;
            }
            Debug.Log("Cible back");
        }
        return false;
    }




    //TOUR ENNEMI :
    public void StartAttackEnnemy()
    {
        StartCoroutine(FaireAttaquerLesEnnemis());
    }
      
    IEnumerator FaireAttaquerLesEnnemis()
    {
        InfoPopup.Create(Vector3.zero, "Tour Ennemi");

        int i = 0;
        foreach(Unit unit in UEL)
        {
            if (stateEnemyList[i])
            {
                unit.IAMonster(UPL[0].healthSystem);
                yield return new WaitForSeconds(unit.GetTimerCoroutine());
            }


            i += 1;
        }

        battleManager.ToPlayerTurn();

    }

}
