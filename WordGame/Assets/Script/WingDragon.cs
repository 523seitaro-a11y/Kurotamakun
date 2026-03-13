using UnityEngine;

public class WingDragon : MonoBehaviour
{
    enum State { Waiting, Appearing, Turning, Flying }
    State currentState = State.Waiting;

    [Header("登場・移動設定")]
    public float appearSpeed = 4.0f;
    public float turnPointY = 0.0f;
    public float turnSpeed = 180.0f;
    public float flySpeed = 5.0f;
    public float resetX = -15.0f;

    [Header("うねり設定")]
    public float waveFrequency = 2.0f;
    public float waveMagnitude = 1.0f;

    [Header("出現確率設定")]
    [Tooltip("何秒ごとに判定を行うか")]
    public float checkInterval = 2.0f;
    [Tooltip("出現する確率 (0.0〜1.0)")]
    public float spawnProbability = 0.3f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float timer;

    // 龍の見た目（FBX）をON/OFFするために、子オブジェクトの参照を持つ
    private GameObject modelChild;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // 子オブジェクト（FBXモデル）を取得
        if (transform.childCount > 0)
            modelChild = transform.GetChild(0).gameObject;
    }

    void OnEnable()
    {
        ResetToWaiting();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Waiting:
                UpdateWaiting();
                break;
            case State.Appearing:
                MoveUp();
                break;
            case State.Turning:
                RotateLeft();
                break;
            case State.Flying:
                FlyLeftWithWave();
                break;
        }

        // 左端到達チェック
        if (currentState != State.Waiting && transform.position.x < resetX)
        {
            ResetToWaiting();
        }
    }

    // 出現待機中の処理
    void UpdateWaiting()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;
            // 確率判定
            if (Random.value <= spawnProbability)
            {
                StartFlying();
            }
        }
    }

    void StartFlying()
    {
        currentState = State.Appearing;
        if (modelChild != null) modelChild.SetActive(true);
    }

    void ResetToWaiting()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        currentState = State.Waiting;
        timer = 0f;
        // 待機中は龍の姿だけを隠す
        if (modelChild != null) modelChild.SetActive(false);
    }

    // --- 以下、移動処理（変更なし） ---
    void MoveUp()
    {
        transform.Translate(Vector3.up * appearSpeed * Time.deltaTime, Space.World);
        if (transform.position.y >= turnPointY) currentState = State.Turning;
    }

    void RotateLeft()
    {
        float currentZ = transform.eulerAngles.z;
        float targetZ = 90f;
        float nextZ = Mathf.MoveTowardsAngle(currentZ, targetZ, turnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, nextZ);
        if (Mathf.Abs(Mathf.DeltaAngle(nextZ, targetZ)) < 0.5f) currentState = State.Flying;
    }

    void FlyLeftWithWave()
    {
        Vector3 move = Vector3.left * flySpeed * Time.deltaTime;
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveMagnitude;
        move.y = wave * Time.deltaTime;
        transform.Translate(move, Space.World);
        transform.rotation = Quaternion.Euler(0, 0, 90f + (wave * 5.0f));
    }
}