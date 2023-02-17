using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelGamePlay : MonoBehaviour
{
    [Header("WinLose")]
    public GameObject UI_img_YouWin;
    public GameObject UI_img_YouLose;

    [Header("Next-Home")]

    [Header("Seek Enemy")]
    public List<GameObject> listEnemy;

    [Header("Angry Bird Show")]

    [Header("Seek Angry Bird")]
    public List<Bird> listAngryBirdSeek;
    public static LevelGamePlay instance;
    [Header("All UI")]
    public bool _checkUI;

    private void Awake()
    {
        instance = this;
        listAngryBirdSeek = new List<Bird>();
        listEnemy = new List<GameObject>();
    }

    [ContextMenu("Start")]
    private void Start()
    {
        ControllerPlayGame.instance.LoadLevelGame(GameManager.instance.data._level);
        SeekEnemy();
        if (GameManager.instance.data.listAngryBird.Count > 0)
        {
            SeekAngryBird();
        }
        else
        {
            UI_img_YouLose.SetActive(true);
        }
    }

    // Seek Enemy
    public void SeekEnemy()
    {
        listEnemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemy"));
    }

    // Tim cac con Angry Bird trong Game sau khi Play
    public void SeekAngryBird()
    {
        for (int i = 0; i < GameManager.instance.data.listAngryBird.Count; i++)
        {
            listAngryBirdSeek.Add(GameManager.instance.data.listAngryBird[i]);
        }
    }

    private void Update()
    {
        CheckAmountEnemy();
        RemoveAngryBirdNull();
    }

    public void RemoveAngryBirdNull()
    {
        if (listAngryBirdSeek.Count > 0)
        {
            for (int i = 0; i < listAngryBirdSeek.Count; i++)
            {
                if (listAngryBirdSeek[i]._amountAngryBird <= 0)
                {
                    listAngryBirdSeek.Remove(listAngryBirdSeek[i]);
                }
            }
        }
        else
        {
            if (listEnemy.Count > 0 && GameController.instance.stateAngryBird == Enums.StateAngryBird.Died)
            {
                if (ControllerPlayGame.instance._img_lose == false)
                {
                    Slingshoot.instance.SetSlingshotLineRenderersActive(false);
                    _checkUI = true;
                    UI_img_YouLose.SetActive(true);
                    WinLose.instance.Show();
                }
            }
        }
    }
    public void CheckAmountEnemy()
    {
        if (listEnemy.Count > 0)
        {
            for (int i = 0; i < listEnemy.Count; i++)
            {
                if (listEnemy[i] == null)
                {
                    listEnemy.Remove(listEnemy[i]);
                }
            }
        }
        else
        {
            if (ControllerPlayGame.instance._img_win == false)
            {
                Slingshoot.instance.SetSlingshotLineRenderersActive(false);
                UI_img_YouWin.SetActive(true);
                WinLose.instance.Show();
                _checkUI = true;
                ControllerPlayGame.instance._img_win = true;
            }
        }
    }



}
