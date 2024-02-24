using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
	public ParticleSystem[] particleSystems;
	public GameObject otherObject;

	void Update()
	{
		bool allStopped = true;

		// Check if all particle systems have stopped
		foreach (ParticleSystem ps in particleSystems)
		{
			if (ps.isPlaying)
			{
				allStopped = false;
				break;
			}
		}

		// Check if the other object has stopped
		if (otherObject != null && otherObject.activeSelf)
		{
			allStopped = false;
		}

		// If all particle systems and the other object have stopped, destroy this object
		if (allStopped)
		{
			Destroy(gameObject);
			foreach (ParticleSystem ps in particleSystems)
			{
				Destroy(ps.gameObject);
			}
		}
	}
}
