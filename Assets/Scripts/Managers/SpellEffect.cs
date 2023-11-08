using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    [SerializeField] private HealthSystem healthPlayer;
    [SerializeField] private HealthSystem healthEnemy;

    [SerializeField] private CardManager cardManager;



    //Fonctions en vrac pour appliquer les effets de carte.
    //Les fonctions "nommées" de sorts sont appelles par des overrides de classes héritants de "spell" (fireball, divination, etc)

    //N'a pas a avoir le party manager. C'est spell qui donne les bons arguments en lançant la fonction.


    private void Awake()
    {
        cardManager = GetComponent<CardManager>();
    }


    public void Fireball(HealthSystem healthSystemSelected, int originID = -1) 
    {
        healthSystemSelected.Damage(40, originID);
    }




    public void Zap(StatusSystem statusSystemSelected)
    {
        statusSystemSelected.AddStatus(new ZapStatus(1, false));
    }

    public void Healing(HealthSystem healthSystemSelected)
    {
        healthSystemSelected.Heal(10);
    }




    //BOLT :
    public void Bolt(List<Unit> listUnit)
    {
        StartCoroutine(BoltCoroutine(listUnit));
    }

    //Obligé de coroutine, l'objet "bolt" est détruit des qu'il est joué, stoppant la coroutine si c'est de lui qu'elle provient.
    //Coroutine de BOLT :
    public IEnumerator BoltCoroutine(List<Unit> listUnit)
    {
        int nbUnit = listUnit.Count;
        Unit targetedUnit;
        for (int i = 0; i < 4; i++)
        {
            targetedUnit = listUnit[Random.Range(0, nbUnit)];
            targetedUnit.healthSystem.Damage(15);

            yield return new WaitForSeconds(0.4f);

        }

    }




    public void Divination()
    {
        cardManager.DrawCard(2);
    }

    public void BlessedSoil(List<Unit> listUnit)
    {
        foreach( Unit unit in listUnit)
        {
            unit.healthSystem.Heal(30);
        }
    }

    public void Revive(Unit unit)
    {
        if(unit.healthSystem.IsAlive() == false)
        {
            unit.ReviveUnit();
        }
        unit.healthSystem.Heal(50);
    }

}
