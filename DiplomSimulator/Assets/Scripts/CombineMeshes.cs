using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    [ContextMenu("Combine Meshes")]
    void Combine()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();
        mf.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // Добавлено
        mf.mesh.CombineMeshes(combine);

        gameObject.AddComponent<MeshRenderer>();
        gameObject.GetComponent<MeshRenderer>().sharedMaterials = GetComponentsInChildren<MeshRenderer>()[0].sharedMaterials;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshFilter>() != null)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}