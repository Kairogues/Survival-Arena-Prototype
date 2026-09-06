using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourcePrototype;

    public void PlaySoundFX(AudioClip clip, Transform playPosition, float volume) 
    {
        AudioSource audioSource = Instantiate(audioSourcePrototype, playPosition.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
