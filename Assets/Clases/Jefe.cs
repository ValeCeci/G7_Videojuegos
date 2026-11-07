using System.Collections;
using UnityEngine;

public abstract class Jefe : MonoBehaviour
{

    [Header("Detección")]
    public float RadioDeteccion = 15f;


    [Header("Ataque")]
    public int Vida = 10;
    public int Danio = 3;
    public float CooldownAtaque = 3f;
    public GameObject Golpe;
    public float TiempoRecuperacion = 2f;
    public float RadioSpawnObjetoVital;
    public GameObject ObjetoVital;
    public bool ObjetoVitalConVida;

    protected Transform _jugador;
    protected float _proximoAtaque = 0f;
    protected bool _atacando = false;
    protected Animator _animator;
    protected bool _muerto = false;


    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        _animator = GetComponent<Animator>();
    }

    protected bool JugadorDetectado()
    {
        if (_jugador == null) return false;
        float distancia = Vector2.Distance(transform.position, _jugador.position);
        return distancia <= RadioDeteccion;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RadioDeteccion);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RadioSpawnObjetoVital);
    }

    protected abstract IEnumerator Atacar();
    public abstract void Morir();
    protected abstract void EjecutarGolpe(Vector2 posicion, GameObject golpe);
    public abstract void RecibirDanio(int danio);
    protected bool PuedeAtacar => !_atacando && Time.time >= _proximoAtaque;

    public void RetrasarAtaque(float segundos)
    {
        _proximoAtaque = Mathf.Max(_proximoAtaque, Time.time + segundos);
    }

    public void SpawnearObjetoVital()
    {
        if (!ObjetoVitalConVida)
        {
            ObjetoVitalConVida = true;
            float posicionMinimaSpawn = transform.position.x - RadioSpawnObjetoVital;
            float posicionMaximaSpawn = transform.position.x + RadioSpawnObjetoVital + 1;
            Vector2 posicionSpawn = new Vector2(Random.Range(posicionMinimaSpawn, posicionMaximaSpawn), transform.position.y);
            GameObject instancia = Instantiate(ObjetoVital, posicionSpawn, Quaternion.identity);
            // El prefab no puede referenciar al jefe de la escena, se asigna al instanciar
            var objetoVital = instancia.GetComponent<ObjetoVital>();
            if (objetoVital != null)
                objetoVital.JefeAsociado = this;
        }
    }

}
