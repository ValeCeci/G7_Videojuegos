using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public enum Direccion { Horizontal, Vertical }
    private enum Estado { HaciaB, HaciaA }
    [SerializeField]private Estado _estadoActual = Estado.HaciaB;
    private Vector2 _posAnterior;
    private Rigidbody2D _rb;

    [Header("Movilida")]
    public Direccion TipoMovimiento;
    public float Velocidad = 1;
    public Vector2 Delta { get; private set; }


    [Header("Puntos de Movimiento")]

    public Transform PuntoA;
    public Transform PuntoB;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Delta = (Vector2)transform.position - _posAnterior;
        _posAnterior = transform.position;
        CambioEstado();
        Mover();
    }

    private void CambioEstado()
    {
        if (_estadoActual == Estado.HaciaB && Llego(PuntoB))
            _estadoActual = Estado.HaciaA;
        else if (_estadoActual == Estado.HaciaA && Llego(PuntoA))
            _estadoActual = Estado.HaciaB;
    }

    private void Mover()
    {
        Transform destino = _estadoActual == Estado.HaciaB ? PuntoB : PuntoA;
        Vector2 direccion = (destino.position - transform.position).normalized;
        _rb.velocity = direccion * Velocidad;
    }

    private bool Llego(Transform destino)
    {
        return Vector2.Distance(transform.position, destino.position) <= 0.1f;
    }
}
