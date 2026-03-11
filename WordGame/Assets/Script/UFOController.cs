using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem; // Input System Packageを使用する場合

public class UFO : MonoBehaviour
{
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;

    // 開始時の「ローカル」座標を保存する変数
    private Vector3 _startLocalPos;
    private SpriteRenderer _spriteRenderer;
    private bool _isMoving = false;

    void Awake()
    {
        // ワールド座標ではなく、親から見た位置(LocalPosition)を記録
        _startLocalPos = transform.localPosition;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // 最初は見えないようにする
        _spriteRenderer.enabled = false;
        // 5秒後に表示する
        StartCoroutine(ShowAfterDelay(5.0f));
    }

    void Update()
    {
        // Oキーで移動開始フラグを立てる（Input System用）
        if (Keyboard.current != null && Keyboard.current.oKey.wasPressedThisFrame)
        {
            _isMoving = true;
        }

        // フラグがONの時だけ揺れる
        if (_isMoving)
        {
            Float();
        }
    }

    void Float()
    {
        // ローカル座標のY軸だけをサイン波で動かす
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.localPosition = _startLocalPos + new Vector3(0, y, 0);
    }

    IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spriteRenderer.enabled = true;
    }
}