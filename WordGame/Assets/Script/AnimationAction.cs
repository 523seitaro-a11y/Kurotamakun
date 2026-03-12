using UnityEngine;

public class AnimationAction : MonoBehaviour
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

    public void Speed2()
    {
        Speed = 2f;
    }

    public void Speed4()
    {
        Speed = 4f;
    }

    public void Speed5()
    {
        Speed = 5f;
    }
}