using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barro : MonoBehaviour
{
    [Header("Penalizaciones")]
    public float caminado = 1.5f;
    public float correr = 1.5f;
    public float salto = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            var movimientoJugador = collision.GetComponent<MovimientoJugador>();
            movimientoJugador.fuerzaSalto -= salto;
            movimientoJugador.velocidadCorrer -= correr;
            movimientoJugador.velocidadMovimiento -= caminado;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            var movimientoJugador = collision.GetComponent<MovimientoJugador>();
            movimientoJugador.fuerzaSalto += salto;
            movimientoJugador.velocidadCorrer += correr;
            movimientoJugador.velocidadMovimiento += caminado;
        }
    }
}
