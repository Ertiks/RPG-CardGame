using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Spell
{
    public override void Start()
    {
        spellName = "Healing";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();

        spellEffect.Healing(transformUnitSelected.GetComponent<HealthSystem>());
    }

}
