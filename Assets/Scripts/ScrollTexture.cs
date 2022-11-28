using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] float scrollX, scrollY;

    MeshRenderer MeshRenderer;
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        MeshRenderer.material.mainTextureOffset += new Vector2(scrollX, scrollY);
    }
}
