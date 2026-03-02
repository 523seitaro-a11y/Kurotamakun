using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

//nairo

public class FlagManager : MonoBehaviour
{
    [SerializeField, Header("MainCharacter")]
    private GameObject _mainCharacter;

    [SerializeField, Header("怖")] 
    private GameObject _horror;
    [SerializeField, Header("霊")] 
    private GameObject _ghost;
    [SerializeField, Header("骨")] 
    private GameObject _bone;
    [SerializeField, Header("宙")] 
    private GameObject _rocket;
    [SerializeField, Header("足")] 
    private GameObject _leg;
    [SerializeField, Header("魚")] 
    private GameObject _fish;
    [SerializeField, Header("翼")] 
    private GameObject _wing;
    [SerializeField, Header("掘")] 
    private GameObject _under;
    [SerializeField, Header("夜")] 
    private GameObject _night;
    [SerializeField, Header("虎")] 
    private GameObject _ko;
    [SerializeField, Header("壊")] 
    private GameObject _destroy;
    [SerializeField, Header("雨")] 
    private GameObject _rain;
    [SerializeField, Header("徳")] 
    private GameObject _toku;
    [SerializeField, Header("猫")] 
    private GameObject _neko;
    [SerializeField, Header("図")] 
    private GameObject _zu;
    [SerializeField, Header("筋")] 
    private GameObject _arm;
    [SerializeField, Header("罪")] 
    private GameObject _crime;
    [SerializeField, Header("小")] 
    private GameObject _small;

    public bool inputWord = false;

    public bool pause;//止
    public bool mirror;//鏡
    public bool night;//夜

    public bool horror;//怖
    public bool ko;//虎
    public bool ghost;//霊
    public bool rocket;//宙
    public bool bone;//骨
    public bool destroy;//壊
    public bool rain;//雨
    public bool toku;//徳
    public bool neko;//猫
    public bool zu;//図
    public bool arm;//筋
    public bool wing;//翼
    public bool fish;//魚
    public bool leg;//足
    public bool under;//掘
    public bool crime;//罪
    public bool small;//小

    void Update()
    {
        var keyboard = Keyboard.current;

        pause = keyboard.pKey.isPressed;
        mirror = keyboard.mKey.isPressed;
        night = keyboard.nKey.isPressed;

        horror = keyboard.hKey.isPressed;
        ko = keyboard.kKey.isPressed;
        ghost = keyboard.gKey.isPressed;
        rocket = keyboard.oKey.isPressed;
        bone = keyboard.bKey.isPressed;
        rain = keyboard.rKey.isPressed;
        leg = keyboard.lKey.isPressed;
        fish = keyboard.fKey.isPressed;
        wing = keyboard.wKey.isPressed;
        under = keyboard.uKey.isPressed;
        destroy = keyboard.dKey.isPressed;
        toku = keyboard.tKey.isPressed;
        neko = keyboard.nKey.isPressed;
        zu = keyboard.zKey.isPressed;
        arm = keyboard.aKey.isPressed;
        crime = keyboard.cKey.isPressed;
        small = keyboard.sKey.isPressed;

        inputWord = horror || ko || ghost || rocket || bone || rocket || leg || fish || wing || under || destroy || toku || neko || zu || arm ;

        if (inputWord)
        {
            StartCoroutine(InputSequence(
                horror,
                ghost,
                bone,
                rocket,
                leg,
                fish,
                wing,
                under,
                destroy,
                toku,
                neko,
                zu,
                arm
            ));
        }
        else
        {
            _mainCharacter.SetActive(true);

            // _night.SetActive(night);
            // _rain.SetActive(rain);
            // _crime.SetActive(crime);
            // _small.SetActive(small);

            _horror.SetActive(false);
            _ghost.SetActive(false);
            _bone.SetActive(false);
            _rocket.SetActive(false);
            _leg.SetActive(false);
            _fish.SetActive(false);
            _wing.SetActive(false);
            _under.SetActive(false);
            //_neko.SetActive(false);
            // _destroy.SetActive(false);
            // _toku.SetActive(false);
            // _zu.SetActive(false);
            // _arm.SetActive(false);
        }
    }

    IEnumerator InputSequence(bool h,bool g,bool b,bool o,bool l,bool f,bool w,bool u,bool d,bool t,bool n,bool z,bool a)
    {
        if (_mainCharacter.activeSelf)
        {
            yield return StartCoroutine(_mainCharacter.GetComponent<MainCharacter>().RotateDisappear());
        }
        _horror.SetActive(h);
        _ghost.SetActive(g);
        _bone.SetActive(b);
        _rocket.SetActive(o);
        _leg.SetActive(l);
        _fish.SetActive(f);
        _wing.SetActive(w);
        _under.SetActive(u);
        //_neko.SetActive(n);
        // _destroy.SetActive(d);
        // _toku.SetActive(t);
        // _zu.SetActive(z);
        // _arm.SetActive(a);
    }
}
