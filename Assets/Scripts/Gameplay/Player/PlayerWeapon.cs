using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAnimation;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    [SerializeField] InputActionReference shoot, aim, mousePos;
    [SerializeField] LayerMask layerMask;
    [SerializeField] WeaponType defaultWeaponType = WeaponType.Pistol;

    Vector3 positionToLook;
    Vector2 mousePosition;
    Weapon currentWeapon;

    private void Start()
    {
        EquipWeapon(defaultWeaponType);
    }

    private void Update()
    {
        mousePosition = mousePos.action.ReadValue<Vector2>();

        if (shoot.action.IsPressed())
        {
            RotatePlayer();

            currentWeapon.Fire();
        }


        if (aim.action.IsPressed())
        {
            RotatePlayer();
        }

        PlayerManager.Instance.playerAnimation.animator.SetBool(PLAYER_ANIMATION_PARAMETER.HOLDING_RIGHT.ToString(), aim.action.IsPressed());
    }

    private void RotatePlayer()
    {
        Ray ray = CameraManager.Instance.currentCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            positionToLook = raycastHit.point;

            positionToLook.y = transform.position.y;

            transform.LookAt(positionToLook);

            positionToLook.y = currentWeapon.transform.position.y;

            currentWeapon.transform.LookAt(positionToLook);
        }
    }

    public void EquipWeapon(WeaponType type)
    {
        if (currentWeapon != null)
        {
            PoolManager.Instance[currentWeapon.WeaponData.type].Release(currentWeapon.gameObject);
        }

        currentWeapon = PoolManager.Instance[ResourceType.Pistol].Get().GetComponent<Weapon>();

        currentWeapon.transform.SetParent(weaponHolder);
        currentWeapon.transform.SetPositionAndRotation(weaponHolder.position, weaponHolder.rotation);
    }
}
