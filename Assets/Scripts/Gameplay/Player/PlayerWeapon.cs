using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerAnimation;

public class PlayerWeapon : MonoBehaviour
{
    public PlayerInput PInput;

    [SerializeField] Transform weaponHolder;
    [SerializeField] InputActionReference shoot, aim, mousePos;
    [SerializeField] LayerMask layerMask;
    [SerializeField] WeaponType defaultWeaponType = WeaponType.Pistol;

    Vector3 positionToLook;
    Vector2 mousePosition;
    public Weapon currentWeapon;
    DistanceWeaponData weaponData;

    public List<Func<IProjectileBehaviour, IProjectileBehaviour>> bulletDecoratorFuncs =
        new List<Func<IProjectileBehaviour, IProjectileBehaviour>>();

    private void Start()
    {
        EquipWeapon(defaultWeaponType);
    }

    private void Update()
    {
        mousePosition = mousePos.action.ReadValue<Vector2>();

        bool usingControllerForInput = PInput.currentControlScheme == "Controller";
        if (usingControllerForInput && mousePosition != Vector2.zero)
        {
            mousePosition += new Vector2(Screen.width / 2f, Screen.height / 2f);

            Aim();

            currentWeapon.Fire();
        }
        else if (shoot.action.IsPressed())
        {
            Aim();

            currentWeapon.Fire();
        }


        if (aim.action.IsPressed())
        {
            Aim();
        }

        if (weaponData != null) PlayerManager.Instance.playerAnimation.animator.SetBool(PLAYER_ANIMATION_PARAMETER.HOLDING_RIGHT.ToString(), aim.action.IsPressed());
    }

    private void Aim()
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
            PoolManager.Instance[currentWeapon.weaponData.type].Release(currentWeapon.gameObject);
        }

        currentWeapon = PoolManager.Instance[(ResourceType)defaultWeaponType].Get().GetComponent<Weapon>();

        DistanceWeaponData weaponData = currentWeapon.weaponData as DistanceWeaponData;

        this.weaponData = weaponData != null ? weaponData : null;

        currentWeapon.transform.SetParent(weaponHolder);
        currentWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, weaponHolder.rotation);
    }

    public void AddDecorator<T>(params object[] args) where T : IAttackBehaviour
    {
        if (currentWeapon == null)
        {
            Debug.LogWarning("Aucune arme équipée pour ajouter un décorateur.");
            return;
        }

        // Préparez la liste des arguments à passer au constructeur du décorateur.
        // Le premier argument doit être le comportement actuel de l'arme.
        List<object> argList = new List<object>();
        argList.Add(currentWeapon.attackBehaviour);
        if (args != null && args.Length > 0)
        {
            argList.AddRange(args);
        }

        // Créez une instance du décorateur T avec le comportement actuel en premier paramètre.
        currentWeapon.attackBehaviour = (T)System.Activator.CreateInstance(typeof(T), argList.ToArray());
    }

    /// <summary>
    /// Enregistre un décorateur de projectile à appliquer sur toutes les balles.
    /// Le décorateur doit implémenter IProjectileBehaviour et posséder un constructeur
    /// dont le premier paramètre est le comportement de base à décorer.
    /// Les paramètres supplémentaires (ex. rayon, dégâts, etc.) sont transmis via args.
    /// </summary>
    public void RegisterBulletDecorator<T>(params object[] args) where T : IProjectileBehaviour
    {
        bulletDecoratorFuncs.Add((baseBehaviour) =>
        {
            List<object> argList = new List<object>();
            // Le premier argument est le comportement existant.
            argList.Add(baseBehaviour);
            if (args != null && args.Length > 0)
            {
                argList.AddRange(args);
            }
            return (T)Activator.CreateInstance(typeof(T), argList.ToArray());
        });
    }
}
