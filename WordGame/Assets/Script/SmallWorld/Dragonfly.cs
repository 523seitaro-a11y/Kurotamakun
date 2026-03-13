using UnityEngine;

public class Dragonfly : MonoBehaviour
{
    [SerializeField, Header("右へ進む速さ")]
    private float _rightSpeed = 3f;

    [SerializeField, Header("左へ進む速さ")]
    private float _leftSpeed = 3f;

    [SerializeField, Header("このX座標以上で初期位置に戻る")]
    private float _resetX = 15f;

    [SerializeField, Header("左へ進む時間")]
    private float _leftDuration = 2f;

    [SerializeField, Header("左へ進むまでのインターバル")]
    private float _interval = 5f;

    private Vector3 _startPosition;

    private float _timer;
    private bool _moveLeft = false;

    void Awake()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        // インターバル経過で左移動開始
        if (!_moveLeft && _timer >= _interval)
        {
            _moveLeft = true;
            _timer = 0f;
        }

        // 左移動時間が終わったら右移動に戻る
        if (_moveLeft && _timer >= _leftDuration)
        {
            _moveLeft = false;
            _timer = 0f;
        }

        // 移動
        if (_moveLeft)
        {
            transform.position += Vector3.left * _leftSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * _rightSpeed * Time.deltaTime;
        }

        // 右端に行ったら初期位置に戻る
        if (transform.position.x >= _resetX)
        {
            transform.position = _startPosition;
        }
    }
}