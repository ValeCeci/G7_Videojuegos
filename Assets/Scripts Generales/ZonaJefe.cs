using System.Collections;
using UnityEngine;

public class ZonaJefe : MonoBehaviour
{

    [Header("Puertas")]
    public Puerta Puerta;

    [Header("Jefe")]
    public Jefe JefeDeZona;
    public float CooldownInicioPelea = 5f;



    private bool _jugadorDentro = false;
    private bool _puertaCerrada = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_jugadorDentro && !_puertaCerrada)
        {
            JefeDeZona.RetrasarAtaque(CooldownInicioPelea);
            _puertaCerrada = true;
            Puerta.CerrarPuerta();
        }else if(JefeDeZona == null && _puertaCerrada)
        {
            Puerta.AbrirPuerta();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _jugadorDentro = true;
        }
    }
}
