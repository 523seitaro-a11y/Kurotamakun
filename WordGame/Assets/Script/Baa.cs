using UnityEngine;

public class Baa : MonoBehaviour
{
    public float Speed = 1.0f;

    void OnEnable()
    {
        Speed = 1.0f;
    }

    public void Speed0()
    {
        Speed = 0f;
    }

    public void Speed1()
    {
        Speed = 1f;
    }
}