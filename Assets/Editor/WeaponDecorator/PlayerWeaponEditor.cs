using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class BulletDecoratorWindow : EditorWindow
{
    private int numberOfShots = 3;
    private float angleSpread = 15f;

    private int burstCount = 3;
    private float timeBetweenShots = 0.2f;

    private float explosionRadius = 2f;
    private float explosionDamage = 5f;
    private float explosionForce = 5f;

    private float chainRadius = 2f;
    private int chainNumber = 2;
    private int pierceCount = 2;

    private bool explosiveFoldout = true;
    private bool piercingFoldout = false;
    private bool chainFoldout = false;
    private bool burstFoldout = false;
    private bool multiFoldout = false;

    private void OnEnable()
    {
        
    }

    [MenuItem("Window/Bullet Decorator Manager")]
    public static void ShowWindow()
    {
        GetWindow<BulletDecoratorWindow>("Bullet Decorator Manager");
    }

    private void OnGUI()
    {
        GUIStyle DecoratorBoxStyle = new GUIStyle(GUI.skin.box)
        {
            fontSize = 16,
            normal = { textColor = Color.white },
            alignment = TextAnchor.UpperLeft,
            padding = new RectOffset(10, 10, 10, 10),
            margin = new RectOffset(5, 5, 5, 5)
        };

        GUIStyle ParamsBoxStyle = new GUIStyle(GUI.skin.box)
        {
            fontSize = 16,
            normal = { textColor = Color.yellow },
            alignment = TextAnchor.UpperLeft,
            padding = new RectOffset(15, 15, 15, 15),
            margin = new RectOffset(7, 7, 7, 7)
        };

        // On s'assure que l'instance du PlayerWeapon est présente dans la scène.
        if (PlayerManager.Instance.playerWeapon == null)
        {
            EditorGUILayout.HelpBox("Aucune instance de PlayerWeapon trouvée dans la scène.", MessageType.Error);
            return;
        }

        GUILayout.Label("Ajouter des Decorators d'armes", EditorStyles.boldLabel);

        

        GUILayout.BeginVertical(DecoratorBoxStyle);
        multiFoldout = EditorGUILayout.Foldout(multiFoldout, "MultiShoot Decorator");
        if (multiFoldout)
        {
            GUILayout.BeginVertical(ParamsBoxStyle);
            numberOfShots = EditorGUILayout.IntField("number of bullet", numberOfShots);
            angleSpread = EditorGUILayout.FloatField("Spread", angleSpread);
            GUILayout.EndVertical();
        }
        // Section MultiShot Decorator
        if (GUILayout.Button("Appliquer MultiShot Decorator", GUILayout.Height(25)))
        {
            PlayerManager.Instance.playerWeapon.AddDecorator<MultiShotDecorator>(numberOfShots, angleSpread);
            Debug.Log("MultiShot Decorator ajouté.");
        }
        GUILayout.EndVertical();
        EditorGUILayout.Space();


        GUILayout.BeginVertical(DecoratorBoxStyle);
        burstFoldout = EditorGUILayout.Foldout(burstFoldout, "Raffale Decorator");
        if (burstFoldout)
        {
            GUILayout.BeginVertical(ParamsBoxStyle);
            burstCount = EditorGUILayout.IntField("burst Count", burstCount);
            timeBetweenShots = EditorGUILayout.FloatField("time Between Shots", timeBetweenShots);
            GUILayout.EndVertical();
        }
        // Section MultiShot Decorator
        if (GUILayout.Button("Appliquer Raffale Decorator", GUILayout.Height(25)))
        {
            PlayerManager.Instance.playerWeapon.AddDecorator<BurstFireDecorator>(burstCount, timeBetweenShots);
            Debug.Log("MultiShot Decorator ajouté.");
        }
        GUILayout.EndVertical();
        EditorGUILayout.Space();


        DrawSeparator(Color.gray);

        GUILayout.Label("Ajouter des Decorators de Projectile", EditorStyles.boldLabel);
        GUILayout.BeginVertical(DecoratorBoxStyle);
        explosiveFoldout = EditorGUILayout.Foldout(explosiveFoldout, "Explosive Decorator");
        if (explosiveFoldout)
        {            
            GUILayout.BeginVertical(ParamsBoxStyle);
            // Section Explosive Projectile Decorator
            explosionRadius = EditorGUILayout.FloatField("Explosion Radius", explosionRadius);
            explosionDamage = EditorGUILayout.FloatField("Explosion Damage", explosionDamage);
            explosionForce = EditorGUILayout.FloatField("Explosion Force", explosionForce);
            GUILayout.EndVertical();
            DrawSeparator(Color.grey, 1f, 5);
            
        }
        if (GUILayout.Button("Ajouter Explosive Bullet", GUILayout.Height(25)))
        {
            PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ExplosiveProjectileDecorator>(explosionRadius, explosionDamage, explosionForce);
            Debug.Log("Explosive Bullet Decorator ajouté.");
        }
        GUILayout.EndVertical();
        EditorGUILayout.Space();

        GUILayout.BeginVertical(DecoratorBoxStyle);
        chainFoldout = EditorGUILayout.Foldout(chainFoldout, "Chain Decorator");
        if (chainFoldout)
        {
            GUILayout.BeginVertical(ParamsBoxStyle);
            // Section Chain Projectile Decorator
            chainRadius = EditorGUILayout.FloatField("Chain Radius", chainRadius);
            chainNumber = EditorGUILayout.IntField("Chain Number", chainNumber);
            GUILayout.EndVertical();
            DrawSeparator(Color.grey, 1f, 5);            
        }
        if (GUILayout.Button("Ajouter Chain Bullet", GUILayout.Height(25)))
        {
            PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ChainProjectileDecorator>(chainRadius, chainNumber);
            Debug.Log("Chain Bullet Decorator ajouté.");
        }
        GUILayout.EndVertical();
        EditorGUILayout.Space();



        ///

        GUILayout.BeginVertical(DecoratorBoxStyle);
        piercingFoldout = EditorGUILayout.Foldout(piercingFoldout, "piercing Decorator");
        if (piercingFoldout)
        {
            GUILayout.BeginVertical(ParamsBoxStyle);
            // Section Piercing Projectile Decorator
            pierceCount = EditorGUILayout.IntField("Pierce Count", pierceCount);
            GUILayout.EndVertical();
            DrawSeparator(Color.grey, 1f, 5);
        }
        if (GUILayout.Button("Ajouter Pierce Bullet",  GUILayout.Height(25)))
        {
            PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<PiercingProjectileDecorator>(pierceCount);
            Debug.Log("Piercing Bullet Decorator ajouté.");
        }
        GUILayout.EndVertical();        
    }
    private void DrawSeparator(Color color, float thickness = 2f, float padding = 10f)
    {
        GUILayout.Space(padding);
        Rect rect = EditorGUILayout.GetControlRect(false, thickness);
        EditorGUI.DrawRect(rect, color);
        GUILayout.Space(padding);
    }


}