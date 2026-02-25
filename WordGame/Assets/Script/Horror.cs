using UnityEngine;
using System.Collections;

public class Horror : MonoBehaviour
{
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;
    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 1.0f;
    [SerializeField, Header("表示時間")]
    private float _stayTime = 2.0f;
    [SerializeField, Header("フェードアウト時間")]
    private float _fadeTime = 1.0f;

    [SerializeField, Header("怖い顔")]
    private GameObject _horrorFace;

    private Vector3 _startPos;
    private Vector3 _startScale;
    private SpriteRenderer _renderer;
    private Coroutine _fadeCoroutine;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _startPos = transform.position;
        _startScale = transform.localScale;
        _horrorFace.SetActive(false);
    }

    void Update()
    {
        Float();
    }

    void OnEnable()
    {
        StartCoroutine(RotateAppear());

        SetAlpha(1f);

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeSequence());
    }

    void OnDisable()
    {
        if (_fadeCoroutine != null)
        {
            _horrorFace.SetActive(false);
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
    }
    
    IEnumerator RotateAppear()
    {
        float elapsed = 0f;

        Vector3 startScale = _startScale;

        transform.localScale = new Vector3(0f, startScale.y, startScale.z);

        while (elapsed < _rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _rotationDuration;

            float xScale = Mathf.Lerp(0f, startScale.x, t);
            transform.localScale = new Vector3(xScale, startScale.y, startScale.z);

            yield return null;
        }
    }

    IEnumerator FadeSequence()
    {
        yield return new WaitForSeconds(_stayTime);

        float t = 0f;
        while (t < _fadeTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / _fadeTime);
            SetAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        _horrorFace.SetActive(true);

        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        Color c = _renderer.color;
        c.a = alpha;
        _renderer.color = c;
    }

    void Float()
    {
        if (!_horrorFace.activeSelf)
        {
            float y = Mathf.Sin(Time.time * _speed) * _amplitude;
            transform.position = _startPos + new Vector3(0, y, 0);
        }
    }
}
