using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDanioDeEnemigo : MonoBehaviour
{
    private Enemigo enemigo;
    void Start()
    {
        enemigo = GetComponentInParent<Enemigo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            var jugador = collision.GetComponent<PlayerController>();
            jugador.RecibirDanio(enemigo.Danio, new Vector2(transform.position.x, 0));
        }
    }


}
