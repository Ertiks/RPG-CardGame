using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapStatus : Status
{
    private List<Unit> unitList;
    private PartyManager PM;


    public ZapStatus(int _remainingTurns, bool isStackable)
    {
        this.remainingTurns = _remainingTurns;
        EventManager.Instance.onPlayedCard += Trigger_OnPlayedCard;

        PM = GameObject.FindWithTag("Manager").GetComponent<PartyManager>();

    }

    public void Trigger_OnPlayedCard(object sender, System.EventArgs e) //Effet du status quand une carte est jouée.
    {

        unitList = PM.GetListUnitAlive(true);

        unitList[Random.Range(0, unitList.Count)].healthSystem.Damage(10);

    }

    public override void RemoveStatus() //Pour se désincrire de l'evenement, sinon ça BUG
    {
        Debug.Log("Status Removed : Zap");
        EventManager.Instance.onPlayedCard -= Trigger_OnPlayedCard;

    }
}
