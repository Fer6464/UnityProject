using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictory : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Inicio");
    }
}
