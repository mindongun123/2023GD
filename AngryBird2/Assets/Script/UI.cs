using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{

    [Header("Button Of Menu")]
    public GameObject[] UI_MenuButton;
    public Image[] UI_img_MenuButton;
    public Sprite[] UI_spritesBirdOnButton;// Sprite cua button de hien thi con Angry Bird ta chon
    public TextMeshProUGUI[] UI_txtAmountAngryBird;

    [Header("Pause")]
    public GameObject UI_img_Pause;

    [Header("Setting")]
    // hien tai setting moi co minh volume
    public GameObject UI_img_Volume;
    public Slider sliderVolume;

    [Header("Score")]
    public TextMeshProUGUI UI_txt_Score;

    [Header("FX Start Level New")]
    public GameObject imageFXnewLevel;
    float x = 1;

    private void Start()
    {
        sliderVolume.value = GameManager.instance.data._volume;

        // StartCoroutine(StartUI());
        Debug.Log("UI" + GameManager.instance.data.listAngryBird.Count);
        for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
        {
            UI_img_MenuButton[i].sprite = UI_spritesBirdOnButton[GameManager.instance.data.listAngryBird[i]._idAngryBird];
            UI_MenuButton[i].SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        FX_start();
    }

    public void FX_start()
    {
        if (x > 0.2f)
        {
            imageFXnewLevel.GetComponent<Image>().color = new Color(255, 255, 255, x - Time.fixedDeltaTime);
            x = x - Time.fixedDeltaTime / 5;
        }
        else
        {
            Slingshoot.instance.SetSlingshotLineRenderersActive(true);
            imageFXnewLevel.SetActive(false);
        }
    }
    private void Update()
    {
        sliderVolume.maxValue = 1;
        sliderVolume.minValue = 0;
        GameController.instance._volumeGame = sliderVolume.value;

        UI_txt_Score.text = GameManager.instance.data._score.ToString();
        if (GameManager.instance.data.listAngryBird.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
            {
                UI_txtAmountAngryBird[i].text = GameManager.instance.data.listAngryBird[i]._amountAngryBird.ToString();
            }
        }
        RemoveAngryBird();


        if (LevelGamePlay.instance._checkUI == true)
        {
            UI_img_Volume.SetActive(false);
            UI_img_Pause.SetActive(false);
            _pClick = false;
            _stClick = false;
        }
    }

    #region Menu UI tao ra AngryBird
    public void BtMenuClick(int _id)
    {
        if (GameController.instance.stateGame == Enums.StateGame.Start && LevelGamePlay.instance._checkUI == false)
        {
            if (GameController.instance.stateSlingshoot == Enums.StateSlingshot.AfterShoot && GameManager.instance.data.listAngryBird[_id]._amountAngryBird > 0)
            {
                GameObject newAngryBird = Instantiate(GameManager.instance.data.listAngryBird[_id].AngryBirdPrefabs, Slingshoot.instance.Mid_locationAngryBirdStart, transform.rotation);

                // Di chuyen camera
                CameraFollow.instance.BirdToFollow = newAngryBird;
                // CameraFollow.instance.StartingPosition = CameraFollow.instance.transform.position;
                CameraFollow.instance.IsFollowing = true;


                GameManager.instance.data.listAngryBird[_id]._amountAngryBird = GameManager.instance.data.listAngryBird[_id]._amountAngryBird - 1;

                GameController.instance.stateSlingshoot = Enums.StateSlingshot.BeforeShoot;
            }
        }
    }
    #endregion
    #region Pause
    bool _pClick = false;
    public void BtPause()
    {
        if (LevelGamePlay.instance._checkUI == false)
        {
            _pClick = true;
            GameController.instance.stateGame = Enums.StateGame.Pause;
            UI_img_Pause.SetActive(true);
        }
    }

    // Continue 
    public void BtCotinue()
    {
        _pClick = false;
        UI_img_Pause.SetActive(false);
        GameController.instance.stateGame = Enums.StateGame.Start;
    }
    // Home
    public void BtHome()
    {
        Slingshoot.instance.SetSlingshotLineRenderersActive(false);
        for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
        {
            if (GameManager.instance.data.listAngryBird[i]._amountAngryBird <= 0)
            {
                GameManager.instance.data.listAngryBird.Remove(GameManager.instance.data.listAngryBird[i]);
            }
        }
        if (ControllerPlayGame.instance._img_lose == true)
        {
            LevelGamePlay.instance.UI_img_YouLose.SetActive(false);
            ControllerPlayGame.instance._img_lose = false;
        }
        if (ControllerPlayGame.instance._img_win == true)
        {
            LevelGamePlay.instance.UI_img_YouWin.SetActive(false);
            ControllerPlayGame.instance._img_win = false;
        }
        x = 1;
        LevelGamePlay.instance._checkUI = false;
        GameManager.instance.SaveData();
        SceneManager.LoadScene("Home");
    }
    #endregion
    #region  Button Next Level 
    public void BtNextClick()
    {
        GameManager.instance.data._score = GameManager.instance.data._score + 1000;
        Slingshoot.instance.SetSlingshotLineRenderersActive(false);
        GameManager.instance.data._level = GameManager.instance.data._level + 1;

        // Cac button trong Menu WinLose = fasle
        WinLose.instance.Next.SetActive(false);
        WinLose.instance.Home.SetActive(false);
        // Giup cho viec chon Angry Bird ban tiep theo

        GameController.instance.stateAngryBird = Enums.StateAngryBird.Died;
        GameController.instance.stateGame = Enums.StateGame.Start;
        // LevelGamePlay.instance.AngryBirdOnShow.Clear();

        // Len Level moi 
        ControllerPlayGame.instance.LoadLevelGame(GameManager.instance.data._level);
        // ControllerPlayGame.instance.LoadLevelGame();

        imageFXnewLevel.SetActive(true);
        x = 1;
        FX_start();

        // Tim kiem Enemy, AngryBird o Level moi
        LevelGamePlay.instance.SeekEnemy();
        LevelGamePlay.instance.SeekAngryBird();


        LevelGamePlay.instance._checkUI = false;
    }
    #endregion
    #region Button Setting
    bool _stClick = false;

    public void BtSettingClick()
    {
        if (_pClick == false && LevelGamePlay.instance._checkUI == false)
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
    #region Xoa di cac Image cua Angry Bird khi amount bang 0

    public int sl;
    public void RemoveAngryBird()
    {
        sl = GameManager.instance.data.listAngryBird.Count;

        if (GameManager.instance.data.listAngryBird.Count > 0)
        {
            for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
            {
                if (GameManager.instance.data.listAngryBird[i]._amountAngryBird <= 0)
                {
                    UI_MenuButton[i].SetActive(false);
                    // GameManager.instance.data.listAngryBird.Remove(GameManager.instance.data.listAngryBird[i]);
                }
            }
        }
    }
    #endregion

}