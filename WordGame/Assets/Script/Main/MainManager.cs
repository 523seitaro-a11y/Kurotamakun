using UnityEngine;
using UnityEngine.InputSystem;

public class MainManager : MonoBehaviour
{
    [SerializeField,Header("進行速度")]
    private float _speed = 1.0f;

    [SerializeField,Header("足")]
    private GameObject _leg;

    [SerializeField,Header("翼")]
    private GameObject _wing;

    public float Speed = 1.0f;
    
    private float _legSpeed = 1.0f;

    private float _wingSpeed = 1.0f;

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

        Speed = _speed * _legSpeed * _wingSpeed;
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        Application.Quit();
    }
}
