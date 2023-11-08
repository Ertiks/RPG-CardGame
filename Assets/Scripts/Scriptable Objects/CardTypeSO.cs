using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card")]
public class CardTypeSO : ScriptableObject
{
    public Transform prefab;
    public Transform InstanceIG;

    //Infos Cartes :
    public int manaCost;
    public int classe; //1 mage, 2 pretre, 3 guerrier
}
