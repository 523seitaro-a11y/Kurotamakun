using UnityEngine;

public class ChildSwitch : MonoBehaviour
{
    [SerializeField, Header("切り替えを行うX座標")]
    private float switchX = 0f;

    [SerializeField, Header("非アクティブにする子オブジェクト")]
    private GameObject childOff;

    [SerializeField, Header("アクティブにする子オブジェクト")]
    private GameObject childOn;

    private bool switched = false;

    void Update()
    {
        // 一度だけ切り替える
        if (!switched && transform.position.x < switchX)
        {
            childOff.SetActive(false);
            childOn.SetActive(true);

            switched = true;
        }
    }
}