using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class InGameDialogue : MonoBehaviour
{
    public GameDialogue[] Dialogues;
}

[Serializable]
public struct GameDialogue
{
    public List<String> Dialogues;
    public List<AudioClip> SfxList;
    public List<float> Delays;
}