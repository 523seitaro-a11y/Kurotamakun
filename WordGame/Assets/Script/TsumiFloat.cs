using UnityEngine;
using System.Collections;

public class TsumiFloat : MonoBehaviour
{
    [SerializeField, Header("上下の幅")]
    private float _amplitude = 0.2f;

    [SerializeField, Header("揺れる速さ")]
    private float _speed = 1.0f;

    [SerializeField] private GameObject prefab;

    [SerializeField, Header("積み上げ間隔")]
    private float _stackY = 4.0f;

    [SerializeField, Header("最初の高さ")]
    private float _startY = 2.0f;

    [SerializeField, Header("出現開始の高さ")]
    private float _spawnStartY = 15.0f;

    [SerializeField, Header("落下時間")]
    private float _moveDuration = 0.5f;

    [SerializeField, Header("Zのずれ")]
    private float _zStep = 0.1f;

    private Vector3 _startLocalPos;
    private Tsumi parent;

    private int _prevTsumiCount;

    void Awake()
    {
        _startLocalPos = transform.localPosition;
        parent = GetComponentInParent<Tsumi>();

        if (parent == null)
        {
            Debug.LogError("親階層に Tsumi が見つかりません。", this);
        }
    }

    void OnEnable()
    {
        ClearChildren();

        if (parent != null && parent.TsumiCount > 0)
        {
            _prevTsumiCount = 0;

            // 初期分はその場に並べるだけ
            for (int i = 0; i < parent.TsumiCount; i++)
            {
                CreateObjectAt(i, false);
            }

            _prevTsumiCount = parent.TsumiCount;
        }
        else
        {
            _prevTsumiCount = 0;
        }
    }

    void OnDisable()
    {
        ClearChildren();
    }

    void Update()
    {
        if (parent == null) return;

        FloatMove();
        PileUpTsumi();
    }

    void FloatMove()
    {
        float y = Mathf.Sin(Time.time * _speed) * _amplitude;
        transform.localPosition = _startLocalPos + new Vector3(0f, y, 0f);
    }

    void PileUpTsumi()
    {
        int currentTsumiCount = parent.TsumiCount;

        // 増えたときだけ追加
        if (currentTsumiCount > _prevTsumiCount)
        {
            for (int i = _prevTsumiCount; i < currentTsumiCount; i++)
            {
                // 新しく増えた一番上だけ落下演出
                CreateObjectAt(i, true);
            }

            _prevTsumiCount = currentTsumiCount;
        }
        // 減ったときは全部作り直す
        else if (currentTsumiCount < _prevTsumiCount)
        {
            ClearChildren();

            for (int i = 0; i < currentTsumiCount; i++)
            {
                CreateObjectAt(i, false);
            }

            _prevTsumiCount = currentTsumiCount;
        }
    }

    void CreateObjectAt(int index, bool fallFromTop)
    {
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab, transform);

        float z = 0.5f - index * _zStep;  // ← 上ほど小さくする

        Vector3 targetPos = new Vector3(0f, index * _stackY + _startY, z);
        obj.transform.localRotation = Quaternion.identity;

        if (fallFromTop)
        {
            Vector3 startPos = new Vector3(0f, _spawnStartY, z);
            obj.transform.localPosition = startPos;

            StartCoroutine(MoveToPosition(obj.transform, startPos, targetPos, _moveDuration));
        }
        else
        {
            obj.transform.localPosition = targetPos;
        }
    }

    IEnumerator MoveToPosition(Transform target, Vector3 start, Vector3 end, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            if (target == null) yield break;

            time += Time.deltaTime;
            float t = time / duration;
            target.localPosition = Vector3.Lerp(start, end, t);

            yield return null;
        }

        if (target != null)
        {
            target.localPosition = end;
        }
    }

    void ClearChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }
    }
}