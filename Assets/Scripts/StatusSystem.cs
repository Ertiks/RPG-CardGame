using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSystem : MonoBehaviour
{
    public List<Status> activeStatus = new List<Status> ();

    private GameObject _entityRoot;

    void Start()
    {
        _entityRoot = this.gameObject;

        EventManager.Instance.onEndTurn += UpdateStatus_OnEndTurn;

    }

    public void AddStatus(Status s)
    { 
        if (s.isStackable)
        {
            Debug.Log("Status stackable TODO");
        }
        else
        {
            activeStatus.Add(s);
        }
    
    }

    public void RemoveStatus(Status s) { activeStatus.Remove(s);  }


    private void UpdateStatus_OnEndTurn(object sender, System.EventArgs e)
    {

        //List regroupant les status a remove a la fin du tour, pour éviter de les enlever pendant l'itération sur la meme liste durant le foreach (crash, sinon)
        List<Status> copyListStatus = new List<Status>();


        foreach (Status s in activeStatus)
        {
            s.remainingTurns--;
            if(s.remainingTurns <= 0)
            {
                copyListStatus.Add(s);
            }
        }

        foreach(Status j in copyListStatus)
        {
            j.RemoveStatus(); //Desincrit le status a l'event.
            activeStatus.Remove(j); //Enleve le status de la liste des status actifs.
        }

    }


}
