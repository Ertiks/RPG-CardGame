using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHero : UnitAnimation
{
    //CLASSE D'AFFICHAGE POUR LES ALLIES SEULEMENT.
    //HERITE DE "UNITANIMATION" QUI CONTIENT LES FONCTIONS UTILES A TOUTES LES UNITS.


    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite spriteIdle;
    [SerializeField] Sprite spriteDeath;

    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Pour recuperer les sprites du SO
        base.Start();
    }

    public void ToIdle()
    {
        spriteRenderer.sprite = spriteIdle;
    }

    public override void Die()
    {
        spriteRenderer.sprite = spriteDeath;
    }

    public override void Revive()
    {
        spriteRenderer.sprite = spriteIdle;
    }
}
