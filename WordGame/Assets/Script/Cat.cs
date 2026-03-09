using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour
{
    [SerializeField, Header("回転時間")]
    private float _rotationDuration = 0.1f;

    [SerializeField, Header("移動速度")]
    private float _speed = 2.0f;

    public bool AfterJump = false;

    private Vector3 _startScale;

    private Animator _anim;

    private Coroutine _jumpCoroutine;

    void Awake()
    {
        _startScale = transform.localScale;
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
    }

    void OnDisable()
    {
        if (_jumpCoroutine != null)
        {
            StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = null;
        }
        AfterJump = false;
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
        _anim.SetTrigger("Jump");
    }
}