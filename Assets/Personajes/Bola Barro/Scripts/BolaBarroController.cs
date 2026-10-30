using System.Collections;
using UnityEngine;

public class BolaBarroController : Enemigo
{

    private static readonly int CaminandoHash = Animator.StringToHash("caminando");
    private static readonly int PrepararAtaqueHash = Animator.StringToHash("prepararAtaque");
    private static readonly int AtaqueHash = Animator.StringToHash("atacar");
    private static readonly int RecibioDanioHash = Animator.StringToHash("recibioDanio");
    private static readonly int MuerteHash = Animator.StringToHash("muerte");

    public float SegundosHastaDesaparecer = 1.5f;

    // Mientras prepara o ejecuta el dash es invulnerable
    private bool enAtaque;
    private bool estaMuerto;

    void Start()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        if (!PuedeAtacar || estaMuerto) return;
        ControlarMovimiento();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccion);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccionAtaque);
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

    protected override void ControlarMovimiento()
    {
        float distancia = Vector2.Distance(Jugador.position, transform.position);
        float dirX = Mathf.Sign(Jugador.position.x - transform.position.x);

        if (distancia <= RadioDeteccionAtaque)
        {
            StartCoroutine(Atacar());
            return;
        }

        if (distancia <= RadioDeteccion)
        {
            EstaCaminando = true;
            Rb.velocity = new Vector2(dirX * Velocidad, Rb.velocity.y);
            Orientar(dirX);
        }
        else 
        {
            EstaCaminando = false;
            Rb.velocity = new Vector2(0f, Rb.velocity.y);
        }

        Animator.SetBool(CaminandoHash, EstaCaminando);
    }

    protected override IEnumerator Atacar()
    {
        PuedeAtacar = false;
        enAtaque = true;

        // Detenerse y dejar de animar el caminar
        EstaCaminando = false;
        Animator.SetBool(CaminandoHash, false);
        Rb.velocity = new Vector2(0f, Rb.velocity.y);

        // Mirar al jugador y fijar la dirección una sola vez
        float dirX = Mathf.Sign(Jugador.position.x - transform.position.x);
        Orientar(dirX);

        // Preparación (telegrafía el ataque)
        Animator.SetTrigger(PrepararAtaqueHash);
        yield return new WaitForSeconds(TiempoPreparacionDash);

        // Dash
        var zonaDeDanio = ZonaDanio.GetComponent<BoxCollider2D>();

        zonaDeDanio.enabled = true;
        Animator.SetBool(AtaqueHash, true);
        float tiempo = 0f;
        while (tiempo < DuracionDash)
        {
            Rb.velocity = new Vector2(dirX * Velocidad * 2, Rb.velocity.y);
            tiempo += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        zonaDeDanio.enabled = false;
        Animator.SetBool(AtaqueHash, false);
        enAtaque = false;
        Rb.velocity = new Vector2(0f, Rb.velocity.y);

        yield return new WaitForSeconds(CooldownAtaque);
        PuedeAtacar = true;
    }

    public override void RecibirDanio(int danio)
    {
        if (enAtaque || estaMuerto) return;
        Vida -= danio;
        if (Vida <= 0)
        {
            Morir();
            return;
        }
        Rb.velocity = Vector2.zero;
        Animator.SetTrigger(RecibioDanioHash);
    }

    protected override void Morir()
    {
        estaMuerto = true;
        StopAllCoroutines();
        ZonaDanio.GetComponent<BoxCollider2D>().enabled = false;
        foreach (var col in GetComponents<Collider2D>())
            col.enabled = false;
        Rb.velocity = Vector2.zero;
        Rb.simulated = false;
        Animator.SetBool(CaminandoHash, false);
        Animator.SetBool(AtaqueHash, false);
        Animator.SetTrigger(MuerteHash);
        Destroy(gameObject, SegundosHastaDesaparecer);
    }

    private void Orientar(float dirX)
    {
        if ((dirX > 0 && !MiradoDerecha()) || (dirX < 0 && MiradoDerecha()))
            Girar();
    }
}
