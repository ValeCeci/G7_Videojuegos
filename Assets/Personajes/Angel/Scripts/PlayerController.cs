using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Daño")]
    public int Danio = 1;
    public float SegundosInvulnerabilidad = 0.5f;
    public float Rebote = 10f;
    public float CooldownAtaque = 1.5f;
    public GameObject ZonaDanio;
    [SerializeField] private int Vida = 5;

    private Animator animator;
    private bool PuedeRecibirDanio = true;
    private Rigidbody2D rb;
    private MovimientoJugador movimiento;
    private bool estaAtacando = false;
    private bool estaMuerto = false;

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
        var entradaAtaque = Input.GetMouseButtonDown(0);
        if (entradaAtaque && !estaMuerto) StartCoroutine(Atacar());
    }

    public void RecibirDanio(int danio, Vector2 direccion)
    {
        if (PuedeRecibirDanio && !estaMuerto)
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
        estaMuerto = true;
        StopAllCoroutines();
        ZonaDanio.GetComponent<BoxCollider2D>().enabled = false;

        // Sin control y sin desplazamiento horizontal; la gravedad lo deja caer si muere en el aire
        movimiento.enabled = false;
        rb.velocity = new Vector2(0f, Mathf.Min(rb.velocity.y, 0f));
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        animator.SetTrigger(HashMuerte);
    }

    private IEnumerator Atacar()
    {
        if (!estaAtacando)
        {
            estaAtacando = true;
            animator.SetTrigger(HashAtacar);
            yield return new WaitForSecondsRealtime(CooldownAtaque);
            estaAtacando = false;
        }
    }

    // Los Animation Events no ejecutan corrutinas; deben llamar a un metodo void
    public void ActivarZonaDanio()
    {

        StartCoroutine(ActivarZonaDanioRutina());
    }

    private IEnumerator ActivarZonaDanioRutina()
    {
        var zona = ZonaDanio.GetComponent<BoxCollider2D>();
        zona.enabled = true;
        yield return new WaitForSecondsRealtime(0.05f);
        zona.enabled = false;
    }

    public void BloquearMovimiento()
    {
        movimiento.MovimientoBloqueado = true;
    }

    public void DesbloquearMovimiento()
    {
        if (estaMuerto) return;
        movimiento.MovimientoBloqueado = false;
    }

    private IEnumerator invulnerabilidad()
    {
        PuedeRecibirDanio = false;
        yield return new WaitForSecondsRealtime(SegundosInvulnerabilidad);
        PuedeRecibirDanio = true;
    }
}
