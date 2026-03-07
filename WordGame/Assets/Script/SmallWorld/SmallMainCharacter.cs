using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SmallMainChacter : MonoBehaviour
{
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;

    private Vector3 _startPos;

    private Vector3 _shrinkPos;

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
        transform.localScale = new Vector3(10f,10f,10f);
        StartCoroutine(Shrink());
    }

    void OnDisable()//非Activeになった瞬間に開始される処理（コルーチンの停止用）
    {
        StopCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(1.0f,1.0f,1.0f);

        Vector3 startPosition = new Vector3(0f,12.0f,0f);
        Vector3 targetPosition = Vector3.zero;

        float elapsed = 0f;
        float time = 1f;

        while (elapsed < time)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / time);
            _shrinkPos = Vector3.Lerp(startPosition, targetPosition, elapsed / time);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    void Float()//上下するアニメーション
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + _shrinkPos + new Vector3(0, y, 0);
    }
}