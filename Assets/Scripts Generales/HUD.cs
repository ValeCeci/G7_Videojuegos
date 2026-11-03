using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Sprite[] Vidas;
    public GameObject ContenedorVida;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActualizarVidas(int vidas)
    {
        // La vida puede bajar de 0 con un golpe fuerte; se limita al rango del array
        int indice = Mathf.Clamp(vidas, 0, Vidas.Length - 1);
        ActualizarSpriteVida(Vidas[indice]);
    }

    private void ActualizarSpriteVida(Sprite sprite)
    {
        ContenedorVida.GetComponent<Image>().sprite = sprite;
    }
}
