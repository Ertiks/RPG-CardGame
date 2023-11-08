using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    public Sprite icon;
    public string description;
    public int remainingTurns;

    public bool isStackable;

    //TO DO : ajouter un eventhandler pour la fin des events

    //public abstract void Apply(StatusSystem effectContainer, GameObject entityRoot);


    public virtual void RemoveStatus()
    {
        Debug.Log("RemoveStatus non défini.");
    }
}
