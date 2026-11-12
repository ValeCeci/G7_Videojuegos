using System.Collections;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    [Header("Deteccion")]
    public float RadioDeteccion = 2f;
    public float RadioDeteccionAtaque = 1.0f;
    public float DuracionDash = 1.2f;
    public float TiempoPreparacionDash = 0.67f;

    [Header("Movimiento")]
    public float Velocidad = 2f;
    protected Transform Jugador;

    [Header("Ataque")]
    public int Danio = 1;
    public float CooldownAtaque = 2f;
    public int Vida = 3;
    public GameObject ZonaDanio;
    public float SegundosInvulnerabilidad = 0.5f;

    protected Rigidbody2D Rb;
    protected Animator Animator;
    protected float ProximoAtaque = 0f;
    protected bool EstaCaminando = false;
    protected bool PuedeAtacar = true;
    protected bool EstaRecibiendoDanio = false;
    protected bool EstaAtacando = false;
    protected enum EstadosEnemigo {Caminando, Idle, Atacando, RecibiendoDanio, Muerto}
    [SerializeField] protected EstadosEnemigo EstadoActual = EstadosEnemigo.Idle;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            Jugador = obj.transform;
        else
            Debug.LogWarning("No se encontró un objeto con tag Player");
    }

    protected abstract void ControlarMovimiento();
    protected abstract IEnumerator Atacar();
    public abstract IEnumerator RecibirDanio(int danio);
    protected abstract void Morir();

    protected void CambioEstado()
    {
        if (EstadoActual == EstadosEnemigo.RecibiendoDanio || EstadoActual == EstadosEnemigo.Muerto) return;

        float distancia = Vector2.Distance(Jugador.position, transform.position);
        if(distancia <= RadioDeteccionAtaque)
        {
            EstadoActual = EstadosEnemigo.Atacando;
            Rb.velocity = Vector2.zero;
        } else if (distancia <= RadioDeteccion && !EstaAtacando)
        {
            EstadoActual = EstadosEnemigo.Caminando;
        }
        else if (distancia > RadioDeteccion && EstadoActual != EstadosEnemigo.RecibiendoDanio && !EstaAtacando)
        {
            EstadoActual = EstadosEnemigo.Idle;
            Rb.velocity = Vector2.zero;
        }
    }
    protected void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1f;
        transform.localScale = escala;
    }

    protected bool MiradoDerecha()
    {
        return transform.localScale.x > 0f;
    }

    protected void Orientar(float dirX)
    {
        if ((dirX > 0 && !MiradoDerecha()) || (dirX < 0 && MiradoDerecha()))
            Girar();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccionAtaque);
    }

}
