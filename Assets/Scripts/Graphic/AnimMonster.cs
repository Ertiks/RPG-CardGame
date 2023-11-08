using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMonster : UnitAnimation
{
    //CLASSE D'AFFICHAGE POUR LES ENNEMIS SEULEMENT.
    //HERITE DE "UNITANIMATION" QUI CONTIENT LES FONCTIONS UTILES A TOUTES LES UNITS.

        
    private SpriteRenderer spriteRenderer;

    [SerializeField] private MonsterTypeSO monsterTypeSO;


    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Pour recuperer les sprites du SO
        base.Start();
    }

    public void ToIdle()
    {
        spriteRenderer.sprite = monsterTypeSO.idle;
    }

    public override void Die()
    {
        spriteRenderer.sprite = monsterTypeSO.dead;
    }

    public override void Revive()
    {
        spriteRenderer.sprite = monsterTypeSO.idle;
    }
}
