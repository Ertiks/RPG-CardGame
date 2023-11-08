using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceText : MonoBehaviour
{
    //SCRIPT GERANT LES DIFFERENTS TEXTES AFFICHES A L'ECRAN

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI manaText;

    [SerializeField] TextMeshProUGUI deckText;
    [SerializeField] TextMeshProUGUI gravText;

    public void UpdateMana(int amount, int maxAmount = 3)
    {
        text.SetText(amount.ToString());
        manaText.SetText("Mana : " + amount.ToString() + "/" + maxAmount.ToString());
    }

    public void UpdateDeckText(int amount)
    {
        deckText.SetText("Deck : \n\n" + amount.ToString() + "Cards");
    }

    public void UpdateGravText(int amount)
    {
        gravText.SetText("Graveyard : \n\n" + amount.ToString() + "Cards");
    }
}
