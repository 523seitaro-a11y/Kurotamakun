using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;

    private AudioSource audioSource;

    [Range(0f,1f)]
    public float volume = 0.5f;

    void Awake()
    {
        // すでにBGMがある場合は削除
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // シーン遷移しても消えない
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    void Update()
    {
        audioSource.volume = volume;
    }
}