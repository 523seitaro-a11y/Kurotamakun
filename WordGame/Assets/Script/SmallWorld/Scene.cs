using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Scene : MonoBehaviour
{
    

    void Update()//Activeになる度に開始される処理
    {
        var keyboard = Keyboard.current;

        if (!keyboard.sKey.isPressed)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}