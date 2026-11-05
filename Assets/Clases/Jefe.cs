using System.Collections;
using UnityEngine;

public abstract class Jefe : MonoBehaviour
{

    [Header("Detección")]
    public float RadioDeteccion = 15f;


    [Header("Ataque")]
    public int Danio = 3;
    public float CooldownAtaque = 3f;
    public GameObject Golpe;

    protected Transform _jugador;
    protected bool _puedeAtacar = true;
    protected Animator _animator;

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
    }

    protected abstract IEnumerator Atacar();
    protected abstract void EjecutarGolpe(Vector2 posicion, GameObject golpe);
}
