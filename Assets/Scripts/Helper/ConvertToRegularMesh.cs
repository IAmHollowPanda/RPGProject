using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConvertToRegularMesh : MonoBehaviour
{
    [ContextMenu("Convert to regular mesh")]
    void Convert(){
        SkinnedMeshRenderer skinnedMeshrenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedMeshrenderer.sharedMesh;
        meshRenderer.sharedMaterials = skinnedMeshrenderer.sharedMaterials;

        DestroyImmediate(skinnedMeshrenderer);
        DestroyImmediate(this);
    }
}
