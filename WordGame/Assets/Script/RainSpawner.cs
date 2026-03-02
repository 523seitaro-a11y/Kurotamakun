using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    [SerializeField, Header("水のプレハブ")]
    private GameObject _mizuPrefab;

    [SerializeField, Header("生成間隔")]
    private float _spawnInterval = 0.2f;

    [SerializeField, Header("生成範囲（横幅）")]
    private float _spawnRangeX = 8f;

    private float _timer;

    void Update()
    {
        // オブジェクトがActiveな間だけ実行される
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnMizu();
            _timer = 0;
        }
    }

    void SpawnMizu()
    {
        // 画面上方のランダムな位置を計算
        float randomX = Random.Range(-_spawnRangeX, _spawnRangeX);
        Vector3 spawnPos = transform.position + new Vector3(randomX, 5f, 0); // 5fは高さ調整

        // 生成
        GameObject mizu = Instantiate(_mizuPrefab, spawnPos, Quaternion.identity);

        // 5秒後に自動で削除（メモリ節約のため）
        Destroy(mizu, 5f);
    }
}