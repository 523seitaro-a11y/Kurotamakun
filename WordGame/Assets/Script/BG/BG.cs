using UnityEngine;

public class InfiniteScrollBG3Manager : MonoBehaviour
{
    [SerializeField,Header("背景3枚（左→右の順で登録）")]
    private Transform[] _backgrounds;

    [SerializeField,Header("スクロール速度")]
    private float _scrollSpeed = 1f;

    private float _width;
    private float _speed = 1f;

    void Start()
    {
        _width = _backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        MainManager manager = FindFirstObjectByType<MainManager>();
        _speed = manager.Speed;

        foreach (Transform bg in _backgrounds)
        {
            bg.Translate(Vector3.left * _scrollSpeed * _speed * Time.deltaTime);
        }

        for (int i = 0; i < _backgrounds.Length; i++)
        {
            if (_backgrounds[i].position.x <= -_width)
            {
                MoveToRightEnd(_backgrounds[i]);
            }
        }
    }

    void MoveToRightEnd(Transform target)
    {
        float rightMostX = GetRightMostX();
        target.position = new Vector3(
            rightMostX + _width,
            target.position.y,
            target.position.z
        );
    }

    float GetRightMostX()
    {
        float maxX = _backgrounds[0].position.x;
        foreach (Transform bg in _backgrounds)
        {
            if (bg.position.x > maxX)
                maxX = bg.position.x;
        }
        return maxX;
    }
}
