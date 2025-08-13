
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ObjectGenerator : MonoBehaviour
{
    public int generationLimit = 0;
    public float rangoMinY = -2f;
    public float rangoMaxY = 2f;
    public float waitTime = 5f;
    PlayerPointsManager playerPointsManager;
    public GameObject[] prefabs;

    public void generateObjects()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerPointsManager = player.GetComponent<PlayerPointsManager>();
        StartCoroutine(SpawnCouroutine(waitTime));
    }
    IEnumerator SpawnCouroutine(float waitTime)
    {
        int num = 0;
        while (num < generationLimit)
        {
            float centroY = transform.position.y;
            float posicionYAleatoria = Random.Range(centroY + rangoMinY, centroY + rangoMaxY);
            Vector2 posicionDeGeneracion = new Vector2(transform.position.x, posicionYAleatoria);
            Instantiate(
                prefabs[Random.Range(0, prefabs.Length)],
                posicionDeGeneracion,
                Quaternion.identity
            );
            playerPointsManager.addEnemiesToDefeat();
            yield return new WaitForSeconds(waitTime);
            num++;
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        // Define el color del Gizmo para que sea fácil de ver.
        Gizmos.color = Color.yellow;

        // Crea los puntos de inicio y fin para la línea vertical del rango.
        Vector2 puntoInicio = new Vector2(transform.position.x, rangoMinY + transform.position.y);
        Vector2 puntoFinal = new Vector2(transform.position.x, rangoMaxY + transform.position.y);

        // Dibuja la línea vertical que representa el rango de generación.
        Gizmos.DrawLine(puntoInicio, puntoFinal);

        // Dibuja dos pequeñas esferas en los extremos para marcar claramente el mínimo y el máximo.
        Gizmos.DrawSphere(puntoInicio, 0.1f);
        Gizmos.DrawSphere(puntoFinal, 0.1f);
    }
}
