using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Scene : MonoBehaviour
{
    public NFCReader nfcReader;

    void Update()//Activeになる度に開始される処理
    {
        var keyboard = Keyboard.current;

        if (!keyboard.sKey.isPressed && !nfcReader.isS)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}