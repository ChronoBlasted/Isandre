using System;
using System.Collections.Generic;
using System.Reflection;
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

    /// <summary>
    /// Ajoute ou met à jour un décorateur pour le comportement d'attaque de l'arme.
    /// Si un décorateur du même type existe déjà dans la chaîne, sa méthode Upgrade est appelée.
    /// </summary>
    public void AddDecorator<T>(params object[] args) where T : class, IAttackBehaviour
    {
        if (currentWeapon == null)
        {
            Debug.LogWarning("Aucune arme équipée pour ajouter un décorateur.");
            return;
        }

        // Recherche dans la chaîne de comportements si un décorateur de type T existe déjà
        T existingDecorator = FindDecorator<T>(currentWeapon.attackBehaviour);
        if (existingDecorator != null)
        {
            // Utilisation de la réflexion pour appeler la méthode "Upgrade" sur le décorateur existant
            MethodInfo upgradeMethod = existingDecorator.GetType().GetMethod("Upgrade", BindingFlags.Public | BindingFlags.Instance);
            if (upgradeMethod != null)
            {
                // La méthode Upgrade attend un seul paramètre : un tableau d'objets
                upgradeMethod.Invoke(existingDecorator, new object[] { args });
                Debug.Log("Décorateur " + typeof(T).Name + " mis à jour.");
            }
            else
            {
                Debug.LogWarning("Le décorateur existant n'a pas de méthode Upgrade.");
            }
            return;
        }
        else
        {
            // Aucun décorateur du même type n'a été trouvé, on en ajoute un nouveau
            List<object> argList = new List<object>();
            // Le premier argument est le comportement actuel à décorer
            argList.Add(currentWeapon.attackBehaviour);
            if (args != null && args.Length > 0)
                argList.AddRange(args);
            currentWeapon.attackBehaviour = (T)Activator.CreateInstance(typeof(T), argList.ToArray());
            Debug.Log("Nouveau décorateur ajouté : " + typeof(T).Name);
        }
    }

    /// <summary>
    /// Recherche récursive d'un décorateur de type T dans la chaîne de comportements.
    /// </summary>
    private T FindDecorator<T>(IAttackBehaviour behaviour) where T : class, IAttackBehaviour
    {
        if (behaviour is T t)
            return t;

        // Recherche dans le champ privé "decoratedBehaviour" si présent
        FieldInfo field = behaviour.GetType().GetField("decoratedBehaviour", BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            object inner = field.GetValue(behaviour);
            if (inner is IAttackBehaviour innerBehaviour)
            {
                return FindDecorator<T>(innerBehaviour);
            }
        }
        return null;
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
