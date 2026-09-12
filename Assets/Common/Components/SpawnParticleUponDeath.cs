using UnityEngine;

public class SpawnParticleUponDeath : MonoBehaviour
{
    [SerializeField] private LifeComponent lifeComponent;
    [SerializeField] private ParticleSystem particleSystem;



    private void Start()
    {
        lifeComponent.Died += SpawnParticle;
    }


    private void OnDestroy()
    {
        lifeComponent.Died -= SpawnParticle;
    }


    private void SpawnParticle()
    {
        particleSystem.Play();
    }
}
