using System.Collections;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] EntityData data;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Rigidbody rb;
    [SerializeField] InputActionReference movement, dash, run, mousePos;

    Camera cam;
    PlayerManager pManager;
    Vector2 movementInput, mousePosition;
    Vector3 positionToLook;
    bool isRunning;

    private void Start()
    {
        cam = CameraManager.Instance.currentCamera;
        pManager = PlayerManager.Instance;
    }

    void Update()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        mousePosition = mousePos.action.ReadValue<Vector2>();
        isRunning = run.action.IsPressed();

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (rb.linearVelocity != Vector3.zero) pManager.playerAnimation.PlayAnimation(PlayerAnimation.PLAYER_ANIMATION.DASH);
        else if (movementInput != Vector2.zero && isRunning) pManager.playerAnimation.PlayAnimation(PlayerAnimation.PLAYER_ANIMATION.RUN);
        else if (movementInput != Vector2.zero) pManager.playerAnimation.PlayAnimation(PlayerAnimation.PLAYER_ANIMATION.WALK);
        else pManager.playerAnimation.PlayAnimation(PlayerAnimation.PLAYER_ANIMATION.IDLE);

         // TO DO FIX ANIMATION
    }

    void FixedUpdate()
    {
        float bonusSpeed = isRunning ? data.runSpeed : data.walkSpeed;
        transform.Translate(new Vector3(movementInput.x * bonusSpeed, 0, movementInput.y * bonusSpeed) * Time.deltaTime, Space.World);
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
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(new Vector3(movementInput.x, 0, movementInput.y) * data.dashForce, ForceMode.Impulse);
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
