using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraParent;
    public Camera minZoomCamera, maxZoomCamera;
    public float maxX, minX, maxZ, minZ;
    public float scrollSpeed = 10f;
    public float zoomSpeed = 5f;

    private float zoomAmount;

    private void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        zoomAmount += zoomInput * zoomSpeed;

        zoomAmount = Mathf.Clamp(zoomAmount, -1, 1);
        LerpCamera(minZoomCamera, maxZoomCamera, zoomAmount);


    }

    private void LerpCamera(Camera cam1, Camera cam2, float t)
    {
        float orthographicSize = Mathf.Lerp(cam1.orthographicSize, cam2.orthographicSize, t);
        Vector3 position = Vector3.Lerp(cam1.transform.localPosition, cam2.transform.localPosition, t);
        Vector3 eulerAngles = Vector3.Lerp(cam1.transform.localEulerAngles, cam2.transform.localEulerAngles, t);
        float fieldOfView = Mathf.Lerp(cam1.fieldOfView, cam2.fieldOfView, t);
        float nearClipPlane = Mathf.Lerp(cam1.nearClipPlane, cam2.nearClipPlane, t);
        float farClipPlane = Mathf.Lerp(cam1.farClipPlane, cam2.farClipPlane, t);

        Camera.main.DOOrthoSize(orthographicSize, 0.1f);
        Camera.main.transform.DOLocalMove(position, 0.1f);
        Camera.main.transform.DOLocalRotate(eulerAngles, 0.1f);
        Camera.main.fieldOfView = fieldOfView;
        Camera.main.nearClipPlane = nearClipPlane;
        Camera.main.farClipPlane = farClipPlane;

    }

    private void ScrollCameraByVector(Vector3 direction)
    {
        Vector3 targetPosition = cameraParent.position + direction * scrollSpeed * Time.deltaTime;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
        cameraParent.position = targetPosition;
    }
}
