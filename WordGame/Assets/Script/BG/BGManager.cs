using UnityEngine;

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
    private float _catTime = 5.0f;

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

    private float _timer;

    private float _wingCount = 0f;

    private float _catCount = 0f;

    private float _rocketCurrentX;
    private float _rocketPosY;

    private float _drillCurrentZ;
    private float _drillPosY;

    private float _posX;

    void Update()
    {
        if ((_rocket == null || !_rocket.gameObject.activeInHierarchy) && 
        (_wing == null || !_wing.gameObject.activeInHierarchy) && 
        (_drill == null || !_drill.gameObject.activeInHierarchy) &&
        (_cat == null || !_cat.gameObject.activeInHierarchy))
        {
            _rocketCurrentX = 0f;
            _drillCurrentZ = 270f;
            _wingCount = 0f;
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
        else if (_cat.gameObject.activeInHierarchy)
        {
            _catCount += Time.deltaTime * _catTime;
            _catCount = Mathf.Clamp(_catCount, 0f, 2f);
        }
        _rocketPosY = -25 * _rocketCurrentX / 90;
        _drillPosY = 15 * (_drillCurrentZ - 270) / 90;
        transform.position = new Vector3(0f, _rocketPosY + _drillPosY + -_wingCount + -_catCount, 0f);

        HoleObject();

        
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
