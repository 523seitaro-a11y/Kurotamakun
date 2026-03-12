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

    public int TokuCount = 0;

    private List<int> _events;
    private int _currentIndex;
    private float _timer;

    void Awake()//Activeになった瞬間に一度だけ開始される処理
    {

    }

    void OnEnable()//Activeになる度に開始される処理
    {
        ResetEvents();
        TokuCount = 0;

        _baa.SetActive(false);
        _akunin.SetActive(false);
        _girl.SetActive(false);
        NextEvent();
        _timer = 0f;
    }

void Update()//Activeになっている間に繰り返される処理
    {
        if (_events == null || _currentIndex >= _events.Count)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer >= _interval)
        {
            _timer = 0f;
            NextEvent();
            TokuCount += 1;
        }

        if (_baa.activeSelf)
        {
            TokuMoveSpeed = _baa.GetComponent<Baa>().Speed;
        }
        else if (_akunin.activeSelf)
        {
            TokuMoveSpeed = _akunin.GetComponent<Akunin>().Speed;
        }
        else
        {
            TokuMoveSpeed = 1f;
        }
    }

    void OnDisable()//非Activeになった瞬間に開始される処理（コルーチンの停止用）
    {
        StopAllCoroutines();
        TokuMoveSpeed = 1f;
    }

    void ResetEvents()
    {
        _events = new List<int>() { 0, 1, 2, 3, 4};
        _currentIndex = 0;
        _timer = 0f;

        Shuffle(_events);
    }


    public void NextEvent()
    {
        if (_currentIndex >= _events.Count)
        {
            Debug.Log("すべてのイベント終了");
            return;
        }

        int eventID = _events[_currentIndex];
        _currentIndex++;

        switch (eventID)
        {
            case 0:
                StartCoroutine(EventA());
                break;

            case 1:
                StartCoroutine(EventB());
                break;

            case 2:
                StartCoroutine(EventC());
                break;

            case 3:
                StartCoroutine(EventD());
                break;
        }
    }

    IEnumerator EventA()
    {
        Debug.Log("イベントA");

        _baa.SetActive(true);
        WaitForSeconds wait = new WaitForSeconds(5.6f);
        yield return wait;
        _baa.SetActive(false);
    }

    IEnumerator EventB()
    {
        Debug.Log("イベントB");

        _akunin.SetActive(true);
        WaitForSeconds wait = new WaitForSeconds(4.5f);
        yield return wait;
        _akunin.SetActive(false);
    }

    IEnumerator EventC()
    {
        Debug.Log("イベントC");

        _girl.SetActive(true);
        _kurotama.SetActive(false);
        WaitForSeconds wait = new WaitForSeconds(4.6f);
        yield return wait;
        _girl.SetActive(false);
        _kurotama.SetActive(true);
    }

    IEnumerator EventD()
    {
        Debug.Log("イベントD");
        yield return null;
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
