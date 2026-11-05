using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeRocaController : Jefe
{
    private static readonly int _hashCargandoAtaque = Animator.StringToHash("cargandoAtaque");

    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (JugadorDetectado() && _puedeAtacar)
        {
            _puedeAtacar = false;
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
            yield return new WaitForSeconds(CooldownAtaque);
            _puedeAtacar = true;
    }

    protected override void EjecutarGolpe(Vector2 posicion, GameObject golpe)
    {
        Instantiate( golpe, posicion, Quaternion.identity);
    }


}
