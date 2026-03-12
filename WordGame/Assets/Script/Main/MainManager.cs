using UnityEngine;
using UnityEngine.InputSystem;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("進行速度")]
    private float _speed = 1.0f;

    [SerializeField, Header("足")]
    private GameObject _leg;

    [SerializeField, Header("翼")]
    private GameObject _wing;

    public float Speed = 1.0f;

    [SerializeField, Header("図")]
    private GameObject _zu;

    [SerializeField, Header("虎")]
    private GameObject _tiger;

    [SerializeField, Header("徳")]
    private GameObject _toku;

    [SerializeField, Header("罪")]
    private GameObject _tsumi;


    private float _legSpeed = 1.0f;

    private float _wingSpeed = 1.0f;

    private float _zuSpeed = 1.0f;

    private float _tigerSpeed = 1.0f;

    private float _tokuSpeed = 1.0f;

    private float _tsumiSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Leg leg = FindFirstObjectByType<Leg>();
        if (leg != null)
        {
            _legSpeed = leg.speed;
        }
        else
        {
            _legSpeed = 1.0f;
        }

        Wing wing = FindFirstObjectByType<Wing>();
        if (wing != null)
        {
            _wingSpeed = wing.speed;
        }
        else
        {
            _wingSpeed = 1.0f;
        }

        Zu zu = FindFirstObjectByType<Zu>();
        if (zu != null)
        {
            _zuSpeed = zu.speed;
        }
        else
        {
            _zuSpeed = 1.0f;
        }

        Tiger tiger = FindFirstObjectByType<Tiger>();
        if (tiger != null)
        {
            _tigerSpeed = tiger.speed;
        }
        else
        {
            _tigerSpeed = 1.0f;
        }

        Toku toku = FindFirstObjectByType<Toku>();
        if (toku != null)
        {
            _tokuSpeed = toku.TokuMoveSpeed;
        }
        else
        {
            _tokuSpeed = 1.0f;
        }

        Tsumi tsumi = FindFirstObjectByType<Tsumi>();
        if (tsumi != null)
        {
            _tsumiSpeed = tsumi.TsumiMoveSpeed;
        }
        else
        {
            _tsumiSpeed = 1.0f;
        }

        Speed = _speed * _legSpeed * _wingSpeed * _zuSpeed * _tigerSpeed * _tokuSpeed * _tsumiSpeed;
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
            Application.Quit();
    }
}
