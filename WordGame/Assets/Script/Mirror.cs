using UnityEngine;

public class Mirror : MonoBehaviour
{
    private Camera _cam;
    private FlagManager _flagManager;

    void Awake()
    {
        _cam = GetComponent<Camera>();

        _flagManager = FindFirstObjectByType<FlagManager>();
    }

    void Update()
    {
        if (_flagManager.mirror)
        {
            _cam.ResetProjectionMatrix();
            _cam.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
        }
        else
        {
            _cam.ResetProjectionMatrix();
        }
    }
}
