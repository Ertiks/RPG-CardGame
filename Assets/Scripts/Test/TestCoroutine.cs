using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    
    void Start()
    {
        print("1");

        StartCoroutine(pouetCoroutine());

        print("COROUTINE FINI ?");
    }

    IEnumerator pouetCoroutine()
    {
        yield return new WaitForSeconds(1f);
        print("fin");
    }
}
