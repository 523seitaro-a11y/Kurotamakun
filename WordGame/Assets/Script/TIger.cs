using UnityEngine;
using System.Collections;

public class Tiger : MonoBehaviour
{
    //��]�A�j���[�V�����p�̕ϐ�
    [SerializeField, Header("��]����")]
    private float _rotationDuration = 0.1f;
    private Vector3 _startScale;

    //Float�p�̕ϐ�(Float�p�̃R�[�h�͏㉺����A�j���[�V�������s�v�ȏꍇ�폜���Ă�OK)
    [SerializeField, Header("�㉺�̕�")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("�h��鑬��")]
    private float _speed = 1.0f;
    private Vector3 _startPos;

    //���x
    [SerializeField, Header("���x")]
    public float speed = 5.0f;

    [SerializeField, Header("木")] 
    private GameObject _tree;
    [SerializeField, Header("竹")] 
    private GameObject _bamboo;

    void Awake()//Active�ɂȂ����u�ԂɈ�x�����J�n����鏈��
    {
        _startScale = transform.localScale;
        _startPos = transform.position;//Float�p

    }

    void Update()//Active�ɂȂ��Ă���ԂɌJ��Ԃ���鏈��
    {
        Float();//�㉺����A�j���[�V����
    }

    void OnEnable()//Active�ɂȂ�x�ɊJ�n����鏈��
    {
        StartCoroutine(RotateAppear());
        _tree.SetActive(false);
        _bamboo.SetActive(true);
    }

    void OnDisable()//��Active�ɂȂ����u�ԂɊJ�n����鏈���i�R���[�`���̒�~�p�j
    {
        _tree.SetActive(true);
        _bamboo.SetActive(false);
    }


    IEnumerator RotateAppear()//��]�A�j���[�V����
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
   
    void Float()//�㉺����A�j���[�V����
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }
}
