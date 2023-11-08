using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enfant : TestHeritage1
{
    // Start is called before the first frame update
    public override void Start()
    {
        Enfantprint();
        base.Start();
    }

    protected void Enfantprint()
    {
        print("HIIIIIIIIII");
    }


}
