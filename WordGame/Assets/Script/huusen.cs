using UnityEngine;

public class huusen : MonoBehaviour
{
    public float moveSpeed = 3f; // 上昇スピード
    public float sideRange = 0.5f; // 左右のゆらゆら幅
    public float sideSpeed = 2f; // ゆらゆらの速さ

    private float _randomOffset;

    void Start()
    {
        // 動きに個性を出すためのランダム値
        _randomOffset = Random.Range(0f, 10f);
        // 5秒経ったら自動で削除（メモリ節約のため）
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // 上に移動
        float x = Mathf.Sin(Time.time * sideSpeed + _randomOffset) * sideRange;
        transform.Translate(new Vector3(x * Time.deltaTime, moveSpeed * Time.deltaTime, 0));
    }
}