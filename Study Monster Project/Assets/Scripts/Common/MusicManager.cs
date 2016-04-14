using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public bool On { get; private set; }
    public AudioSource Music;

    public AudioClip MenuBgMusic;
    public AudioClip GameBgMusic;

    void Awake()
    {
        if (Main.Instance.PlayerData != null)
            On = Main.Instance.PlayerData.Music;
        else
            On = true;
    }

    void Start()
    {
        Play(MenuBgMusic);
    }

    public void Play(AudioClip clip)
    {
        Music.clip = clip;
    }

    public void MenuToggle(bool value)
    {
        On = value;
    }
}
