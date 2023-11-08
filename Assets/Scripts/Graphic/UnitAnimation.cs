using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    public enum DamageType
    {
        Typeless,
        Fire
    }

    //Script principale de gestion d'animations des unites.
    protected Animator animatorDamage;


    public virtual void Start()
    {
        animatorDamage = transform.Find("pfDamageAnimation").GetComponent<Animator>(); //l'Animator pour l'objet qui s'occupe des degats subis
    }

    public void AttackAnim()
    {
        GetComponent<Animator>().Play("Attack");
    }

    public void TakeDamage(DamageType damageType = DamageType.Typeless)
    {
        if (damageType == DamageType.Fire)
        {
            animatorDamage.Play("TypelessDamage");
        }
        else
        {
            animatorDamage.Play("TypelessDamage");
        }


    }

    public void Heal()
    {

        animatorDamage.Play("Healing");

    }

    //A CHANGER :
    public virtual void Die()
    {
        print("Erreur Die Anim");
    }

    public virtual void Revive()
    {
        print("Erreur Revive Unit");
    }
}
