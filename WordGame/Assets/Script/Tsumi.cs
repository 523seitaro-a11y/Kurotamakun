using UnityEngine;
using System.Collections.Generic;

public class Tsumi : MonoBehaviour
{
    //Float用の変数(Float用のコードは上下するアニメーションが不要な場合削除してもOK)
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;
    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;
    private Vector3 _startPos;

    [SerializeField, Header("進行速度")]
    public float TsumiMoveSpeed = 1.0f;

    [SerializeField, Header("イベント発生間隔")]
    private float _interval = 5f;



    private List<int> _events;
    private int _currentIndex;
    private float _timer;

    void Awake()//Activeになった瞬間に一度だけ開始される処理
    {
        _startPos = transform.position;//Float用
    }

    void Update()//Activeになっている間に繰り返される処理
    {
        Float();//上下するアニメーション

        if (_currentIndex >= _events.Count)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer >= _interval)
        {
            _timer = 0f;
            NextEvent();
        }
    }

    void OnEnable()//Activeになる度に開始される処理
    {
        ResetEvents();
    }

    void OnDisable()//非Activeになった瞬間に開始される処理（コルーチンの停止用）
    {
        
    }

    void ResetEvents()
    {
        _events = new List<int>() { 0, 1, 2, 3 };
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
                EventA();
                break;

            case 1:
                EventB();
                break;

            case 2:
                EventC();
                break;

            case 3:
                EventD();
                break;
        }
    }

    void EventA()
    {
        Debug.Log("イベントA");

        
    }

    void EventB()
    {
        Debug.Log("イベントB");
    }

    void EventC()
    {
        Debug.Log("イベントC");
    }

    void EventD()
    {
        Debug.Log("イベントD");
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

    void Float()//上下するアニメーション
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.position = _startPos + new Vector3(0, y, 0);
    }
}
