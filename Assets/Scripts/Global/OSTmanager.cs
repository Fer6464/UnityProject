using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OSTmanager : MonoBehaviour
{
    // Clips de audio iniciales
    public AudioClip ostOnStart;
    public AudioClip ostOnLoop;

    // Fuentes de audio para reproducir los clips
    public AudioSource ostStart;
    public AudioSource ostLoop;

    [Tooltip("Duración del efecto fade en segundos.")]
    public float fadeDuration = 1.5f;
    
    private Coroutine coroutine;

    // --- MÉTODOS DE UNITY ---

    void Start()
    {
        // Inicia la música al comenzar la escena
        if (ostOnStart != null)
        {
            coroutine = StartCoroutine(TransitionCoroutine(ostOnStart, ostOnLoop));
        }
        ostOnLoop.LoadAudioData();
    }

    // --- MÉTODOS PÚBLICOS ---

    /// <summary>
    /// Cambia la música actual por una nueva con una transición suave.
    /// </summary>
    public void ChangeStartOst(AudioClip newOstOnStart, AudioClip newOstOnLoop)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(TransitionCoroutine(newOstOnStart, newOstOnLoop));
    }
    
    // --- CORRUTINAS ---

    /// <summary>
    /// Corrutina principal que gestiona la transición de audio.
    /// </summary>
    private IEnumerator TransitionCoroutine(AudioClip newStartClip, AudioClip newLoopClip)
    {
        // 1. Fade Out: Si hay música sonando, baja el volumen.
        if (ostStart.isPlaying) yield return StartCoroutine(FadeAudio(ostStart, fadeDuration, 0f));
        if (ostLoop.isPlaying) yield return StartCoroutine(FadeAudio(ostLoop, fadeDuration, 0f));

        ostStart.Stop();
        ostLoop.Stop();
        
        // 2. Asignar y Preparar: Cambia los clips de audio.
        ostStart.clip = newStartClip;
        ostLoop.clip = newLoopClip;

        // 3. Fade In: Inicia el nuevo intro subiendo el volumen.
        ostStart.volume = 0f;
        ostStart.Play();
        yield return StartCoroutine(FadeAudio(ostStart, fadeDuration, 0.3f));

        // 4. Transición al Loop: Espera a que termine el intro y empieza el loop.
        yield return new WaitUntil(() => !ostStart.isPlaying);

        if (ostLoop.clip != null)
        {
            ostLoop.volume = 0.3f; // El loop empieza a volumen completo
            ostLoop.Play();
        }
    }

    /// <summary>
    /// Corrutina auxiliar que ajusta el volumen de un AudioSource.
    /// </summary>
    private IEnumerator FadeAudio(AudioSource source, float duration, float targetVolume)
    {
        float timer = 0f;
        float startVolume = source.volume;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }
}
