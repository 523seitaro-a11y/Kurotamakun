using UnityEngine;

public class MoveLeftReset : MonoBehaviour
{
    [SerializeField, Header("左へ進む速さ")]
    private float _speed = 3f;

    [SerializeField, Header("このX座標以下で初期位置に戻る")]
    private float _resetX = -10f;

    private Vector3 _startPosition;

    void Awake()
    {
        // 最初の位置を保存
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        // 左へ移動
        transform.localPosition += Vector3.left * _speed * Time.deltaTime;

        // 一定のX座標まで行ったら初期位置に戻す
        if (transform.localPosition.x <= _resetX)
        {
            transform.localPosition = _startPosition;
        }
    }
}