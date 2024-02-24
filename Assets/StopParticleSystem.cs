using UnityEngine;

public class StopParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemToStop;

    private bool rockInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FogRock"))
        {
            StonePuzzle.quset1_Counter += 1;
            rockInsideTrigger = true;
            collision.gameObject.layer = LayerMask.NameToLayer("Default");
            // Check if the particle system is valid
            if (particleSystemToStop != null)
            {
                // Stop the particle system
                particleSystemToStop.Stop();
                Destroy(gameObject);
            }
        }
    }
}