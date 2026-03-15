using UnityEngine;

public class Karakasa : MonoBehaviour
{
    [SerializeField, Header("左へ進む速さ")]
    private float _speed = 3f;

    [SerializeField, Header("このX座標以下で初期位置に戻る")]
    private float _resetX = -10f;

    [SerializeField, Header("停止させたいアニメーション名")]
    private string stopAnimation = "Umbrella.yokoyure";

    private Vector3 _startPosition;
    private Animator _anim;

    void Awake()
    {
        _startPosition = transform.position;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        float currentSpeed = _speed;

        // 特定アニメーション再生中なら速度0
        if (_anim != null)
        {
            AnimatorStateInfo state = _anim.GetCurrentAnimatorStateInfo(0);

            if (state.IsName(stopAnimation))
            {
                currentSpeed = 0f;
            }
        }

        // 左へ移動
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        // 一定のX座標まで行ったら初期位置に戻す
        if (transform.position.x <= _resetX)
        {
            transform.position = _startPosition;
        }
    }
}