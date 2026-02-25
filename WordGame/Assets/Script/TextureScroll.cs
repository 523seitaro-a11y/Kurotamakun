using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0f);
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}