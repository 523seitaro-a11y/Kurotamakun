using UnityEngine;

public class FootprintHandler : MonoBehaviour
{
    [SerializeField, Header("左足のプレハブ（足）")] private GameObject _leftFootPrefab;  // 「足」
    [SerializeField, Header("右足のプレハブ（亦）")] private GameObject _rightFootPrefab; // 「亦」
    [SerializeField, Header("背景の速度")] private float _bgSpeed = 4f;         // BGManagerの_speedと同じにする
    [SerializeField, Header("足跡を出す間隔（秒）")] private float _spawnInterval = 0.5f; // 足跡を出す間隔（秒）
    [SerializeField, Header("足跡の生成位置オフセット(キャラ中心からのズレ)")]
    private Vector3 _baseOffset = new Vector3(0, -1.0f, 0); // ここで基本の上下位置を調整
    [SerializeField, Header("足の幅（上下）")] private float _footWidth = 0.3f; // 上下のズレ

    private float _timer;
    private bool _isLeftStep = true;

    void Update()
    {
        // 脚のオブジェクトがActive（生えている）時だけカウント
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            Spawn();
            _timer = 0f;
        }
    }

    void Spawn()
    {
        if (_leftFootPrefab == null || _rightFootPrefab == null) return;

        GameObject prefab = _isLeftStep ? _leftFootPrefab : _rightFootPrefab;

        // 1. まず基本のオフセットを加える（ここで足元へ移動させる）
        Vector3 spawnPosition = transform.position + _baseOffset;

        // 2. 次に、左足か右足かによって上下(Y軸)を少しずらす
        float yWidth = _isLeftStep ? _footWidth : -_footWidth;
        spawnPosition.y += yWidth;

        // 生成
        GameObject footprint = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // 足跡に速度を渡す
        if (footprint.TryGetComponent<Footprint>(out var fp))
        {
            fp.SetSpeed(_bgSpeed);
        }

        _isLeftStep = !_isLeftStep;
    }

    // 脚が消えたときにタイマーをリセットする
    void OnEnable() { _timer = 0f; }
}