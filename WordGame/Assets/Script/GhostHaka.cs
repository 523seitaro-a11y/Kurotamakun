using UnityEngine;
using System.Collections;

public class GhostHaka : MonoBehaviour
{
    private Vector3 _basePos;

    [SerializeField, Header("ç∂Ç…ó¨ÇÍÇÈë¨ìx")]
    private float _scrollSpeed = 2.0f;
    [SerializeField, Header("çÌèúÇ∑ÇÈç∂í[ÇÃXç¿ïW")]
    private float _leftLimit = -12f;

    void Awake()
    {
        _basePos = transform.position;
    }

    void Update()
    {
        _basePos.x -= _scrollSpeed * Time.deltaTime;
        transform.position = _basePos;

        if (_basePos.x < _leftLimit)
        {
            Destroy(gameObject);
        }
    }
}