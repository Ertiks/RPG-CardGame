using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : Spell
{
    public override void Start()
    {
        spellName = "Revive";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();

        spellEffect.Revive(unitSelected);
    }

}
