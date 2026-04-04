using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Toku : MonoBehaviour
{
    [SerializeField, Header("進行速度")]
    public float TokuMoveSpeed = 1.0f;

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

    public int TokuCount = 0;

    private int _currentIndex;

    void Awake()
    {
    }

    void OnEnable()
    {
        TokuCount = 0;

        _kurotama.SetActive(true);
        
        _currentIndex = 0;

        _baa.SetActive(false);
        _akunin.SetActive(false);
        _girl.SetActive(false);
        _car.SetActive(false);

        StartCoroutine(EventSequence());
    }

    void Update()
    {
        if (_baa.activeSelf)
        {
            TokuMoveSpeed = _baa.GetComponent<Baa>().Speed;
        }
        else if (_akunin.activeSelf)
        {
            TokuMoveSpeed = _akunin.GetComponent<Akunin>().Speed;
        }
        else if (_car.activeSelf)
        {
            TokuMoveSpeed = _car.GetComponent<AnimationAction>().Speed;
        }
        else
        {
            TokuMoveSpeed = 1f;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();

        TokuMoveSpeed = 1f;

        _baa.SetActive(false);
        _akunin.SetActive(false);
        _girl.SetActive(false);
        _car.SetActive(false);
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
        yield return new WaitForSeconds(6.0f);
        _baa.SetActive(false);

        TokuCount++;
    }

    IEnumerator EventB()
    {
        Debug.Log("イベントB");

        _akunin.SetActive(true);
        yield return new WaitForSeconds(4.6f);
        _akunin.SetActive(false);

        TokuCount++;
    }

    IEnumerator EventC()
    {
        Debug.Log("イベントC");

        _girl.SetActive(true);
        _kurotama.SetActive(false);
        yield return new WaitForSeconds(6f);
        _girl.SetActive(false);
        _kurotama.SetActive(true);

        TokuCount++;
    }

    IEnumerator EventD()
    {
        Debug.Log("イベントD");

        _car.SetActive(true);
        _kurotama.SetActive(false);
        yield return new WaitForSeconds(8f);
        _car.SetActive(false);
        _kurotama.SetActive(true);

        TokuCount++;
    }
}