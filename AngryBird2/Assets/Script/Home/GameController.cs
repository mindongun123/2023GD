using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float _volumeGame;
    private void Awake()
    {
        instance = this;
    }
    [Header("Enums")]
    public StateGame stateGame;
    public StateSlingshot stateSlingshoot;
    public StateAngryBird stateAngryBird;

    private void Start()
    {
        stateSlingshoot = Enums.StateSlingshot.AfterShoot;
    }
    public AudioSource auCoin;

    private void Update() {
        GameManager.instance.data._volume= _volumeGame;
    }

}
