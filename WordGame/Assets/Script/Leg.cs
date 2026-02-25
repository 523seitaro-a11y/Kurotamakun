using UnityEngine;
using System.Collections;

public class Leg : MonoBehaviour
{
    //回転アニメーション用の変数
    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 0.1f;
    private Vector3 _startScale;

    [SerializeField, Header("速度")]
    public float speed = 1.5f;

    void Awake()//Activeになった瞬間に一度だけ開始される処理
    {
        _startScale = transform.localScale;
    }

    void Update()//Activeになっている間に繰り返される処理
    {
        
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
}
