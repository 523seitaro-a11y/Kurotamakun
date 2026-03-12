using UnityEngine;

public class Akunin : MonoBehaviour
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

    public void Speed2()
    {
        Speed = 3f;
    }
}