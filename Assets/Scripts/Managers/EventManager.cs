using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    //Gere les events globaux de la scene.

    private static EventManager instance;

    //Evenements "écoutables" du manager : 
    public event EventHandler onPlayedCard;
    public event EventHandler onEndTurn;
    public event EventHandler onFightEnd;


    //SINGLETON
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<EventManager>();
                    singletonObject.name = "EventManagerSingleton";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }


    
    public void EventOnPlayedCard() //Quand une carte est lancée depuis la main
    {
        onPlayedCard?.Invoke(this, EventArgs.Empty);
    }

    public void EventOnEndTurn() //A la fin du tour du joueur
    {
        onEndTurn?.Invoke(this, EventArgs.Empty);
    }

    public void EventOnFightEnd(bool isPlayerWinner = true) //Quand le combat se termine. [True si joueur gagnant, false si perdant (inutilisé)]
    {
        onFightEnd?.Invoke(this, EventArgs.Empty);
    }
}
