using UnityEngine;
using System.Collections;

public class Bone : MonoBehaviour
{
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;

    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;

    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 0.1f;

    [SerializeField, Header("動かす子オブジェクト")]
    private Transform _child;

    [SerializeField, Header("子オブジェクトの初期位置")]
    private Vector3 _childStartLocalPos = new Vector3(-8f, -1f, 0f);

    [SerializeField, Header("移動距離")]
    private float _moveDistance = 1f;

    [SerializeField, Header("移動速度")]
    private float _moveSpeed = 1f;

    [SerializeField, Header("停止時間")]
    private float _waitTime = 2f;

    [SerializeField] private GameObject childObject;

    private Vector3 _startPos;
    private Vector3 _startScale;

    private Coroutine _moveCoroutine;
    private float _childStartX;

    void Awake()
    {
        _startScale = transform.localScale;
        _startPos = transform.position;

        if (_child != null)
        {
            _childStartX = _childStartLocalPos.x; // 左端
        }
    }

    void Update()
    {
        Float();
    }

    void OnEnable()
    {
        childObject.SetActive(false);
        StartCoroutine(RotateAppear());

        if (_child != null)
        {
            _child.localPosition = new Vector3(_childStartX, _child.localPosition.y, _child.localPosition.z);
            StartCoroutine(MoveChild());
        }
    }

    void OnDisable()
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        if (_child != null)
        {
            _child.localPosition = _childStartLocalPos;
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

        transform.localScale = startScale;

        childObject.SetActive(true);
    }

    void Float()
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }

    IEnumerator MoveChild()
    {
        while (true)
        {
            // 左端 → 右へ移動
            yield return StartCoroutine(MoveTo(_childStartX + _moveDistance));

            yield return new WaitForSeconds(_waitTime);

            // 右 → 左端へ戻る
            yield return StartCoroutine(MoveTo(_childStartX));

            yield return new WaitForSeconds(_waitTime);
        }
    }
    IEnumerator MoveTo(float targetX)
    {
        while (Mathf.Abs(_child.localPosition.x - targetX) > 0.01f)
        {
            float x = Mathf.MoveTowards(_child.localPosition.x, targetX, _moveSpeed * Time.deltaTime);
            _child.localPosition = new Vector3(x, _child.localPosition.y, _child.localPosition.z);
            yield return null;
        }

        _child.localPosition = new Vector3(targetX, _child.localPosition.y, _child.localPosition.z);
    }
}