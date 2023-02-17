using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayGame : MonoBehaviour
{
    [Header("Level game")]
    public GameObject[] objectsLevelGame;
    public static ControllerPlayGame instance;

    [Header("Kiem tra da bat win-lose")]
    public bool _img_win;
    public bool _img_lose;

    public AudioSource auCoin;


    private void Awake()
    {
        instance = this;
    }

    public void LoadLevelGame(int _levelLoad)
    {
        if (GameManager.instance.data._level > 8)
        {
            GameManager.instance.data._level = 0;
            GameManager.instance.data._score = GameManager.instance.data._score + 10000;
        }
        //! LevelGamePlay.instance.listAngryBirdSeek.Clear();
        for (int i = 0; i < objectsLevelGame.Length; i++)
        {
            if (i == _levelLoad)
            {
                objectsLevelGame[i].SetActive(true);

                if (_img_win == true)
                {
                    LevelGamePlay.instance.UI_img_YouWin.SetActive(false);
                    _img_win = false;
                }
                if (_img_lose == true)
                {
                    LevelGamePlay.instance.UI_img_YouLose.SetActive(false);
                    _img_lose = false;
                }
            }
            else
            {
                objectsLevelGame[i].SetActive(false);
            }
        }
    }


}
