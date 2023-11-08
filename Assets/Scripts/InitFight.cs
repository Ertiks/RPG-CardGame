using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFight : MonoBehaviour
{

    //Script gerant l'ordre d'initialisation des differents scripts manager.


    private PartyManager partyManager;
    private CardManager cardManager;
    private BattleManager battleManager;


    //Liste d'appoint qui gere l'equipe.
    [SerializeField] private List<CaracterTypeSO> listUnit = new List<CaracterTypeSO>();
    [SerializeField] private Transform startPositionHeroes;

    [SerializeField] private List<MonsterTypeSO> listMonstersFront = new List<MonsterTypeSO>(); //Liste des monstres a faire spawn
    [SerializeField] private List<MonsterTypeSO> listMonstersBack = new List<MonsterTypeSO>(); 
    [SerializeField] private Transform startPositionEnemy;

    [SerializeField] private float offsetx, offsety;
    private int i;
    private int IDunit;
    private float j;

    private const float offsetHeroes = 2.2f;

    private Transform positionStockage;

    private void Awake()
    {
        partyManager = PartyManager.Instance;
        cardManager = partyManager.GetComponent<CardManager>();
        battleManager = partyManager.GetComponent<BattleManager>();
    }

    private void Start()
    {
        InitEnnemies();
        InitHeroes();

        battleManager.InitBattle();
    }


    public void InitHeroes()
    {
        i = 0;

        foreach(CaracterTypeSO caracter in listUnit)
        {
            //On Instancie :
            Transform instance = Instantiate(caracter.unit.transform, startPositionHeroes);
            instance.localPosition = Vector2.zero;
            instance.position = new Vector2(instance.position.x, instance.position.y + (i * offsetHeroes));
            
            IDunit = partyManager.AddUnit(instance.GetComponent<Unit>(), true);



            //Decks :
            cardManager.InitiateHeroDecklist(DeckManager.Instance.DeckLists[i], i);

            i++;
        }

        cardManager.FinishInitDeck();
    }

     //SPAWN UNIT :
    public void InitEnnemies()
    {
        i = 0;
        j = offsety;

        //Instancie Frontlane, puis Backlane.

        //FrontLane
        foreach(MonsterTypeSO monster in listMonstersFront)
        {

            Transform instance = Instantiate(monster.prefab, startPositionEnemy);
            instance.localPosition = Vector2.zero;
            instance.position = new Vector2(instance.position.x, instance.position.y - (i*j));


            partyManager.AddUnit(instance.GetComponent<Unit>(), false, 0);

            i++;
        }

        i = 0;

        //BackLane :
        foreach(MonsterTypeSO monster in listMonstersBack)
        {
            Transform instance = Instantiate(monster.prefab, startPositionEnemy);
            instance.localPosition = Vector2.zero;
            instance.position = new Vector2(instance.position.x + offsetx, instance.position.y - (i * j) - (j/2));


            partyManager.AddUnit(instance.GetComponent<Unit>(), false, 1);

            i++;
        }
    }

    
}
