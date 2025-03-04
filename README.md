# ISANDRE - Twin Stick Shooter

## 📜 Sommaire
1. [Introduction](#introduction)  
   - Objectif  
   - Description du jeu  
   - Public cible  
   - Technologies utilisées  
2. [Gameplay et Mécaniques](#gameplay-et-mécaniques)  
   - Contrôles  
   - Système de tir et projectiles  
   - IA des ennemis  
   - Système de spawn  
   - Gestion des collisions  
   - Progression  
3. [Architecture du Code](#architecture-du-code)  
   - Structure du projet  
   - Patrons de conception  
   - Convention de code  
4. [Graphismes et Audio](#graphismes-et-audio)  
   - Assets graphiques  
   - Sound design et gestion audio  
5. [Tools, Tests & CI/CD](#tools-tests--cicd)  
   - Outils de développement  
   - Tests unitaires  
   - Intégration Continue & Déploiement Automatisé  

---

## 1️⃣ Introduction

### 🎯 Objectif  
Développer un **Twin Stick Shooter** jouable **au clavier/souris et à la manette**, avec des armes variées (corps à corps et distance).  
Un projet documenté pour une meilleure évolutivité.

### 🎮 Description du jeu  
Un jeu **d’action rapide** où le joueur affronte des vagues d’ennemis en alternant entre **tir à distance** et **attaques au corps à corps**.  
L’objectif est **d’éliminer les vagues d’ennemis** tout en maîtrisant les déplacements et combats.

### 🏆 Public cible  
- Joueurs de Twin Stick Shooters *(Nuclear Throne, Enter the Gungeon, Dead Cells)*  
- Fans de jeux d’action rapide  
- Joueurs PC et console (16+)

### 🔧 Technologies utilisées  
- **Moteur** : Unity 6  
- **Langage** : C#  
- **Packages** :  
  - DoTween (Animations avancées)  
  - Cinemachine (Caméra dynamique)  
  - TextMesh Pro (Affichage amélioré)  

---

## 2️⃣ Gameplay et Mécaniques  

### 🎮 Contrôles  

#### **Clavier / Souris**  
- **ZQSD / Flèches** → Déplacement  
- **Souris** → Viser  
- **Clic gauche** → Attaque  
- **Clic droit** → Viser  
- **Shift** → Courir  
- **E** → Interagir  

#### **Manette**  
- **Stick gauche** → Déplacement
- **Stick droit** → Viser & Tirer
- **A / LS / RS** → Saut rapide
- **LT / X** → Courir
- **B** → Interagir

### 🔫 Système de tir et projectiles  
- **Tir simple** : Projectile unique  
- **Tir en rafale** : Plusieurs projectiles rapidement  
- **Tir à dispersion** : Shotgun-style  

### 🧠 IA des ennemis  
- **Mêlée** : Attaque rapprochée  
- **Distance** : Tire à distance  
- **Tank** : Ennemi lent mais résistant  
- **NavMesh** pour navigation, **FSM** pour la gestion des états  

### 🏹 Système de spawn  
- **Apparition hors champ**  
- **Progression dynamique de la difficulté** (PV augmentent avec le nombre d'ennemis éliminés)  

### 🎯 Gestion des collisions (Layers)  
- **Player** : Joueur  
- **Enemy** : Ennemis  
- **Bullets** : Projectiles  
- **Ground / Wall** : Sols et murs  

### 📈 Progression  
- Déblocage d’armes en éliminant des ennemis  
- Mini-boss lâchant de nouvelles armes  

---

## 3️⃣ Architecture du Code  

### 📂 Structure du projet  
Organisation en dossiers pour faciliter la navigation dans **Unity** :  
- **Animations** (Animator, Timeline)  
- **Prefabs** (Objets réutilisables)  
- **Scripts** (Code du projet)  
- **Scenes** (Niveaux du jeu)  

### 🏗️ Patrons de conception utilisés  
- **Singleton** → Gestionnaires globaux *(GameManager, UI Manager...)*  
- **State Machine** → IA des ennemis  
- **Component** → Système d’armes modulaire  
- <span style="color: red;">**Decorator** → Système de buff et de modification de nos tirs</span> 

### 🔤 Conventions de nommage  
- **Classes** : `PascalCase` → `WeaponManager`  
- **Variables** : `camelCase` → `playerHealth`  
- **Constantes** : `SNAKE_CASE` → `MAX_HEALTH`  
- **Enums** : `PascalCase` → `WeaponType.MeleeSword`  

---

## 4️⃣ Graphismes et Audio  

### 🎨 Assets graphiques  
- **Mini Arcade** → Modèles 3D  
- **Blaster Kit** → Armes et projectiles  
- **Cartoon FX** → Effets visuels stylisés  

### 🎵 Sound design et gestion audio  
- **YouTube Audio Library** (Musique et effets sonores)  

---

### Ajout de code 
- Creation de decorators :
     - creer un nouveau script qui prend en parent ProjectileDecorator,
     - Ajouter les fonctions constructeur et OnHit()
       ```csharp
       public class ExplosiveProjectileDecorator : ProjectileDecorator
         {
             public float explosionRadius;
             public float explosionDamage;
             public float explosionForce;    // Force de recul appliquée aux ennemis
             public GameObject explosionVFX; // Prefab du VFX à instancier lors de l'explosion
         
             // Le constructeur doit prendre en premier le comportement de base, puis les paramètres.
             public ExplosiveProjectileDecorator(IProjectileBehaviour decoratedBehaviour, float explosionRadius, float explosionDamage, float explosionForce)
                 : base(decoratedBehaviour)
             {
                 this.explosionRadius = explosionRadius;
                 this.explosionDamage = explosionDamage;
                 this.explosionForce = explosionForce;        
             }
         
             public override void OnHit()
             {
                 // Essayer de récupérer le projectile depuis le comportement de base.
                 BasicProjectileBehaviour baseBehaviour = decoratedBehaviour as BasicProjectileBehaviour;
                
                   //Do Something
         
                 // Enfin, appel du comportement de base (libération du projectile, etc.)
                 base.OnHit();
             }
         }

       ```
     - Prendre exemple sur ExplosiveProjectileDecorator pour la nomenclature
     - Creer un SO PowerUp, qui va ajouter le decorator
       ```csharp
          [CreateAssetMenu(fileName = "Scriptable Objects", menuName = "PowerUp/Explosive", order = 1)]
         public class Explosive : PowerUp
         {
             [SerializeField] private float explosionRadius = 2f;
             [SerializeField] private float explosionDamage = 5f;
             [SerializeField] private float explosionForce = 5f;
             public override void OnUse()
             {
                 base.OnUse();
                 PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ExplosiveProjectileDecorator>(explosionRadius, explosionDamage, explosionForce);
             }
         }

       ```
       
     - ex : PlayerManager.Instance.playerWeapon.RegisterBulletDecorator<ExplosiveProjectileDecorator>(explosionRadius, explosionDamage, explosionForce);
     - Ajouter le nouveau SO au SO ChestLvlUpData, pour qu'il puisse apparaitre en jeu
  

## 5️⃣ Tools, Tests & CI/CD  

### 🛠️ Outils de développement  
- **Moteur** : Unity 6  
- **IDE** : Visual Studio / Rider  
- **Versioning** : Git + GitHub  
- **Gestion projet** : Trello  

### ✅ Tests unitaires  
- **Player** : Déplacements, Dash, Vie  
- **Armes** : Poing, Pistolet, Fusil à pompe  

### 🚀 CI/CD (Intégration Continue & Déploiement)  
- **GitHub Actions** pour automatiser :  
  1. **Tests unitaires**  
  2. **Compilation**  
  3. **Build & Déploiement**  

---

## 🎯 Conclusion  
ISANDRE est un **Twin Stick Shooter dynamique** avec une **IA avancée, un système de progression équilibré et un gameplay nerveux**.  
Le projet est documenté pour **faciliter les contributions** et **améliorer l’expérience des développeurs**.  

📌 **Projet en cours de développement !** 
