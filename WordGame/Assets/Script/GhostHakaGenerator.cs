using UnityEngine;
using System.Collections;

public class GhostHakaGenerator : MonoBehaviour
{
    [SerializeField, Header("墓のプレハブ")]
    private GameObject _hakaPrefab;

    [Header("出現間隔の設定")]
    [SerializeField, Header("最低限空ける間隔（秒）")]
    private float _minInterval = 1.5f;
    [SerializeField, Header("追加するランダムな最大時間（秒）")]
    private float _randomRange = 3.0f;

    [Header("出現位置の設定")]
    [SerializeField, Header("出現位置の右端X")]
    private float _spawnX = 12f;
    [SerializeField, Header("高さ(Y)の最小値")]
    private float _minY = -3.5f;
    [SerializeField, Header("高さ(Y)の最大値")]
    private float _maxY = -2.5f;

    [SerializeField, Header("Z座標")] //位置が上手く生成されなかったためコードで
    private float _spawnZ = 5f;

    void OnEnable()
    {
        // 切り替え時に古い墓が残らないよう全削除
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(SpawnRoutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnRoutine()
    {
        // 最初の出現。0.1秒待ってすぐ右端から出す
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            // Y軸（高さ）をランダムに決定
            float randomY = Random.Range(_minY, _maxY);
            Vector3 spawnPos = new Vector3(_spawnX, randomY, _spawnZ); //_spawnZを指定

            // 生成
            GameObject newHaka = Instantiate(_hakaPrefab, spawnPos, Quaternion.identity);
            newHaka.transform.SetParent(this.transform);

            // 次の出現までの時間を計算
            // 「最低限の間隔」＋「0〜ランダム最大時間」
            float waitTime = _minInterval + Random.Range(0f, _randomRange);

            yield return new WaitForSeconds(waitTime);
        }
    }
}