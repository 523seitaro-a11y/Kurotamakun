using UnityEngine;
using System.Collections;

public class Tsumi : MonoBehaviour
{
    //Float用の変数(Float用のコードは上下するアニメーションが不要な場合削除してもOK)
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;
    private Vector3 _startPos;


    void Awake()//Activeになった瞬間に一度だけ開始される処理
    {
        _startPos = transform.position;//Float用
    }

    void Update()//Activeになっている間に繰り返される処理
    {
        Float();//上下するアニメーション
    }

    void OnEnable()//Activeになる度に開始される処理
    {

    }

    void OnDisable()//非Activeになった瞬間に開始される処理（コルーチンの停止用）
    {
        
    }

    void Float()//上下するアニメーション
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }
}
