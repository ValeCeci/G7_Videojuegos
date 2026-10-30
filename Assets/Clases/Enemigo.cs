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

    protected Rigidbody2D Rb;
    protected Animator Animator;
    protected bool EstaCaminando = false;
    protected bool PuedeAtacar = true;
    protected bool EstaRecibiendoDanio = false;

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
    public abstract void RecibirDanio(int danio);
    protected abstract void Morir();

}
