using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Story : MonoBehaviour
{
    public StoryScene[] StoryScenes;
}

[Serializable]
public struct StoryScene
{
    public Sprite image;
    public List<String> Dialogues;
    public List<AudioClip> SfxList;
    public List<float> Delays;
}