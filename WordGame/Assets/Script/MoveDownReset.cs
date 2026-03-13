using UnityEngine;

public class MoveDownReset : MonoBehaviour
{
    [SerializeField] private float speed = 2f;      // 移動速度
    [SerializeField] private float resetX = -10f;   // このXに達したら戻る
    [SerializeField] private float startX = 10f;    // 戻るX座標

    void Update()
    {
        // 左に移動
        transform.position += Vector3.left * speed * Time.deltaTime;

        // 指定位置に到達したら戻す
        if (transform.position.x <= resetX)
        {
            Vector3 pos = transform.position;
            pos.x = startX;
            transform.position = pos;
        }
    }
}