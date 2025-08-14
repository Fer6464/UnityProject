
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
    private float rangeStart = 7f;
    private float rangeEnd = 14f;
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
                prefabs[Random.Range(0 , 2)],
                posicionDeGeneracion,
                Quaternion.identity
            );
            playerPointsManager.addEnemiesToDefeat();
            yield return new WaitForSeconds(waitTime);
            num++;
        }
    }

    public void minionsGen()
    {

        StartCoroutine(SpawnMinionCouroutine());

    }

    IEnumerator SpawnMinionCouroutine()
    {
        while (true)
        {
            float centroY = transform.position.y;
            float posicionYAleatoria = Random.Range(centroY + rangoMinY, centroY + rangoMaxY);
            Vector2 posicionDeGeneracion = new Vector2(transform.position.x, posicionYAleatoria);
            Instantiate(
                prefabs[2],
                posicionDeGeneracion,
                Quaternion.identity
            );
            yield return new WaitForSeconds(Random.Range(rangeStart,rangeEnd));
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
