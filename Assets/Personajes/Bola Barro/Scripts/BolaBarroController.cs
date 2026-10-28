using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaBarroController : MonoBehaviour
{
    [Header("Deteccion")]
    public float RadioDeteccion = 1.5f;

    [Header("Movimiento")]
    public float Velocidad = 2f;
    public Transform jugador;

    private Rigidbody2D rb;
    private Animator animator;

    private bool estaCaminando = false;

    private static readonly int CaminandoHash = Animator.StringToHash("caminando");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ControlarMovimiento()
    {
        Vector3 haciaObjetivo = jugador.position - transform.position;
        Vector3 direccion = haciaObjetivo.normalized;
        if (haciaObjetivo.magnitude <= RadioDeteccion)
        {
            estaCaminando = true;
            rb.velocity = new Vector2(direccion.x * Velocidad, rb.velocity.y);
            if ((direccion.x > 0 && !MiradoDerecha()) || (direccion.x < 0 && MiradoDerecha()))
                Girar();
        }
        else
        {
            estaCaminando= false;
        }
        animator.SetBool(CaminandoHash, estaCaminando);
    }

    private void FixedUpdate()
    {
        ControlarMovimiento();
    }

    private void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1f;
        transform.localScale = escala;
    }

    private bool MiradoDerecha()
    {
        return transform.localScale.x > 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccion);
    }
}
