using UnityEngine;

public class FireParent : MonoBehaviour
{
    [SerializeField] private GameObject targetChild;

    public void Activate()
    {
        targetChild.SetActive(true);
    }

    void OnEnable()
    {
        targetChild.SetActive(false);
    }

    void OnDisable()
    {
        targetChild.SetActive(false);
    }
}
