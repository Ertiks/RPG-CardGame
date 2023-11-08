using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : Spell
{
    public override void Start()
    {
        spellName = "BlessedSoil";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();

        spellEffect.BlessedSoil(partyManager.GetListUnitAlive(false));
    }
}
