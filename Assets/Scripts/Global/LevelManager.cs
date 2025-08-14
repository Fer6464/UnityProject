using UnityEngine;
using UnityEngine.SceneManagement; // <-- MUY IMPORTANTE AÑADIR ESTO

public class LevelManager : MonoBehaviour
{
    // Patrón Singleton: permite que este script sea fácilmente accesible desde cualquier otro script
    public static LevelManager instance;

    private void Awake()
    {
        // Configuración del Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: hace que no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToNextLevel()
    {
        // Obtiene el índice de la escena actual en los Build Settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calcula el índice de la siguiente escena
        int nextSceneIndex = currentSceneIndex + 1;
        
        // Si la siguiente escena existe, la carga
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Cargando siguiente nivel...");
            SceneManager.LoadScene(0);
        }
        else
        {
            // Si no hay más niveles, carga el menú principal (escena 0) o muestra un mensaje
            Debug.Log("¡Has completado todos los niveles! Volviendo al inicio.");
            SceneManager.LoadScene("Inicio"); 
        }
    }
   public void skiptutorial()
    {
        SceneManager.LoadScene(0);
    }
}
