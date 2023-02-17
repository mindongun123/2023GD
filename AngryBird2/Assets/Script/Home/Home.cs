using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Home : MonoBehaviour
{
    [Header("Setting")]
    public GameObject UI_img_Volume;// moi co minh Volume trong Setting
    public Slider sliderVolume;

    [Header("Button Shop")]
    public GameObject UI_img_Shop;

    [Header("Score")]
    public TextMeshProUGUI UI_txt_Score;

    [Header("Menu Angry Bird")]
    public GameObject[] UI_MenuButton;
    public Image[] UI_img_MenuButton;
    public Sprite[] UI_spritesBirdOnButton;// Sprite cua button de hien thi con Angry Bird ta chon
    public TextMeshProUGUI[] UI_txtAmountAngryBird;

    [Header("Buy Angry Bird")]
    public Bird[] BuyAngryBird;

    [Header("FX Start Game")]
    public GameObject imageFXStartGame;

    [Header("Audio")]
    public AudioSource auPlay;
    private void Start()
    {
        sliderVolume.value= GameManager.instance.data._volume;
        auPlay.Play();
        auPlay.loop = true;
        ViewMenuAmountAngryBird();
        if (GameManager.instance.data._startGameTheOne == false)
        {
            GameManager.instance.data.listAngryBird.Add(BuyAngryBird[0]);
            GameManager.instance.data.listAngryBird[0]._amountAngryBird = 5;
            GameManager.instance.data._startGameTheOne = true;
        }
    }


    float i = 1;
    private void FixedUpdate()
    {
        ViewMenuAmountAngryBird();
        if (i > 0.2f)
        {
            imageFXStartGame.GetComponent<Image>().color = new Color(255, 255, 255, i - Time.fixedDeltaTime);
            i = i - Time.fixedDeltaTime / 5;
        }
        else
        {
            imageFXStartGame.SetActive(false);
        }
    }
    private void Update()
    {
        sliderVolume.maxValue = 1;
        sliderVolume.minValue = 0;
        GameController.instance._volumeGame = sliderVolume.value;
        auPlay.volume = GameController.instance._volumeGame;
        UI_txt_Score.text = GameManager.instance.data._score.ToString();
        RemoveAngryBird();
        ViewMenuAmountAngryBird();
    }

    #region Button Setting
    bool _stClick = false;
    public void BtSettingClick()
    {
        if (_spClick == false)
        {
            if (_stClick == false)
            {
                UI_img_Volume.SetActive(true);
                _stClick = true;
                GameController.instance.stateGame = Enums.StateGame.Pause;
            }
            else
            {

                UI_img_Volume.SetActive(false);
                GameController.instance.stateGame = Enums.StateGame.Start;
                _stClick = false;
            }
        }
    }
    #endregion
    #region Button Shop
    bool _spClick = false;
    public void BtShopClick()
    {
        if (_stClick == false)
        {
            if (_spClick == false)
            {
                UI_img_Shop.SetActive(true);
                GameController.instance.stateGame = Enums.StateGame.Pause;
                _spClick = true;
            }
            else
            {
                UI_img_Shop.SetActive(false);
                GameController.instance.stateGame = Enums.StateGame.Start;
                _spClick = false;
            }
        }
    }
    #endregion
    #region  Shop Buy Angry Bird
    public void BtBuyAngryBird(int _id)
    {
        bool _check = false;
        if (GameManager.instance.data._score > BuyAngryBird[_id]._moneyAngryBird)
        {
            for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
            {
                if (GameManager.instance.data.listAngryBird[i]._idAngryBird == _id)
                {
                    GameManager.instance.data.listAngryBird[i]._amountAngryBird = GameManager.instance.data.listAngryBird[i]._amountAngryBird + 1;
                    GameManager.instance.data._score = GameManager.instance.data._score - BuyAngryBird[_id]._moneyAngryBird;
                    GameController.instance.auCoin.Play();
                    _check = true;
                    break;
                }
            }
            if (_check == false)
            {
                GameManager.instance.data.listAngryBird.Add(BuyAngryBird[_id]);
                GameManager.instance.data._score = GameManager.instance.data._score - BuyAngryBird[_id]._moneyAngryBird;
                GameController.instance.auCoin.Play();

            }
        }
        ViewMenuAmountAngryBird();
    }

    #endregion
    #region View Menu Amount Angry Bird
    public void ViewMenuAmountAngryBird()
    {
        if (GameManager.instance.data.listAngryBird.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
            {
                UI_img_MenuButton[i].sprite = UI_spritesBirdOnButton[GameManager.instance.data.listAngryBird[i]._idAngryBird];
                UI_txtAmountAngryBird[i].text = GameManager.instance.data.listAngryBird[i]._amountAngryBird.ToString();
                UI_MenuButton[i].SetActive(true);
            }
        }
    }
    #endregion
    #region Remove Angry Bird khi Amount Angry Bird = 0
    public void RemoveAngryBird()
    {

        if (GameManager.instance.data.listAngryBird.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
            {
                if (GameManager.instance.data.listAngryBird[i]._amountAngryBird <= 0)
                {
                    UI_MenuButton[i].SetActive(false);
                    GameManager.instance.data.listAngryBird.Remove(GameManager.instance.data.listAngryBird[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                UI_MenuButton[i].SetActive(false);
            }
        }
    }
    #endregion
    #region Play Game
    public void BtPlayGame()
    {
        GameManager.instance.SaveData();
        SceneManager.LoadScene("SampleScene");
    }
    #endregion

}
