using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class Player : MonoBehaviour {

    public PlayerSave PlayerData { get; private set; }
    public GameSave GameData { get; private set;  }
    public bool PlayerDataExists { get { return PlayerDataExist(); } }

    private int ActiveSaveNumber;
    private int ProgressIndex;

    void OnDestroy()
    {
        Save();
    }

    public void Init()
    {
        Load();

        GameData = new GameSave();

        if (!PlayerDataExist())
        {
            ResetGameSave(1);
            ResetGameSave(2);
            ResetGameSave(3);
        }
    }

    public void ResetGameSave(int SaveNum)
    {
        GameData.ProgressIndex = 0;
        GameData.SceneLocation = "01-Start";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file1 = File.Create(Application.persistentDataPath + "/game" + SaveNum.ToString() + ".dat");
        formatter.Serialize(file1, GameData);
        file1.Close();
    }

    public void SetProgress(int PIndex)
    {
        ProgressIndex = PIndex;
    }

    public void SaveGame() // Save the current GameData to permanent memory.
    {
        GameData.ProgressIndex = ProgressIndex;
        GameData.SceneLocation = Main.Instance.SceneMgr.ActiveScene;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game" + ActiveSaveNumber.ToString() + ".dat");
        GameSave save = GameData;

        formatter.Serialize(file, save);
        file.Close();
    }

    public void LoadGame(int saveNumber)
    {
        ActiveSaveNumber = saveNumber;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/game" + saveNumber.ToString() + ".dat", FileMode.Open);
        GameData = (GameSave)formatter.Deserialize(file);
        file.Close();

        ProgressIndex = GameData.ProgressIndex;
    }

    public void GetSaveStatus(out bool save1, out bool save2, out bool save3) // Check which saves exist and which are empty
    {
        save3 = save2 = save1 = false;
        if(PlayerDataExist())
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file1 = File.Open(Application.persistentDataPath + "/game1.dat", FileMode.Open);
            GameSave Save1 = (GameSave)formatter.Deserialize(file1);
            save1 = (Save1.ProgressIndex > 0);
            file1.Close();

            FileStream file2 = File.Open(Application.persistentDataPath + "/game2.dat", FileMode.Open);
            GameSave Save2 = (GameSave)formatter.Deserialize(file2);
            save2 = (Save2.ProgressIndex > 0);
            file2.Close();

            FileStream file3 = File.Open(Application.persistentDataPath + "/game3.dat", FileMode.Open);
            GameSave Save3 = (GameSave)formatter.Deserialize(file3);
            save3 = (Save3.ProgressIndex > 0);
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
    public string SceneLocation;
}