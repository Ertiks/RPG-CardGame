using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterDohri : Unit
{
    //IA D'ATK DE DOHRI.
    private int i;
    private int playerCount;

    public override void IAMonster(HealthSystem HealthSystemCible)
    {
        StartCoroutine(CoroutineAttack(HealthSystemCible));

    }

    IEnumerator CoroutineAttack(HealthSystem HealthSystemCible) 
    {
        timerCoroutine = 1f;

        animeUnit.AttackAnim();
        yield return new WaitForSeconds(timerCoroutine);

        //IA : CHOIX DE L'UNITE A FRAPPER (DOHRI = RANDOM)
        List<Unit> listPlayerAlive = partyManager.GetListUnitAlive(false);
        playerCount = listPlayerAlive.Count;
        i = Random.Range(0, playerCount);
        listPlayerAlive[i].healthSystem.Damage(15);

    }





}
