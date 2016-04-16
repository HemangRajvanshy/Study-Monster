using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class Player : MonoBehaviour {

    public PlayerSave PlayerData { get; private set; }
    public GameSave GameData { get; private set;  }
    public bool PlayerDataExists { get { return PlayerDataExist(); } }

    private int ProgressIndex;

    void OnDestroy()
    {
        Save();
    }

    public void Init()
    {
        Load();

        GameData = new GameSave();
        GameData.ProgressIndex = 0;

        if (PlayerDataExist())
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file1 = File.Create(Application.persistentDataPath + "/game1.dat");
            formatter.Serialize(file1, GameData);
            file1.Close();

            FileStream file2 = File.Create(Application.persistentDataPath + "/game2.dat");
            formatter.Serialize(file2, GameData);
            file2.Close();

            FileStream file3 = File.Create(Application.persistentDataPath + "/game3.dat");
            formatter.Serialize(file3, GameData);
            file3.Close();
        }
    }

    public void SetProgress(int PIndex)
    {
        ProgressIndex = PIndex;
    }

    public void SaveGame(int saveNumber)
    {

    }

    public void LoadGame()
    {

    }

    public void GetSaveStatus(out bool save1, out bool save2, out bool save3)
    {
        save3 = save2 = save1 = false;
        if(PlayerDataExist())
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file1 = File.Open(Application.persistentDataPath + "/game1.dat", FileMode.Open);
            GameSave Save1 = (GameSave)formatter.Deserialize(file1);
            save1 = (Save1.ProgressIndex == 1);
            file1.Close();

            FileStream file2 = File.Open(Application.persistentDataPath + "/game2.dat", FileMode.Open);
            GameSave Save2 = (GameSave)formatter.Deserialize(file2);
            save2 = (Save2.ProgressIndex == 1);
            file2.Close();

            FileStream file3 = File.Open(Application.persistentDataPath + "/game3.dat", FileMode.Open);
            GameSave Save3 = (GameSave)formatter.Deserialize(file3);
            save3 = (Save3.ProgressIndex == 1);
            file3.Close();
        }
    }

    //Private Methods

    private void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/info.dat");

        PlayerSave save = new PlayerSave();
        save = WriteSave();

        formatter.Serialize(file, save);
        file.Close();
    }

    private void Load()
    {
        if (PlayerDataExist())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/info.dat", FileMode.Open);
            PlayerSave save = (PlayerSave)formatter.Deserialize(file);
            file.Close();

            InitializeFromSave(save);
        }
        else
        {
            PlayerData = null;
            ProgressIndex = 0;
        }
    }


    private void InitializeFromSave(PlayerSave Save)
    {
        PlayerData = Save;
       // ProgressIndex = PlayerData.ProgressIndex;
    }

    private PlayerSave WriteSave()
    {
        PlayerSave save = new PlayerSave();
        save.Music = Main.Instance.MusicMgr.On;
        save.Sfx = Main.Instance.SfxMgr.On;
       // save.ProgressIndex = ProgressIndex;
        return save;
    }

    private GameSave WriteGameSave()
    {
        return GameData;
    }

    private bool PlayerDataExist()
    {
        return File.Exists(Application.persistentDataPath + "/info.dat");
    }

}


[Serializable]
public class PlayerSave
{
    public bool Music;
    public bool Sfx; 
}

[Serializable]
public class GameSave
{
    public int ProgressIndex;
}