using UnityEngine;
using System.Collections;

public class UnderGround : MonoBehaviour
{
    //回転アニメーション用の変数
    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 0.1f;
    private Vector3 _startScale;


    //Float用の変数(Float用のコードは上下するアニメーションが不要な場合削除してもOK)
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;
    private Vector3 _startPos;

    [SerializeField, Header("目標角度")]
    private Vector3 _targetEuler = new Vector3(0, 0, 0);
    [SerializeField, Header("向きを変える時間")]
    private float _rotateTime = 5.0f;
    [SerializeField, Header("回転イージング")]
    private AnimationCurve _rotateCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 _startRotate;

    void Awake()//Activeになった瞬間に一度だけ開始される処理
    {
        _startScale = transform.localScale;
        _startPos = transform.position;//Float用
        _startRotate = transform.eulerAngles;
    }

    void Update()//Activeになっている間に繰り返される処理
    {
        Float();//上下するアニメーション
    }

    void OnEnable()//Activeになる度に開始される処理
    {
        StartCoroutine(RotateAppear());
        SetRotate();
        transform.rotation = Quaternion.Euler(_startRotate);

        StartCoroutine(Rotate());
    }

    void OnDisable()//非Activeになった瞬間に開始される処理（コルーチンの停止用）
    {
        StopCoroutine(Rotate());
    }
    
    void SetRotate()
    {
        _startRotate = new Vector3(0, 0, -90);
    }

    IEnumerator RotateAppear()//回転アニメーション
    {
        float elapsed = 0f;

        Vector3 startScale = _startScale;

        transform.localScale = new Vector3( startScale.x, 0f, startScale.z);

        while (elapsed < _rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _rotationDuration;

            float yScale = Mathf.Lerp(0f, startScale.y, t);
            transform.localScale = new Vector3( startScale.x, yScale, startScale.z);

            yield return null;
        }
    }

    IEnumerator Rotate()
{
    Quaternion startRot = Quaternion.Euler(_startRotate);
    Quaternion endRot = Quaternion.Euler(_targetEuler);

    float t = 0f;
    while (t < 1f)
    {
        t += Time.deltaTime / _rotateTime;

        float easedT = _rotateCurve.Evaluate(t);
        transform.rotation = Quaternion.Lerp(startRot, endRot, easedT);

        yield return null;
    }

    transform.rotation = endRot;
}
    void Float()//上下するアニメーション
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }
}
