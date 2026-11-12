using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaObjeto : MonoBehaviour
{

    [Header("Objeto")]
    public Rigidbody2D Objeto;
    public float VelocidadCaida = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Objeto.gravityScale = VelocidadCaida;
        }
    }

}
