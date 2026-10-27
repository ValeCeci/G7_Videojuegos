using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento;
    public float velocidadCorrer;
    public float fuerzaSalto;
    public float margenCambioDireccion = 0.1f;


    [Header("Detección de suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    private Rigidbody2D rb2D;
    private Animator animator;
    private float entradaHorizontal;
    private bool quiereSaltar;
    private bool enSuelo;
    private bool corriendo;
    private float tiempoSinEntrada;

    private static readonly int EstaCaminandoHash = Animator.StringToHash("caminando");
    private static readonly int EnSueloHash = Animator.StringToHash("enSuelo");
    private static readonly int VelocidadYHash = Animator.StringToHash("velocidadY");
    private static readonly int CorriendoHash = Animator.StringToHash("corriendo");

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        entradaHorizontal = Input.GetAxisRaw("Horizontal");
        corriendo = entradaHorizontal != 0 && Input.GetKey(KeyCode.LeftShift);

        // Al cambiar de direccion el eje pasa por 0 unos frames; se aplica un margen para que el Animator no vaya a Idle
        tiempoSinEntrada = entradaHorizontal != 0 ? 0f : tiempoSinEntrada + Time.deltaTime;
        bool hayEntrada = tiempoSinEntrada < margenCambioDireccion;

        animator.SetBool(EstaCaminandoHash, hayEntrada);
        animator.SetBool(CorriendoHash, hayEntrada && Input.GetKey(KeyCode.LeftShift));
        animator.SetBool(EnSueloHash, enSuelo);

        if (Input.GetButtonDown("Jump"))
            quiereSaltar = true;
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapCircle(puntoSuelo.position, radioSuelo, capaSuelo);
        animator.SetFloat(VelocidadYHash, rb2D.velocity.y);

        ControlarMovimientoHorizontal();
        ControlarSalto();
    }

    private void ControlarMovimientoHorizontal()
    {
        float velocidadActual = corriendo ? velocidadCorrer : velocidadMovimiento;
        rb2D.velocity = new Vector2(entradaHorizontal * velocidadActual, rb2D.velocity.y);

        if ((entradaHorizontal > 0 && !MirandoALaDerecha()) || (entradaHorizontal < 0 && MirandoALaDerecha()))
            Girar();
    }

    private void ControlarSalto()
    {
        if (quiereSaltar && enSuelo)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, fuerzaSalto);
        }
        quiereSaltar = false;
    }

    private void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private bool MirandoALaDerecha()
    {
        return transform.localScale.x > 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoSuelo != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoSuelo.position, radioSuelo);
        }
    }
}
