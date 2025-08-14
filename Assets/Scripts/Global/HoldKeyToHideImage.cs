using UnityEngine;

// Estructura de datos para asociar una tecla, su imagen y un temporizador.
[System.Serializable]
public class KeyHoldPair
{
    public KeyCode key;
    public GameObject imageObject;
    
    // Este temporizador es específico para cada tecla. No se toca en el Inspector.
    [HideInInspector] public float holdTimer = 0f;
}

// Script principal que gestiona la lógica.
public class HoldKeyToHideImage : MonoBehaviour
{
    [Header("Configuración de Retención")]
    [Tooltip("El tiempo en segundos que se debe mantener la tecla presionada.")]
    public float holdDuration = 1.0f; // Puedes poner 1.0, 0.5, etc.

    [Header("Mapeo de Teclas e Imágenes")]
    public KeyHoldPair[] keyMappings;

    void Update()
    {
        // Usamos un bucle 'for' para poder modificar los valores de cada elemento.
        for (int i = 0; i < keyMappings.Length; i++)
        {
            // Primero, nos aseguramos de que el objeto de imagen todavía exista y esté visible.
            if (keyMappings[i].imageObject == null || !keyMappings[i].imageObject.activeSelf)
            {
                continue; // Si no está visible, pasamos al siguiente.
            }

            // --- Lógica Principal ---

            // 1. Si la tecla ESTÁ SIENDO PRESIONADA ahora mismo...
            if (Input.GetKey(keyMappings[i].key))
            {
                // Incrementamos su temporizador personal.
                keyMappings[i].holdTimer += Time.deltaTime;

                // Si el temporizador ha alcanzado la duración requerida...
                if (keyMappings[i].holdTimer >= holdDuration)
                {
                    // Ocultamos la imagen.
                    keyMappings[i].imageObject.SetActive(false);
                }
            }
            // 2. Si la tecla FUE SOLTADA en este frame...
            else if (Input.GetKeyUp(keyMappings[i].key))
            {
                // Reseteamos su temporizador a cero porque el usuario no la mantuvo el tiempo suficiente.
                keyMappings[i].holdTimer = 0f;
            }
        }
    }
}