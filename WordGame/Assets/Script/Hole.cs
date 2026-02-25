using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField,Header("穴の速度")]
    private float _speed = 4f;

    void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;

        if (transform.position.x <= -15f)
        {
            Destroy(gameObject);
        }
    }
}