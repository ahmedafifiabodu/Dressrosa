using Cinemachine;
using UnityEngine;

public class CameraZoomInAndZoomOut : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private float maxZoomOut = 5f;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    private bool zoomIn = false;

    [Range(0, 10)]
    [SerializeField]
    private float zoomSize;

    [Range(0.01f, 0.1f)]
    [SerializeField]
    private float zoomSpeed;

    [Range(1, 3)]
    [SerializeField]
    private float waitTime = 1.3f;

    private float waitCounter;

    //private void ZoomIn() => camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomSize, zoomSpeed);
    private void ZoomIn() => virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomSize, zoomSpeed);

    //private void ZoomOut() => camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 10, zoomSpeed);
    private void ZoomOut() => virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, maxZoomOut, zoomSpeed);

    private void CameraUpdate()
    {
        if (Mathf.Abs(playerRigidbody2D.velocity.magnitude) < 1)
        {
            waitCounter += Time.deltaTime;

            if (waitCounter > waitTime)
            {
                zoomIn = true;
            }
        }
        else
        {
            zoomIn = false;
            waitCounter = 0;
        }

        if (zoomIn)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void LateUpdate() => CameraUpdate();
}