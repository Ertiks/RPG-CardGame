using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Monster")]
public class MonsterTypeSO : ScriptableObject
{
    public Transform prefab;
    public Animator animator;

    public Sprite idle;
    public Sprite dead;
}
