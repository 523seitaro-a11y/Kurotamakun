using UnityEngine;
using System.Collections;

public class RainKatatsumuri : MonoBehaviour
{
    // --- 既存フォーマットの変数 ---
    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 0.1f;
    private Vector3 _startScale;

    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.1f; // カタツムリなので少し控えめに
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;
    private Vector3 _basePos; // 初期位置（移動の基準点）

    // --- カタツムリ専用の変数 ---
    [SerializeField, Header("左に流れる速度")]
    private float _scrollSpeed = 1.5f;
    [SerializeField, Header("ループする左端のX座標")]
    private float _leftLimit = -10f;
    [SerializeField, Header("再出現する右端のX座標")]
    private float _rightStart = 10f;

    void Awake()
    {
        _startScale = transform.localScale;
        _basePos = transform.position;
    }

    void OnEnable()
    {
        // 出現した瞬間に位置を少しリセットしたい場合はここ
        _basePos.x = _rightStart;
        StartCoroutine(RotateAppear());
    }

    void Update()
    {
        MoveLeft(); // 左へ移動
        Float();    // 上下揺れ
    }

    void MoveLeft()
    {
        // 基準位置（basePos）を左へ動かしていく
        _basePos.x -= _scrollSpeed * Time.deltaTime;

        // 画面外（左）まで行ったら右端に戻す（ループ処理）
        if (_basePos.x < _leftLimit)
        {
            _basePos.x = _rightStart;
        }
    }

    void Float()
    {
        // MoveLeftで更新された _basePos.x をベースに上下に揺らす
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = new Vector3(_basePos.x, _basePos.y + y, _basePos.z);
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
    }
}