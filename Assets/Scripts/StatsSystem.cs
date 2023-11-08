using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystem : MonoBehaviour
{

    //Stock, update et renvoie les statistiques associées aux unités.

    //Liste des variables
    float damageMultiplier;
    float damageFlat;

    float damageTakenMultiplier;
    float damageTakenFlat;

    //get,set
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public float DamageFlat { get => damageFlat; set => damageFlat = value; }
    public float DamageTakenMultiplier { get => damageTakenMultiplier; set => damageTakenMultiplier = value; }
    public float DamageTakenFlat { get => damageTakenFlat; set => damageTakenFlat = value; }

    private void Start()
    {
        damageMultiplier = 1;
        damageTakenMultiplier = 1;

        DamageTakenFlat = 0;
    }
}
