using UnityEngine;
using System.Collections;

public class Tsumi : MonoBehaviour
{
    [SerializeField, Header("進行速度")]
    public float TsumiMoveSpeed = 1.0f;

    [SerializeField, Header("イベント発生間隔")]
    private float _interval = 5f;

    [SerializeField, Header("くろたまくん")]
    private GameObject _kurotama;

    [SerializeField, Header("婆")]
    private GameObject _baa;

    [SerializeField, Header("悪人")]
    private GameObject _akunin;

    [SerializeField, Header("女の子")]
    private GameObject _girl;

    [SerializeField, Header("車")]
    private GameObject _car;

    public int TsumiCount = 0;

    private int _currentIndex;

    void OnEnable()
    {
        TsumiCount = 0;
        _currentIndex = 0;

        _kurotama.SetActive(true);

        _baa.SetActive(false);
        _girl.SetActive(false);
        _car.SetActive(false);

        StartCoroutine(EventSequence());
    }

    void Update()
    {
        if (_baa.activeSelf)
        {
            TsumiMoveSpeed = _baa.GetComponent<AnimationAction>().Speed;
        }
        else if (_girl.activeSelf)
        {
            TsumiMoveSpeed = _girl.GetComponent<AnimationAction>().Speed;
        }
        else if (_car.activeSelf)
        {
            TsumiMoveSpeed = _car.GetComponent<AnimationAction>().Speed;
        }
        else
        {
            TsumiMoveSpeed = 1f;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        TsumiMoveSpeed = 1f;
    }

    IEnumerator EventSequence()
    {
        while (_currentIndex < 4)
        {
            yield return new WaitForSeconds(_interval);

            switch (_currentIndex)
            {
                case 0:
                    yield return StartCoroutine(EventA());
                    break;

                case 1:
                    yield return StartCoroutine(EventB());
                    break;

                case 2:
                    yield return StartCoroutine(EventC());
                    break;

                case 3:
                    yield return StartCoroutine(EventD());
                    break;
            }

            _currentIndex++;
        }

        Debug.Log("すべてのイベント終了");
    }

    IEnumerator EventA()
    {
        Debug.Log("イベントA");

        _baa.SetActive(true);
        yield return new WaitForSeconds(5.6f);
        _baa.SetActive(false);

        TsumiCount++;
    }

    IEnumerator EventB()
    {
        Debug.Log("イベントB");

        _girl.SetActive(true);
        _kurotama.SetActive(false);

        yield return new WaitForSeconds(8f);

        _girl.SetActive(false);
        _kurotama.SetActive(true);

        TsumiCount++;
    }

    IEnumerator EventC()
    {
        Debug.Log("イベントC");

        _car.SetActive(true);
        _kurotama.SetActive(false);

        yield return new WaitForSeconds(8f);

        _car.SetActive(false);
        _kurotama.SetActive(false);

        TsumiCount++;
    }

    IEnumerator EventD()
    {
        Debug.Log("イベントD");

        yield return new WaitForSeconds(4.6f);

        TsumiCount++;
    }
}