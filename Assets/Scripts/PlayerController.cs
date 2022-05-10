using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 20f;
    public LayerMask groundLayer;

    [SerializeField] private float speed = 100f;

    private float horizontalInput;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        rigidbody.AddForce(Vector3.right * (speed * horizontalInput));
        IsOnTheGround();
        if (Input.GetKeyDown(KeyCode.Space) && IsOnTheGround())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsOnTheGround()
    {
        //configurando el rayo
        float yOffset = 0.2f; //ese poquito más que nos deja saber que aun en una cuesta detecte que estamos en el suelo
        Vector3 origin = transform.position;
        SphereCollider playerCollider = GetComponent<SphereCollider>();

        Physics.Raycast(origin, Vector3.down, out RaycastHit hit, playerCollider.radius + yOffset, groundLayer); //out RaycastHit hit, dice que guardemos una variable RaycastHit que llamaremos Hit
        //GroundLayer es la capa donde si podrá interactuar el raycast
        
        //el dibujo del rayo
        Color raycastColor = hit.collider != null ? Color.green : Color.magenta; //en el suelo es verde el rayo pero en el aire es magenta ifelse de una sola linea, si collider de hit no es nulo es verde, si no es magenta
        //primero hemos dicho lo que queremos modificar y luego los valores

        Debug.DrawRay(origin, Vector3.down * (playerCollider.radius + yOffset), raycastColor, 0, false);
        return hit.collider != null;
    }
}
