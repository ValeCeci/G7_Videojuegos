using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjetoVital : MonoBehaviour
{

    [Header("Vitalidad")]
    public int Vida = 5;
    public Jefe JefeAsociado;
    public float TiempoInvulnerable = 2f;

    protected Animator _animator;
    protected bool _invulnerable = false;
    protected SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract IEnumerator RecibirDanio(int danio);

    public abstract void Desaparecer();

    protected virtual IEnumerator Invulnerabilidad()
    {
        _invulnerable = true;
        yield return new WaitForSeconds(TiempoInvulnerable);
        _invulnerable = false;
    }

}
