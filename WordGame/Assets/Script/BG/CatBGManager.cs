using UnityEngine;

public class CatBGManager : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField, Header("移動スピード")]
    private float _time = 5.0f;

    private float _count = 0f;

    void Update()
    {
        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            _count = 0f;
        }
        else
        {
            _count += Time.deltaTime * _time;
            _count = Mathf.Clamp(_count, 0f, 10f);
        }
        transform.position = new Vector3(0f, _count, -10f);
    }
}
