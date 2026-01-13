using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public Transform cameraParent;
    public Camera minZoomCamera, maxZoomCamera;
    public float edgeScrollThreshold = 5f;
    public float maxXAtMinZoom, maxXAtMaxZoom, minXAtMinZoom, minXAtMaxZoom, maxZAtMinZoom, maxZAtMaxZoom, minZAtMinZoom, minZAtMaxZoom;
    public float scrollSpeed = 1f;
    public float zoomSpeed = 1f;

    private GameInput gameInput;
    private float zoomAmount;
    private Vector3 currentDirectionHotkey;
    private Vector3 currentDirectionEdgeScrolling;

    private void Awake()
    {
        gameInput = GameManager.Instance.gameInput;
        gameInput.MapControls.Enable();
    }
    private void OnEnable()
    {
        gameInput.MapControls.ScrollUp.started += ctx => ScrollCameraByVector(Vector3.forward, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollDown.started += ctx => ScrollCameraByVector(Vector3.back, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollLeft.started += ctx => ScrollCameraByVector(Vector3.left, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollRight.started += ctx => ScrollCameraByVector(Vector3.right, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollUp.canceled += ctx => SetDirectionToZero();
        gameInput.MapControls.ScrollDown.canceled += ctx => SetDirectionToZero();
        gameInput.MapControls.ScrollLeft.canceled += ctx => SetDirectionToZero();
        gameInput.MapControls.ScrollRight.canceled += ctx => SetDirectionToZero();
        gameInput.MapControls.Zoom.performed += ctx => HandleZoomInput(ctx);
    }
    private void OnDisable()
    {
        gameInput.MapControls.ScrollUp.started -= ctx => ScrollCameraByVector(Vector3.forward, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollDown.started -= ctx => ScrollCameraByVector(Vector3.back, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollLeft.started -= ctx => ScrollCameraByVector(Vector3.left, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollRight.started -= ctx => ScrollCameraByVector(Vector3.right, ref currentDirectionHotkey);
        gameInput.MapControls.ScrollUp.canceled -= ctx => SetDirectionToZero();
        gameInput.MapControls.ScrollDown.canceled -= ctx => SetDirectionToZero();
        gameInput.MapControls.ScrollLeft.canceled -= ctx => SetDirectionToZero();
        gameInput.MapControls.ScrollRight.canceled -= ctx => SetDirectionToZero();
        gameInput.MapControls.Zoom.performed -= ctx => HandleZoomInput(ctx);
    }

    private void Update()
    {
        HandleEdgeScrolling();
        cameraParent.transform.DOMove(currentDirectionHotkey, 0.1f).SetRelative();
        cameraParent.transform.DOMove(currentDirectionEdgeScrolling, 0.1f).SetRelative();
    }

    private void HandleEdgeScrolling()
    {
        // Check if the mouse is near the left edge of the screen
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x < edgeScrollThreshold)
        {
            ScrollCameraByVector(Vector3.left, ref currentDirectionEdgeScrolling);
        }
        // Check if the mouse is near the right edge of the screen
        if (Input.mousePosition.x <= Screen.width && Input.mousePosition.x > Screen.width - edgeScrollThreshold)
        {
            ScrollCameraByVector(Vector3.right, ref currentDirectionEdgeScrolling);
        }
        // Check if the mouse is near the bottom edge of the screen
        if (Input.mousePosition.y >= 0 && Input.mousePosition.y < edgeScrollThreshold)
        {
            ScrollCameraByVector(Vector3.back, ref currentDirectionEdgeScrolling);
        }
        // Check if the mouse is near the top edge of the screen
         if (Input.mousePosition.y <= Screen.height && Input.mousePosition.y > Screen.height - edgeScrollThreshold)
        {
            ScrollCameraByVector(Vector3.forward, ref currentDirectionEdgeScrolling);
        }
        // If the mouse is not near any edge, set the scroll direction to zero
        if (Input.mousePosition.x >= edgeScrollThreshold && Input.mousePosition.x <= Screen.width - edgeScrollThreshold &&
            Input.mousePosition.y >= edgeScrollThreshold && Input.mousePosition.y <= Screen.height - edgeScrollThreshold)
        {
            SetScrollDirectionToZero();
        }
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

    private void ScrollCameraByVector(Vector3 direction, ref Vector3 currentDirection)
    {
        Vector3 targetPosition = direction * scrollSpeed * Time.deltaTime;

        float maxX = Mathf.Lerp(maxXAtMinZoom, maxXAtMaxZoom, zoomAmount + 1);
        float minX = Mathf.Lerp(minXAtMinZoom, minXAtMaxZoom, zoomAmount + 1);
        float maxZ = Mathf.Lerp(maxZAtMinZoom, maxZAtMaxZoom, zoomAmount + 1);
        float minZ = Mathf.Lerp(minZAtMinZoom, minZAtMaxZoom, zoomAmount + 1);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        currentDirection = targetPosition;
    }
    private void SetDirectionToZero()
    { 
        currentDirectionHotkey = Vector3.zero;
    }
    private void SetScrollDirectionToZero()
    {
        currentDirectionEdgeScrolling = Vector3.zero;
    }
}
