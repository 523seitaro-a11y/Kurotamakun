using UnityEngine;
using System.Collections;

public class Mizu : MonoBehaviour
{
    [SerializeField, Header("消えるまでの時間")]
    private float _fadeDuration = 0.2f;

    [SerializeField, Header("広がる大きさ")]
    private float _targetScaleMultiplier = 1.5f;

    private bool _isHitting = false; // 二重衝突防止
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 傘に当たった、かつ、まだ消え始めていない場合
        if (collision.gameObject.CompareTag("Kasa") && !_isHitting)
        {
            _isHitting = true;
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    IEnumerator FadeOutAndDestroy()
    {
        // 物理挙動を止める（その場に留まって消える演出のため）
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * _targetScaleMultiplier;
        Color startColor = _spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsed = 0f;
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _fadeDuration;

            // 大きさを変える
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            // 透明度を変える
            _spriteRenderer.color = Color.Lerp(startColor, targetColor, t);

            yield return null;
        }

        Destroy(gameObject);
    }
}