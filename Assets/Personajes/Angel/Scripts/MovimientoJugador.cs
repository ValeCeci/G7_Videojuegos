using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float entradaHorizontal;
    public float velocidadMovimiento;
    private Animator animator;
    private static readonly int EstaCaminandoHash = Animator.StringToHash("caminando");
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        entradaHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool(EstaCaminandoHash, entradaHorizontal != 0);
    }

    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();
    }

    private void ControlarMovimientoHorizontal()
    {
        rb2D.velocity = new Vector2(entradaHorizontal * velocidadMovimiento, rb2D.velocity.y);

        if ((entradaHorizontal > 0 && !MirandoALaDerecha()) || (entradaHorizontal < 0 && MirandoALaDerecha()))
        {
            Girar();
        }
    }

    private void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private bool MirandoALaDerecha()
    {
        return transform.localScale.x == 1;
    }
}
