using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
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


    void Awake()//Activeになった瞬間に一度だけ開始される処理
    {
        _startScale = transform.localScale;
        _startPos = transform.position;//Float用

    }

    void Update()//Activeになっている間に繰り返される処理
    {
        Float();//上下するアニメーション

    }

    void OnEnable()//Activeになる度に開始される処理
    {
        StartCoroutine(RotateAppear());

    }

    void OnDisable()//非Activeになった瞬間に開始される処理（コルーチンの停止用）
    {
        
    }


    IEnumerator RotateAppear()//回転アニメーション
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

    void Float()//上下するアニメーション
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }
}
