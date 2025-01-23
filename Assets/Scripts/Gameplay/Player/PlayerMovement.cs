using System.Collections;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] EntityData data;
    [SerializeField] LayerMask layerMask;
    [SerializeField] InputActionReference movement, dash, mousePos;

    Camera cam;
    Vector2 movementInput, mousePosition;
    Vector3 positionToLook;

    private void Start()
    {
        cam = CameraManager.Instance.currentCamera;
    }

    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        mousePosition = mousePos.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(movementInput.x * data.movementSpeed, 0, movementInput.y * data.movementSpeed) * Time.deltaTime, Space.World);
    }

    void LateUpdate()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            positionToLook = raycastHit.point;

            positionToLook.y = transform.position.y;

            transform.LookAt(positionToLook);
        }
    }

    void Dash(InputAction.CallbackContext context)
    {
        transform.Translate(new Vector3(movementInput.x * data.dashSpeed, 0, movementInput.y * data.dashSpeed) * Time.deltaTime, Space.World);
    }

    void OnEnable()
    {
        dash.action.performed += Dash;
    }

    void OnDisable()
    {
        dash.action.performed -= Dash;
    }
}
