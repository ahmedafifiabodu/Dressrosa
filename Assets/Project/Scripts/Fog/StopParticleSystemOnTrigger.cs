
using UnityEngine;

public class StopParticleSystemOnTrigger : MonoBehaviour
{
	[SerializeField] private ParticleSystem particleSystemToStop;

	private bool rockInsideTrigger = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("FogRock"))
		{
			rockInsideTrigger = true;

			// Check if the particle system is valid
			if (particleSystemToStop != null)
			{
				// Stop the particle system
				particleSystemToStop.Stop();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("FogRock"))
		{
			rockInsideTrigger = false;

			// Check if the particle system is valid
			if (particleSystemToStop != null)
			{
				// Start the particle system if the rock has exited the trigger zone
				if (!rockInsideTrigger)
				{
					particleSystemToStop.Play();
				}
			}
		}
	}
}
