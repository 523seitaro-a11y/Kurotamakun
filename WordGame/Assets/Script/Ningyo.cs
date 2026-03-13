using UnityEngine;
using System.Collections;

public class Ningyo : MonoBehaviour
{
    [SerializeField, Header("Ningyo")] 
    private GameObject _ningyo;

    [SerializeField, Header("発動間隔（秒）")]
    private float interval = 15f;

    [SerializeField, Header("表示時間（秒）")]
    private float activeTime = 6f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            StartCoroutine(ActivateNingyo());
            timer = 0f;
        }
    }

    IEnumerator ActivateNingyo()
    {
        _ningyo.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        _ningyo.SetActive(false);
    }
}