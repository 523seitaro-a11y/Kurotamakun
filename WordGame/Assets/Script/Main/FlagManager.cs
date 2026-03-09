using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering; // 追加
using UnityEngine.Rendering.Universal; // 追加
using System.Collections;

//nairo

//branch

public class FlagManager : MonoBehaviour
{
    public NFCReader nfcReader;

    // --- 追加分 ---
    [SerializeField, Header("URPのRendererDataアセット")]
    private UniversalRendererData _rendererData;
    private FullScreenPassRendererFeature _nightModeFeature;
    // --------------

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

    void Start()
    {
        // Renderer Featureの中から名前が "NightModeFeature" のものを探す
        if (_rendererData != null)
        {
            foreach (var feature in _rendererData.rendererFeatures)
            {
                //名前が一致するエフェクトを取得
                if (feature is FullScreenPassRendererFeature && feature.name == "NightModeFeature")
                {
                    _nightModeFeature = (FullScreenPassRendererFeature)feature;
                    break;
                }
            }
        }

        // 開始時はオフにしておく
        if (_nightModeFeature != null) _nightModeFeature.SetActive(false);

    }


    void Update()
    {
        var keyboard = Keyboard.current;

        pause = keyboard.pKey.isPressed || nfcReader.isP;
        mirror = keyboard.mKey.isPressed || nfcReader.isM;
        night = keyboard.yKey.isPressed || nfcReader.isY;

        horror = keyboard.hKey.isPressed || nfcReader.isH;
        ko = keyboard.kKey.isPressed || nfcReader.isK;
        ghost = keyboard.gKey.isPressed || nfcReader.isG;
        rocket = keyboard.oKey.isPressed || nfcReader.isO;
        bone = keyboard.bKey.isPressed || nfcReader.isB;
        rain = keyboard.rKey.isPressed || nfcReader.isR;
        leg = keyboard.lKey.isPressed || nfcReader.isL;
        fish = keyboard.fKey.isPressed || nfcReader.isF;
        wing = keyboard.wKey.isPressed || nfcReader.isW;
        under = keyboard.uKey.isPressed || nfcReader.isU;
        destroy = keyboard.dKey.isPressed || nfcReader.isD;
        toku = keyboard.tKey.isPressed || nfcReader.isT;
        neko = keyboard.nKey.isPressed || nfcReader.isN;
        zu = keyboard.zKey.isPressed || nfcReader.isZ;
        arm = keyboard.aKey.isPressed || nfcReader.isA;
        crime = keyboard.cKey.isPressed || nfcReader.isC;
        small = keyboard.sKey.isPressed || nfcReader.isS;

        // --- 夜（色反転）の反映 ---
        if (_nightModeFeature != null)
        {
            // night変数の true/false をそのままシェーダーのON/OFFに適用
            _nightModeFeature.SetActive(night);
        }
        // --------------------------

        inputWord = horror || ko || ghost || rocket || bone || rocket || leg || fish || wing || under || destroy || toku || neko || zu || arm || small;

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
                arm,
                ko,
                small
            ));
        }
        else
        {
            _mainCharacter.SetActive(true);

            //_night.SetActive(night);
            _rain.SetActive(rain);
            // _crime.SetActive(crime);
            
            _horror.SetActive(false);
            _ghost.SetActive(false);
            _bone.SetActive(false);
            _rocket.SetActive(false);
            _leg.SetActive(false);
            _fish.SetActive(false);
            _wing.SetActive(false);
            _under.SetActive(false);
            _neko.SetActive(false);
            // _destroy.SetActive(false);
            // _toku.SetActive(false);
            _zu.SetActive(false);
            _arm.SetActive(false);
            _ko.SetActive(false);
            _small.SetActive(false);
        }
    }

    IEnumerator InputSequence(bool h,bool g,bool b,bool o,bool l,bool f,bool w,bool u,bool d,bool t,bool n,bool z,bool a,bool k,bool s)
    {
        if (_mainCharacter.activeSelf)
        {
            if (!s)
            {
                yield return StartCoroutine(_mainCharacter.GetComponent<MainCharacter>().RotateDisappear());
            }
            else
            {
                _mainCharacter.SetActive(false);
            }
        }
        _horror.SetActive(h);
        _ghost.SetActive(g);
        _bone.SetActive(b);
        _rocket.SetActive(o);
        _leg.SetActive(l);
        _fish.SetActive(f);
        _wing.SetActive(w);
        _under.SetActive(u);
        _neko.SetActive(n);
        // _destroy.SetActive(d);
        // _toku.SetActive(t);
        _zu.SetActive(z);
        _arm.SetActive(a);
        _ko.SetActive(k);
        _small.SetActive(s);
    }
}
