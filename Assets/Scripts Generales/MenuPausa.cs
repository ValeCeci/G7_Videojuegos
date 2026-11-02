using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    // Lo revisan los scripts del jugador para no leer input mientras el juego esta en pausa
    public static bool JuegoPausado { get; private set; }

    [Header("Referencias")]
    [SerializeField] private GameObject panelPausa;

    [Header("Configuracion")]
    [SerializeField] private KeyCode teclaPausa = KeyCode.Escape;
    [SerializeField] private string escenaInicio = "MenuPrincipal";

    private void Start()
    {
        // Sin EventSystem los botones no reciben clics; si el nivel no tiene uno se crea aqui
        if (EventSystem.current == null)
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));

        CambiarPausa(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(teclaPausa))
            CambiarPausa(!JuegoPausado);
    }

    // Boton SEGUIR
    public void Reanudar()
    {
        CambiarPausa(false);
    }

    // Boton INICIO
    public void VolverAlInicio()
    {
        CambiarPausa(false);

        if (Application.CanStreamedLevelBeLoaded(escenaInicio))
        {
            SceneManager.LoadScene(escenaInicio);
        }
        else
        {
            // Mientras no exista la escena del menu principal se reinicia el nivel actual
            Debug.LogWarning($"La escena '{escenaInicio}' no esta en Build Settings, se reinicia el nivel");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Boton SALIR
    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void CambiarPausa(bool pausar)
    {
        JuegoPausado = pausar;
        panelPausa.SetActive(pausar);

        // Con timeScale en 0 se congelan la fisica, las animaciones y los WaitForSeconds
        Time.timeScale = pausar ? 0f : 1f;
        AudioListener.pause = pausar;

        // Se limpia la seleccion para que Enter o Espacio no activen un boton por accidente
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }
}
