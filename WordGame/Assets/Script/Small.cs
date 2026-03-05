using UnityEngine;
using UnityEngine.SceneManagement;

public class Small : MonoBehaviour
{
    void OnEnable()//Activeになる度に開始される処理
    {
        SceneManager.LoadScene("SmallWorld");
    }
}