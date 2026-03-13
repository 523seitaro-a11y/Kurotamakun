using UnityEngine;
using System.Collections;

public class RainKaminari : MonoBehaviour
{
    [SerializeField, Header("雷のプレハブ")]
    private GameObject _thunderPrefab;

    [SerializeField, Header("出現間隔の最小値")]
    private float _minInterval = 2f;

    [SerializeField, Header("出現間隔の最大値")]
    private float _maxInterval = 8f;

    [SerializeField, Header("生成するZ座標")]
    private float _spawnZ = 0f;   // ←追加

    private Camera _mainCamera;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void OnEnable()
    {
        StartCoroutine(ThunderRoutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ThunderRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(_minInterval, _maxInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnThunder();
        }
    }

    void SpawnThunder()
    {
        float viewportX = Random.Range(0.1f, 0.9f);
        float viewportY = Random.Range(0.6f, 0.9f);

        Vector3 worldPos = _mainCamera.ViewportToWorldPoint(new Vector3(viewportX, viewportY, 10));

        worldPos.z = _spawnZ; // ←Inspectorで設定できる

        GameObject thunder = Instantiate(_thunderPrefab, worldPos, Quaternion.identity);

        thunder.transform.SetParent(this.transform);

        StartCoroutine(FlashEffect(thunder));
    }

    IEnumerator FlashEffect(GameObject thunder)
    {
        SpriteRenderer sr = thunder.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color col = sr.color;
        col.a = 0;
        sr.color = col;

        float elapsed = 0f;
        while (elapsed < 0.05f)
        {
            elapsed += Time.deltaTime;
            col.a = Mathf.Lerp(0, 1, elapsed / 0.05f);
            sr.color = col;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < 0.8f)
        {
            elapsed += Time.deltaTime;
            col.a = Mathf.Lerp(1, 0, elapsed / 0.8f);
            sr.color = col;
            yield return null;
        }

        Destroy(thunder);
    }
}