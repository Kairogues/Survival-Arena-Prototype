using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoBackButton : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    public void GoBackToMenu()
    {
        PlayGoBackSound();
        SceneManager.LoadScene(0);
    }

    private IEnumerator PlayGoBackSound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioClip.length);
    }
}
