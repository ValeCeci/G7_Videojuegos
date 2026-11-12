using System.Collections;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public int Danio = 2;
    public float Velocidad = 5f;
    public float TiempoVida = 5f;

    [Tooltip("Ajuste del ángulo según hacia dónde mira el sprite: 0 = derecha, 90 = arriba, 180 = izquierda, -90 = abajo")]
    public float AjusteAngulo = 0f;

    private PlayerController _player;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        _player = jugador.GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();

        StartCoroutine(DispararHacia(jugador.transform.position));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.RecibirDanio(Danio, new Vector2(transform.position.x, 0));
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Suelo"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DispararHacia(Vector3 objetivo)
    {
        Vector2 direccion = (objetivo - transform.position).normalized;

        // Mirar hacia el objetivo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo + AjusteAngulo);

        // velocity ya es unidades/segundo, no se multiplica por deltaTime
        _rb.velocity = direccion * Velocidad;
        yield return new WaitForSeconds(TiempoVida);
        Destroy(gameObject);
    }

}
