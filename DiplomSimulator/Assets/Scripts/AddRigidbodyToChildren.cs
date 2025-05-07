using System.Diagnostics;
using UnityEngine;

public class AddRigidbodyToChildren : MonoBehaviour
{
    [ContextMenu("Add Rigidbody to Children")]
    void AddRigidbodiesToChildren()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter filter in meshFilters)
        {
            if (filter.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = filter.gameObject.AddComponent<Rigidbody>();
                rb.mass = 200f;
                rb.drag = 0.1f;
                rb.angularDrag = 0.05f;
                rb.useGravity = true;
               
            }
        }
        
    }

    [ContextMenu("Remove Rigidbody from Parent")]
    void RemoveRigidbodyFromParent()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            DestroyImmediate(rb);
        }
       
    }
}