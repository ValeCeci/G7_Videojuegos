using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gema : ObjetoVital
{

    private static readonly int _hashDespawn = Animator.StringToHash("romper");

    // Start is called before the first frame update
    void Start()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Desaparecer()
    {
        JefeAsociado.ObjetoVitalConVida = false;
        Destroy(gameObject);
    }

    public override IEnumerator RecibirDanio(int danio)
    {
        if (!_invulnerable)
        {
            Vida -= danio;
            if(Vida <= 0)
            {
                JefeAsociado.RecibirDanio(danio);
                _animator.SetTrigger(_hashDespawn);
            }
            StartCoroutine(Invulnerabilidad());
            var colorOriginal = _spriteRenderer.color;
            _spriteRenderer.color = new Color(255f, 0f, 235f, 255f); 
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = colorOriginal;
            
        }
    }
}
