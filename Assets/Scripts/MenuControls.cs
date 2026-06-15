using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void cambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void salirJuego()
    {
        Application.Quit();
    }
}
