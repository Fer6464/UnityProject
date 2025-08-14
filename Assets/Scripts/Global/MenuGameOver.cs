using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void Reintentar()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Inicio");
    }

}
