using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour
{
    [SerializeField, Header("瞬きオブジェクト")]
    private GameObject _blinkObject;
    [SerializeField, Header("瞬きの間隔")]
    private float _blinkInterval = 5.0f;
    [SerializeField, Header("瞬きの長さ")]
    private float _blinkLength = 0.2f;

    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;

    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 1.0f;

    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        Float();
    }

    void OnEnable()
    {
        transform.localScale = Vector3.one;
        StartCoroutine(Blink());
    }

    void Float()
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(_blinkInterval);
            _blinkObject.SetActive(true);
            yield return new WaitForSeconds(_blinkLength);
            _blinkObject.SetActive(false);
        }
    }

    public IEnumerator RotateDisappear()
    {
        float elapsed = 0f;

        Vector3 startScale = transform.localScale;

        while (elapsed < _rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _rotationDuration);

            float xScale = Mathf.Lerp(startScale.x, 0f, t);
            transform.localScale = new Vector3(xScale, startScale.y, startScale.z);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
