using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   public GameObject menuOpciones;
   public GameObject menuPrincipal;

    public void AbrirMenuOpciones()
    {
        menuOpciones.SetActive(true);
        menuPrincipal.SetActive(false);
    }

    public void AbrirMenuPrincipal()
    {
        menuOpciones.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void Jugar()
    {
        SceneManager.LoadScene("IslaTierra");
    }

}
