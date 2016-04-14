using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public bool On { get; private set; }
    public AudioSource Music;

    public AudioClip MenuBgMusic;
    public AudioClip GameBgMusic;

    public void Initialize()
    {
        if (Main.Instance.PlayerData != null)
            On = Main.Instance.PlayerData.Music;
        else
            On = true;

        if (!On)
            Music.mute = true;

        Play(MenuBgMusic);
    }

    public void Play(AudioClip clip)
    {
        Music.clip = clip;
    }

    public void MenuToggle(bool value)
    {
        On = value;
        Music.mute = On;
    }
}
