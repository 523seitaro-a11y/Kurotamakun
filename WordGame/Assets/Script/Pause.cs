using UnityEngine;

public class Pause : MonoBehaviour
{
    private FlagManager _flagManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        _flagManager = FindFirstObjectByType<FlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_flagManager.pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
