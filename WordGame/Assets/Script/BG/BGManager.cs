using UnityEngine;
using UnityEngine.InputSystem;

public class BGManager : MonoBehaviour
{
    [SerializeField,Header("ロケット")]
    private Transform _rocket;

    [SerializeField,Header("ドリル")]
    private Transform _drill;

    [SerializeField,Header("翼")]
    private Transform _wing;
    [SerializeField, Header("翼の移動スピード時間")]
    private float _wingTime = 5.0f;

    [SerializeField,Header("猫")]
    private Transform _cat;
    [SerializeField, Header("猫の移動スピード時間")]
    private float _catTime = 3.0f;

    [SerializeField,Header("壊")]
    private Transform _destroy;
    [SerializeField, Header("怪獣の移動スピード時間")]
    private float _destroyTime = 3.0f;

    [SerializeField, Header("穴")]
    private GameObject _hole;
    [SerializeField, Header("穴の間隔")]
    private float _interval = 0.2f;

    [SerializeField, Header("穴オブジェクト１")]
    private GameObject _hole1;
    [SerializeField, Header("穴オブジェクト２")]
    private GameObject _hole2;
    [SerializeField,Header("穴の速度")]
    private float _speed = 4f;

    [SerializeField, Header("木（Tree）の親オブジェクト")]
    private GameObject _treeObject;

    [SerializeField, Header("竹（Tikurin）の親オブジェクト")]
    private GameObject _tikurinObject;

    // --- 追加：月のオブジェクト ---
    [SerializeField, Header("月（Moon）のオブジェクト")]
    private GameObject _moonObject;
    // ----------------------------

    private float _timer;

    private float _wingCount = 0f;

    private float _catCount = 0f;

    private float _destroyCount = 0f;

    private float _rocketCurrentX;
    private float _rocketPosY;

    private float _drillCurrentZ;
    private float _drillPosY;

    private float _posX;

    void Update()
    {
        // --- 追加：Kキーによる背景切り替えとYキーによる月表示 ---
        HandleBackgroundAndEnvironment();
        // ----------------------------------

        if ((_rocket == null || !_rocket.gameObject.activeInHierarchy) && 
        (_wing == null || !_wing.gameObject.activeInHierarchy) && 
        (_drill == null || !_drill.gameObject.activeInHierarchy) &&
        (_cat == null || !_cat.gameObject.activeInHierarchy) &&
        (_destroy == null || !_destroy.gameObject.activeInHierarchy))
        {
            _rocketCurrentX = 0f;
            _drillCurrentZ = 270f;
            _wingCount = 0f;
            _destroyCount = 0f;
            _catCount = 0f;
        }
        else if (_rocket.gameObject.activeInHierarchy)
        {
            _rocketCurrentX = _rocket.eulerAngles.x;
        }
        else if (_drill.gameObject.activeInHierarchy)
        {
            _drillCurrentZ = _drill.eulerAngles.z;
            if (_drillCurrentZ < 1f)
            {
                _drillCurrentZ = 360f;
            }
            Hole();
        }
        else if (_wing.gameObject.activeInHierarchy)
        {
            _wingCount += Time.deltaTime * _wingTime;
            _wingCount = Mathf.Clamp(_wingCount, 0f, 10f);
        }
        else if (_destroy.gameObject.activeInHierarchy)
        {
            _destroyCount += Time.deltaTime * _destroyTime;
            _destroyCount = Mathf.Clamp(_destroyCount, 0f, 3f);
        }
        else if (_cat.gameObject.activeInHierarchy && _cat.GetComponent<Cat>().AfterJump)
        {
            _catCount += Time.deltaTime * _catTime;
            _catCount = Mathf.Clamp(_catCount, 0f, 4f);
        }
        _rocketPosY = -25 * _rocketCurrentX / 90;
        _drillPosY = 15 * (_drillCurrentZ - 270) / 90;
        transform.position = new Vector3(0f, _rocketPosY + _drillPosY + -_wingCount + -_catCount + -_destroyCount , 0f);

        HoleObject();
    }

    void HandleBackgroundAndEnvironment()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Keyboard.current.kKey.isPressed は Kキーが押されている間 true になります
        bool isKPressed = Keyboard.current.kKey.isPressed;

        if (_treeObject != null)
        {
            _treeObject.SetActive(!isKPressed); // Kを押していない時にアクティブ
        }

        if (_tikurinObject != null)
        {
            _tikurinObject.SetActive(isKPressed);  // Kを押している時にアクティブ
        }

        // --- 追加：Yキー：月の表示制御 ---
        bool isYPressed = keyboard.yKey.isPressed;
        if (_moonObject != null)
        {
            _moonObject.SetActive(isYPressed);
        }

    }

    void HoleObject()
    {
        Vector3 pos1 = _hole1.transform.position;
        pos1.x -= _speed * Time.deltaTime;
       
        Vector3 pos2 = _hole2.transform.position;
        pos2.x -= _speed * Time.deltaTime;

        if (!_drill.gameObject.activeInHierarchy)
        {
            pos1.x = 1.5f;
            pos2.x = 1.5f;
        }

        _hole1.transform.position = pos1;
        _hole2.transform.position = pos2;
    }

    void Hole()
    {
        _timer += Time.deltaTime;

        if (_timer >= _interval)
        {
            GameObject holeObj = Instantiate(_hole, transform.position, Quaternion.identity, transform);

            Vector3 pos = holeObj.transform.position;
            pos.y -= _drillPosY + 1.5f;
            pos.z = 11f;
            holeObj.transform.position = pos;

            _timer = 0f;
        }
    }
}
