using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    //GESTION DES RESSOURCES, MANA.

    static private int mana;
    [SerializeField] InterfaceText interf;

    private void Awake()
    {
        mana = 0;
    }

    private void Start()
    {
        interf.UpdateMana(mana);
    }




    
    public bool CanUseMana(int amount) //Dis si on a assez de mana
    {
        if (amount <= mana)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int ReturnMana() //Renvoie le mana actuel
    {
        return mana;
    }

    public void UseMana(int amount) //Depense du mana
    {
        mana -= amount;

        if (mana < 0) //Pas normal d'avoir un mana negatif
        {
            InfoPopup.Create(Vector3.zero, "BUG\nMana inferieur a 0.");
        }

        interf.UpdateMana(mana);
    }

    public void AddMana(int amount) //Ajoute du mana
    {
        mana += amount;

        interf.UpdateMana(mana);
    }

    public void ClearMana()
    {
        mana = 0;
    }

    public void SetMana(int amount)
    {
        mana = amount;
        interf.UpdateMana(amount);
    }

    
}
