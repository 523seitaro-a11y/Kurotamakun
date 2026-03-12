using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour
{
    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 0.1f;

    [SerializeField, Header("移動速度")]
    private float _speed = 2.0f;

    [SerializeField] private GameObject prefab;   // 生成するPrefab
    [SerializeField] private float interval = 3f; // 生成間隔

    public bool AfterJump = false;

    private Vector3 _startScale;
    private Vector3 _startPos;

    private Animator _anim;

    private Coroutine _jumpCoroutine;

    void Awake()
    {
        _startScale = transform.localScale;
        _startPos = transform.localPosition;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo state = _anim.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Cat_arm|Cat_jump"))
        {
            float t = state.normalizedTime % 1f;

            if (t < 0.5f)
            {
                transform.position += Vector3.left * _speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * _speed * Time.deltaTime;
            }
        }
    }

    void OnEnable()
    {
        StartCoroutine(RotateAppear());

        if (_jumpCoroutine == null)
        {
            _jumpCoroutine = StartCoroutine(Jump());
        }

         StartCoroutine(SpawnLoop());
    }

    void OnDisable()
    {
        if (_jumpCoroutine != null)
        {
            StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = null;
        }
        AfterJump = false;

        StopAllCoroutines();

        transform.localPosition = _startPos;
    }

    IEnumerator RotateAppear()
    {
        float elapsed = 0f;
        Vector3 startScale = _startScale;

        transform.localScale = new Vector3(0f, startScale.y, startScale.z);

        while (elapsed < _rotationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / _rotationDuration;

            float xScale = Mathf.Lerp(0f, startScale.x, t);

            transform.localScale = new Vector3(xScale, startScale.y, startScale.z);

            yield return null;
        }
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(3.5f);
        AfterJump = true;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Instantiate(prefab,new Vector3(13, 0, 1), Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}