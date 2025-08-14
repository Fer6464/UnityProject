using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIncio : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Salir()
    {
        Application.Quit(); // Solo funciona en compilado, no en editor
    }
}
