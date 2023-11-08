using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divination : Spell
{

    public override void Start()
    {
        spellName = "Divination";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();

        print("Pioche 2 cartes");
        spellEffect.Divination();
    }

}
