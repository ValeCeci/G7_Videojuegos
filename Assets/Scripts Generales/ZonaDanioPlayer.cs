using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDanioPlayer : MonoBehaviour
{
    private PlayerController PlayerController;

    private void Start()
    {
        PlayerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemigo"))
        {
            var enemigo = collision.GetComponent<Enemigo>();
            enemigo.RecibirDanio(PlayerController.Danio);
        }
    }
}
