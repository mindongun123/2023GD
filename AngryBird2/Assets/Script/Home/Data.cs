using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Data
{
    [Header("Score")]
    public int _score;


    [Header("Save Angry Bird")]
    public List<Bird> listAngryBird = new List<Bird>();

    [Header("Start Game The One")]
    public bool _startGameTheOne = false;

    [Header("Level Game")]
    public int _level;

    [Header("Volume")]
    [Range(0, 1)]
    public float _volume;
    public Data()
    {
        // Score mac dinh khi moi vao Game la 100
        _score = 2000;
        // ListAngryBird 
        _volume = 1;
    }
}
