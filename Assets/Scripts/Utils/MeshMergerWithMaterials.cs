using UnityEngine;
using System.Collections.Generic;

public class MeshMergerWithMaterials : MonoBehaviour
{
    // Méthode pour lancer la fusion des meshes
    public void CombineMeshes()
    {
        // Récupère tous les MeshFilter enfants
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Dictionnaire associant chaque matériau à une liste de CombineInstance
        Dictionary<Material, List<CombineInstance>> materialToCombineInstances = new Dictionary<Material, List<CombineInstance>>();

        // Pour ramener les meshes dans l'espace local de l'objet de base,
        // on utilise transform.worldToLocalMatrix
        Matrix4x4 parentInverseMatrix = transform.worldToLocalMatrix;

        foreach (MeshFilter mf in meshFilters)
        {
            // On ignore l'objet combiné (s'il existe déjà) pour éviter de le réinclure
            if (mf.gameObject == this.gameObject)
                continue;

            MeshRenderer mr = mf.GetComponent<MeshRenderer>();
            if (mr == null)
                continue;

            Mesh mesh = mf.sharedMesh;
            Material[] materials = mr.sharedMaterials;

            // Pour chaque sous-mesh, on associe la CombineInstance au matériau correspondant.
            // On convertit la matrice locale de l'objet en matrice relative au parent.
            for (int subMesh = 0; subMesh < mesh.subMeshCount; subMesh++)
            {
                if (subMesh >= materials.Length)
                    continue;

                Material mat = materials[subMesh];
                if (!materialToCombineInstances.ContainsKey(mat))
                    materialToCombineInstances[mat] = new List<CombineInstance>();

                CombineInstance ci = new CombineInstance();
                ci.mesh = mesh;
                ci.subMeshIndex = subMesh;
                // Transformation : on ramène la matrice du mesh dans l'espace local du parent
                ci.transform = parentInverseMatrix * mf.transform.localToWorldMatrix;
                materialToCombineInstances[mat].Add(ci);
            }
        }

        // Pour chaque matériau, on combine les instances en un sous-mesh
        List<Mesh> subMeshes = new List<Mesh>();
        List<Material> finalMaterials = new List<Material>();
        foreach (var kvp in materialToCombineInstances)
        {
            Material mat = kvp.Key;
            List<CombineInstance> combineInstances = kvp.Value;
            Mesh subMesh = new Mesh();
            // Utilisation de UInt32 pour dépasser la limite des 65 535 vertices
            subMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            subMesh.CombineMeshes(combineInstances.ToArray(), true, true);
            subMeshes.Add(subMesh);
            finalMaterials.Add(mat);
        }

        // Crée une CombineInstance pour chaque sous-mesh combiné
        List<CombineInstance> finalCombine = new List<CombineInstance>();
        foreach (Mesh m in subMeshes)
        {
            CombineInstance ci = new CombineInstance();
            ci.mesh = m;
            ci.subMeshIndex = 0;
            ci.transform = Matrix4x4.identity; // Les meshes sont déjà dans l'espace local
            finalCombine.Add(ci);
        }

        // Crée le mesh final avec plusieurs sous-meshes
        Mesh finalMesh = new Mesh();
        finalMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        finalMesh.CombineMeshes(finalCombine.ToArray(), false, true);

        // Crée un nouvel objet pour le mesh combiné et le place en enfant de l'objet de base
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(transform, false);
        // La position, la rotation et l'échelle restent ainsi alignées sur le pivot de l'objet de base
        combinedObject.transform.localPosition = Vector3.zero;
        combinedObject.transform.localRotation = Quaternion.identity;
        combinedObject.transform.localScale = Vector3.one;

        MeshFilter mfCombined = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer mrCombined = combinedObject.AddComponent<MeshRenderer>();
        mfCombined.mesh = finalMesh;
        mrCombined.materials = finalMaterials.ToArray();

        // Cache les objets d'origine
        foreach (MeshFilter mf in meshFilters)
        {
            mf.gameObject.SetActive(false);
        }

        Debug.Log("Fusion terminée, " + finalMesh.subMeshCount + " sous-mesh(es) conservé(s) pour les matériaux.");
    }

    private void Start()
    {
        CombineMeshes();
    }
}
