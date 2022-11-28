using UnityEngine;

public class OscillateTexture: MonoBehaviour
{
    [SerializeField] float oscillateRangeX, oscillateRangeY;

    Vector2 initalOffset;

    MeshRenderer meshRenderer;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initalOffset = meshRenderer.material.mainTextureOffset;
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset = initalOffset + new Vector2(oscillateRangeX, oscillateRangeY) * Mathf.Sin(Time.time);
    }
}