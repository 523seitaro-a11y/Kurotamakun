using UnityEngine;

public class MoveRightReset : MonoBehaviour
{
    [SerializeField, Header("右へ進む速さ")]
    private float _speed = 3f;

    [SerializeField, Header("このX座標以上で初期位置に戻る")]
    private float _resetX = 10f;

    private Vector3 _startPosition;

    void Awake()
    {
        // 最初の位置を保存
        _startPosition = transform.position;
    }

    void Update()
    {
        // 右へ移動
        transform.position += Vector3.right * _speed * Time.deltaTime;

        // 一定のX座標まで行ったら初期位置に戻す
        if (transform.position.x >= _resetX)
        {
            transform.position = _startPosition;
        }
    }
}