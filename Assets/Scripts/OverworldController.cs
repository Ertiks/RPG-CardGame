using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldController : MonoBehaviour
{



    [SerializeField] private Rigidbody2D rb;


    [SerializeField] private float vitesse;
    [SerializeField] private float dashAmount;
    [SerializeField] private float dashSize;
    private float horizontalMovement, verticalMovement;

    private Vector3 moveDir;//vecteur direction de déplacement

    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        moveDir = new Vector3(horizontalMovement, verticalMovement).normalized;
    }

    private void FixedUpdate()
    {
        MovePlayer();   
    }


    private void MovePlayer()
    {
        rb.velocity = moveDir * vitesse;
    }
}
