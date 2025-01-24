using System.Collections;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAnimation;

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
        if (movementInput != Vector2.zero && isRunning)
        {
            pManager.playerAnimation.animator.SetBool(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.RUN.ToString(), true);
            pManager.playerAnimation.animator.SetBool(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.WALK.ToString(), false);
        }
        else if (movementInput != Vector2.zero)
        {
            pManager.playerAnimation.animator.SetBool(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.WALK.ToString(), true);
            pManager.playerAnimation.animator.SetBool(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.RUN.ToString(), false);
        }
        else
        {
            pManager.playerAnimation.animator.SetBool(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.WALK.ToString(), false);
            pManager.playerAnimation.animator.SetBool(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.RUN.ToString(), false);
        }
    }


    void LateUpdate()
    {
        float bonusSpeed = isRunning ? data.runSpeed : data.walkSpeed;
        transform.Translate(new Vector3(movementInput.x * bonusSpeed, 0, movementInput.y * bonusSpeed) * Time.deltaTime, Space.World);

        RotatePlayer();
    }


    private void RotatePlayer()
    {
        string animName = PlayerManager.Instance.playerAnimation.GetCurrentAnimationName();

        Debug.Log(animName);

        if (movementInput != Vector2.zero
            && (animName == PLAYER_ANIMATION_PARAMETER.WALK.ToString()
            || animName == PLAYER_ANIMATION_PARAMETER.RUN.ToString()))
        {
            transform.LookAt(new Vector3(transform.position.x + movementInput.x, 0, transform.position.z + movementInput.y));
        }
    }

    void Dash(InputAction.CallbackContext context)
    {
        if (movementInput == Vector2.zero) return;

        pManager.playerAnimation.animator.SetTrigger(PlayerAnimation.PLAYER_ANIMATION_PARAMETER.DASH.ToString());

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
