using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// De lu tru du lieu 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Data data;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // data._volume = 
    }
    void Start()
    {
        LoadData();
        
    }
    [ContextMenu("Load")]
    void LoadData()
    {
        if (PlayerPrefs.HasKey(nameof(Data)))
        {
            string _data = PlayerPrefs.GetString(nameof(Data));
            data = JsonUtility.FromJson<Data>(_data);
        }
        else
        {
            data = new Data();
        }
    }

    [ContextMenu("Save")]
    public void SaveData()
    {
        Debug.Log("Save");
        PlayerPrefs.SetString(nameof(Data), JsonUtility.ToJson(data));
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
