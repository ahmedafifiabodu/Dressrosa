using UnityEngine;

public class StopParticleSystemOnTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemToStop;

    private bool rockInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.FOGROCK))
        {
            rockInsideTrigger = true;

            if (particleSystemToStop != null)
                particleSystemToStop.Stop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.FOGROCK))
        {
            rockInsideTrigger = false;

            // Check if the particle system is valid
            if (particleSystemToStop != null)
                if (!rockInsideTrigger)
                    particleSystemToStop.Play();
        }
    }
}