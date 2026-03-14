using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource audioSource;

    [Range(0f,1f)]
    public float volume = 0.5f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    void Update()
    {
        audioSource.volume = volume;
    }
}