using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextType : MonoBehaviour {

    [HideInInspector]
    public bool Typing = false;
    public float LetterPerSec = 50f;

    public IEnumerator TypeText(Text Dialogue, string text, float wait = 0f)
    {
        Dialogue.text = "";
        Typing = true;
        yield return new WaitForSeconds(wait);
        foreach (char letter in text)
        {
            if (Typing)
            {
                Dialogue.text += letter;
                if (Main.Instance.SfxMgr.TypingSfx)
                    Main.Instance.SfxMgr.Play(Main.Instance.SfxMgr.TypingSfx);
                if (letter == '.' || letter == '?')
                    yield return new WaitForSeconds(0.1f);
                yield return new WaitForSeconds(1 / LetterPerSec);
            }
            else
            {
                Dialogue.text = text;
                break;
            }
        }
        Typing = false;
    }

    public void StopTyping()
    {
        Typing = false;
    }

}
