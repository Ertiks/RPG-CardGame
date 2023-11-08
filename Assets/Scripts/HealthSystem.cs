using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using CodeMonkey.Utils;

public class HealthSystem : MonoBehaviour
{

    private StatsSystem stats;
    
    //Event locaux
    public event EventHandler onDamageEvent;
    public event EventHandler onDiedEvent;

    //Dans l'optimal, il faudrait passer l'affichage dans un script a part d'UI.

    [SerializeField] TextMeshPro textHealth;

    [SerializeField] private int health = 100;
    [SerializeField] private int healthMax = 100;

    private bool isPlayer;

    void Start()
    {
        stats = GetComponent<StatsSystem>();
        UpdateHealth();
    }


    public void Heal(int amount) //Soigne l'unite
    {
        if (amount + health > healthMax)
        {
            amount = healthMax - health;
        }

        health += amount;




        UpdateHealth();

        //VISUS :
        DamagePopup.Create(transform.position + new Vector3(1, 0), amount, "3EE000");
        transform.GetComponent<UnitAnimation>().Heal();
    }


    private int CalculateDamage(int damage)
    {
        //Ordre de calcul : (Damage + FlatDamage)*DamageMultiplier

        float actualDamage;

        actualDamage = (damage + stats.DamageTakenFlat) * stats.DamageTakenMultiplier;
        return (int)actualDamage;
    }

    public void Damage(int amount, int origineID = -1, UnitAnimation.DamageType damageType = UnitAnimation.DamageType.Typeless) //Blesse l'unite
    {
        amount = CalculateDamage(amount);

        health -= amount; 
        if (health < 0) { health = 0; } //Verifications pour pas passer en negatif.

        UpdateHealth();

        //VISUS :
        DamagePopup.Create(transform.position + new Vector3(1,0), amount, "CD2400"); //Popup a degats
        transform.GetComponent<UnitAnimation>().TakeDamage();

        onDamageEvent?.Invoke(this, EventArgs.Empty); //Event quand degats subis

        if ( health <= 0)
        {
            onDiedEvent?.Invoke(this, EventArgs.Empty); //Event quand unite morte.
        }
    }

    public void HealFull() //Soigne entierement
    {
        health = healthMax;
    }

    public void SetHealthMax(int amount) //Met a jour les PV max de l'unite
    {
        healthMax = amount;
        UpdateHealth();
    }

    private void UpdateHealth() //Met a jour l'affichage des PV
    {
        textHealth.SetText("HP : " + health.ToString() + "/" + healthMax.ToString());
    }

    public bool IsAlive()
    {
        if (health <= 0)
        {
            return false;
        }
        return true;
    }
}
