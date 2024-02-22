using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitTime = 2f;
    private int currentPointIndex = 0;

    private void Start()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position;
            StartCoroutine(MoveToNextPoint());
        }
    }

    private IEnumerator MoveToNextPoint()
    {
        while (true)
        {
            var targetPoint = points[currentPointIndex].position;
            while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }
}