using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI keyTextDisplay;
    public TextMeshProUGUI hpTextDisplay;
    public TextMeshProUGUI poisonTextDisplay;
    public TextMeshProUGUI coinTextDisplay;

    private PlayerController playerCounters;

    private Image _keyImg;
    private Image _hpPotionImg;
    private Image _poisonPotionImg;
    private Image _coinImg;

    public Sprite fromKeyImg;
    public Sprite toKeyImg;
    public Sprite fromHpPotionImg;
    public Sprite toHpPotionImg;
    public Sprite fromPoisonPotionImg;
    public Sprite toPoisonPotionImg;
    public Sprite fromCoinImg;
    public Sprite toCoinImg;



    private void Start()
    {
        playerCounters =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        _keyImg = GameObject.FindGameObjectWithTag("UIKeyImage").GetComponent<Image>();
        _hpPotionImg = GameObject.FindGameObjectWithTag("hpBottleImg").GetComponent<Image>();
        _poisonPotionImg = GameObject.FindGameObjectWithTag("poisonBottleImg").GetComponent<Image>();
        _coinImg = GameObject.FindGameObjectWithTag("coinImg").GetComponent<Image>();
    }

    private void Update()
    {

        keyTextDisplay.text = $"{playerCounters.KeyCounter}";
        if (playerCounters.KeyCounter == 0) _keyImg.sprite = fromKeyImg;
        else _keyImg.sprite = toKeyImg;

        hpTextDisplay.text = $"{playerCounters.hpPotionCounter}";
        if (playerCounters.hpPotionCounter == 0) _hpPotionImg.sprite = fromHpPotionImg;
        else _hpPotionImg.sprite = toHpPotionImg;

        poisonTextDisplay.text = $"{playerCounters.poisonPotionCounter}";
        if (playerCounters.poisonPotionCounter == 0) _poisonPotionImg.sprite = fromPoisonPotionImg;
        else _poisonPotionImg.sprite = toPoisonPotionImg;

        coinTextDisplay.text = $"{playerCounters.coinCounter}";
        if (playerCounters.coinCounter == 0) _coinImg.sprite = fromCoinImg;
        else _coinImg.sprite = toCoinImg;






        //Debug.Log($"{KeyCount._keyCounter}");

    }










}
