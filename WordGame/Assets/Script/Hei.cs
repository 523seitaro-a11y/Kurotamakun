using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float destroyX = -20f;

    private GameObject target;

    void Start()
    {
        target = GameObject.Find("BG"); // 名前で取得
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (target != null)
        {
            Vector3 pos = transform.position;
            pos.y = target.transform.position.y;
            transform.position = pos;
        }

        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
}