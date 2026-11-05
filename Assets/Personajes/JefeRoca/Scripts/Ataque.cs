using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    [Header("Daño")]
    public Jefe _jefe;

    private BoxCollider2D _boxCollider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestruirObjeto()
    {
        Destroy(gameObject);
    }

    public void ActivarZonaDanio()
    {
        Debug.Log("Activando zona de daño");
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().RecibirDanio(_jefe.Danio, new Vector2(transform.position.x, transform.position.y));
        }
    }
}

