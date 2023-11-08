using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //SCRIPT D'AFFICHAGE POUR LES ENNEMIS

    //UNIQUEMENT POUR L'AFFICHAGE
    private SpriteRenderer spriteRenderer;
    private Animator animatorDamage;

    [SerializeField] private MonsterTypeSO monsterTypeSO;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Pour recuperer les sprites du SO
        animatorDamage = transform.Find("pfDamageAnimation").GetComponent<Animator>(); //l'Animator pour l'objet qui s'occupe des degats subis
    }

    public void ToIdle()
    {
        spriteRenderer.sprite = monsterTypeSO.idle;
    }

    public void Die()
    {
        spriteRenderer.sprite = monsterTypeSO.dead;
    }
    
    public void AttackAnim()
    {
        GetComponent<Animator>().Play("Attack");
    }

    public void TakeDamage()
    {

        animatorDamage.Play("TypelessDamage");

    }
}
