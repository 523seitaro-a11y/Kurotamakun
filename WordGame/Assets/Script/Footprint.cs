using UnityEngine;

public class Footprint : MonoBehaviour
{
    private float _speed;

    public void SetSpeed(float speed) { _speed = speed; }

    void Update()
    {
        // 背景の速度に合わせて左へ移動
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        // 画面外（左側）に出たら消去 (数値は調整してください)
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}