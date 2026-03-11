using UnityEngine;

public class EightMove : MonoBehaviour
{
    [Header("動き")]
    public float speed = 2f;
    public float width = 4f;
    public float height = 2f;

    [Header("タイミングのランダム幅")]
    public float randomOffsetRange = 6f;

    private Vector3 startPos;
    private float t;

    void Start()
    {
        // 子オブジェクトでもズレないようにする
        startPos = transform.localPosition;

        // オブジェクトごとに開始タイミングをランダムにする
        t = Random.Range(0f, randomOffsetRange);
    }

    void Update()
    {
        t += Time.deltaTime * speed;

        float x = Mathf.Sin(t) * width;
        float y = Mathf.Sin(2 * t) * height;

        transform.localPosition = startPos + new Vector3(x, y, 0);
    }
}