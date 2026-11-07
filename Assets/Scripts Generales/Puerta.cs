using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    private Animator _animator;
    private BoxCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirPuerta()
    {
        _animator.SetTrigger("abrir");
    }

    public void CerrarPuerta()
    {
        _animator.SetTrigger("cerrar");
    }

    public void ActivarCollider()
    {
        _collider.enabled = true;
    }

    public void DesactivarCollider()
    {
        _collider.enabled = false;
    }
}
