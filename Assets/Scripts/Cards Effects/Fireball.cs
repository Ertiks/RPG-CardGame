using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{

    public override void Start()
    {
        spellName = "Fireball";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();
        spellEffect.Fireball(transformUnitSelected.GetComponent<HealthSystem>());
    }

}
