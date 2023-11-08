using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zap : Spell
{

    public override void Start()
    {
        spellName = "Zap";
        base.Start();
    }

    protected override void CastSpell()
    {
        base.CastSpell();

        spellEffect.Zap(transformUnitSelected.GetComponent<StatusSystem>());
    }

}
