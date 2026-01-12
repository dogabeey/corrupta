using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public Transform cameraParent;
    public Camera minZoomCamera, maxZoomCamera;
    public float maxXAtMinZoom, maxXAtMaxZoom, minXAtMinZoom, minXAtMaxZoom, maxZAtMinZoom, maxZAtMaxZoom, minZAtMinZoom, minZAtMaxZoom;
    public float scrollSpeed = 10f;
    public float zoomSpeed = 1f;

    private GameInput gameInput;
    private float zoomAmount;

    private void Awake()
    {
        gameInput = GameManager.Instance.gameInput;
        gameInput.MapControls.Enable();
    }
    private void OnEnable()
    {
        gameInput.MapControls.ScrollUp.performed += ctx => ScrollCameraByVector(Vector3.back);
        gameInput.MapControls.ScrollDown.performed += ctx => ScrollCameraByVector(Vector3.forward);
        gameInput.MapControls.ScrollLeft.performed += ctx => ScrollCameraByVector(Vector3.left);
        gameInput.MapControls.ScrollRight.performed += ctx => ScrollCameraByVector(Vector3.right);
        gameInput.MapControls.Zoom.performed += ctx => HandleZoomInput(ctx);
    }
    private void OnDisable()
    {
        gameInput.MapControls.ScrollUp.performed -= ctx => ScrollCameraByVector(Vector3.back);
        gameInput.MapControls.ScrollDown.performed -= ctx => ScrollCameraByVector(Vector3.forward);
        gameInput.MapControls.ScrollLeft.performed -= ctx => ScrollCameraByVector(Vector3.left);
        gameInput.MapControls.ScrollRight.performed -= ctx => ScrollCameraByVector(Vector3.right);
        gameInput.MapControls.Zoom.performed -= ctx => HandleZoomInput(ctx);
    }


    private void HandleZoomInput(InputAction.CallbackContext ctx)
    {
        float zoomInput = ctx.ReadValue<float>();
        zoomAmount -= zoomInput * zoomSpeed * Time.deltaTime * 60;
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

        float maxX = Mathf.Lerp(maxXAtMinZoom, maxXAtMaxZoom, zoomAmount + 1);
        float minX = Mathf.Lerp(minXAtMinZoom, minXAtMaxZoom, zoomAmount + 1);
        float maxZ = Mathf.Lerp(maxZAtMinZoom, maxZAtMaxZoom, zoomAmount + 1);
        float minZ = Mathf.Lerp(minZAtMinZoom, minZAtMaxZoom, zoomAmount + 1);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        cameraParent.DOMove(targetPosition, 0.1f);
    }
}
