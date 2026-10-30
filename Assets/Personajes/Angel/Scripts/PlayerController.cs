using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Daño")]
    public float SegundosInvulnerabilidad = 0.5f;
    public float Rebote = 10f;
    [SerializeField] private int Vida = 5;

    private Animator animator;
    private bool PuedeRecibirDanio = true;
    private Rigidbody2D rb;
    private MovimientoJugador movimiento;

    private static readonly int HashAnimRecibirDanio = Animator.StringToHash("recibioDanio");
    private static readonly int HashAtacar = Animator.StringToHash("atacar");
    private static readonly int HashMuerte = Animator.StringToHash("muerte");

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movimiento = GetComponent<MovimientoJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void recibirDanio(int danio, Vector2 direccion)
    {
        if (PuedeRecibirDanio)
        {
            Vida -= danio;
            if(Vida <= 0)
            {
                Morir();
                return;
            }
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.velocity = Vector2.zero;
            rb.AddForce(rebote * Rebote, ForceMode2D.Impulse);
            animator.SetTrigger(HashAnimRecibirDanio);
            StartCoroutine(invulnerabilidad());
        }
    }

    private void Morir()
    {
        rb.velocity = Vector2.zero;
        BloquearMovimiento();
        animator.SetTrigger(HashMuerte);
        rb.bodyType = RigidbodyType2D.Static;
        
    }

    public void BloquearMovimiento()
    {
        movimiento.MovimientoBloqueado = true;
    }

    public void DesbloquearMovimiento()
    {
        movimiento.MovimientoBloqueado = false;
    }

    private IEnumerator invulnerabilidad()
    {
        PuedeRecibirDanio = false;
        yield return new WaitForSecondsRealtime(SegundosInvulnerabilidad);
        PuedeRecibirDanio = true;
    }
}
