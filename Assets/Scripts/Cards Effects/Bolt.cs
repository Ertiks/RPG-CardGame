using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Spell
{

    public override void Start()
    {
        spellName = "Bolt";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();

        spellEffect.Bolt(partyManager.GetListUnitAlive(true));
    }

}
