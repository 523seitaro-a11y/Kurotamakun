using UnityEngine;

public class MoveAfterAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float resetX = -30f;
    [SerializeField] private float startX = 10f;

    public Vector3 addPosition;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= resetX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }

    public void MovePosition()
    {
        transform.position += addPosition;
    }
}