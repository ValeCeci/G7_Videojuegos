using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeRocaController : Jefe
{
    private static readonly int _hashCargandoAtaque = Animator.StringToHash("cargandoAtaque");
    private static readonly int _hashRecibirDanio = Animator.StringToHash("recibirDanio");
    private static readonly int _hashMorir = Animator.StringToHash("muerte");

    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if(_muerto) return;
        SpawnearObjetoVital();
        if (JugadorDetectado() && PuedeAtacar)
        {
            _atacando = true;
            _animator.SetTrigger(_hashCargandoAtaque);
        }
    }

    public void AtaqueCorrutina()
    {
        StartCoroutine(Atacar());
    }

    protected override IEnumerator Atacar()
    {
            Vector2 posicionGolpe = new Vector2(_jugador.position.x, transform.position.y);
            EjecutarGolpe(posicionGolpe, Golpe);
            _atacando = false;
            _proximoAtaque = Time.time + CooldownAtaque;
            yield break;
    }

    protected override void EjecutarGolpe(Vector2 posicion, GameObject golpe)
    {
        Instantiate( golpe, posicion, Quaternion.identity);
    }


    public override void RecibirDanio(int danio)
    {
        Vida -= danio;
        // Cancela la carga en curso (la animacion de dano la interrumpe) y retrasa el siguiente ataque
        _atacando = false;
        RetrasarAtaque(TiempoRecuperacion);
        if(Vida <= 0)
        {
            _muerto = true;
            _animator.SetTrigger(_hashMorir);
            return;
        }
        _animator.SetTrigger(_hashRecibirDanio);
    }

    public override void Morir()
    {
        Destroy(gameObject);
    }
}
