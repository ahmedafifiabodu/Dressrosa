using UnityEngine;

public class EnableParticalEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.FOGCIRCLE))
            collision.gameObject.GetComponent<ParticleSystem>().Stop();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.FOGCIRCLE))
            collision.gameObject.GetComponent<ParticleSystem>().Play();
    }
}