using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] float threshold = 3f;
    [SerializeField] InputActionReference mousePos;
    [SerializeField] float smoothingSpeed = 5f;

    Camera cam;
    Transform player;
    Vector2 mousePosition;

    private void Start()
    {
        cam = CameraManager.Instance.currentCamera;
        player = PlayerManager.Instance.transform;
    }


    private void LateUpdate()
    {
        mousePosition = mousePos.action.ReadValue<Vector2>();

        Vector3 targetPos = cam.ScreenToViewportPoint(mousePosition);

        targetPos.z = targetPos.y;
        targetPos.x -= 0.5f;
        targetPos.y = 0;
        targetPos.z -= 0.5f;

        targetPos *= threshold;

        Vector3 desiredPosition = player.position + targetPos;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothingSpeed);
    }
}
