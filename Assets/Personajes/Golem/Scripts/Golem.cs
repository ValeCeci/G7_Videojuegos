using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Golem : Enemigo
{

    [Header("Golem")]
    public GameObject Bala;
    [Tooltip("Hijo del Golem desde donde sale la bala. Si está vacío sale del pivote")]
    public Transform PuntoDisparo;

    private static readonly int AtacarHash = Animator.StringToHash("atacar");
    private static readonly int CaminandoHash = Animator.StringToHash("caminando");
    private static readonly int IdleHash = Animator.StringToHash("idle");
    private static readonly int RecibiendoDanioHash = Animator.StringToHash("recibiendoDanio");
    private static readonly int MuerteHash = Animator.StringToHash("muerte");


    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        CambioEstado();
        switch (EstadoActual)
        {
            case EstadosEnemigo.Atacando:
                if(Time.time >= ProximoAtaque)
                {
                    StartCoroutine(Atacar());
                }
                else
                {
                    Animator.SetTrigger(IdleHash);
                }
                break;
            case EstadosEnemigo.Caminando:
                ControlarMovimiento();
                break;
            case EstadosEnemigo.Idle:
                Animator.SetTrigger(IdleHash);
                break;
        }
    }
    protected override IEnumerator Atacar()
    {
        EstaAtacando = true;    
        Animator.SetTrigger(AtacarHash);
        yield return new WaitForSeconds(CooldownAtaque);
        ProximoAtaque = Time.time + CooldownAtaque;
        EstaAtacando = false;
    }
    public void Disparar()
    {
        Vector3 origen = PuntoDisparo != null ? PuntoDisparo.position : transform.position;
        Instantiate(Bala, origen, Quaternion.identity);
    }
    protected override void ControlarMovimiento()
    {
        Animator.SetTrigger(CaminandoHash);
        Vector2 direccion = (Jugador.transform.position - transform.position).normalized;
        float dirX = Mathf.Sign(direccion.x);
        Orientar(dirX);
        Rb.velocity = new Vector2(direccion.x * Velocidad, Rb.velocity.y);
    }
    public override IEnumerator RecibirDanio(int danio)
    {
        if (EstadoActual == EstadosEnemigo.RecibiendoDanio) yield break;
        EstadoActual = EstadosEnemigo.RecibiendoDanio;
        Vida -= danio;
        if(Vida <= 0)
        {
            EstadoActual = EstadosEnemigo.Muerto;
            Morir();
            yield break;
        }
        Animator.SetTrigger(RecibiendoDanioHash);
        yield return new WaitForSeconds(SegundosInvulnerabilidad);
        EstadoActual = EstadosEnemigo.Idle;
    }
    protected override void Morir()
    {
        Animator.SetTrigger(MuerteHash);
        Rb.velocity = Vector2.zero;
        StopAllCoroutines();
        Rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, SegundosInvulnerabilidad);
    }
}
