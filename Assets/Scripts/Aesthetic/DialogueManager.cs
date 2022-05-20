using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A universal aethetical script written by Shuangyuan Cao for NetEase MINI-GAME GameJam in 2021.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public float interval = 0.05f;
    int p = 0, l =0;
    string speaker, content;
    string[] contents;
    string display;
    public float time;
    public bool isStart, isEnd;

    public Text displayLine, speakerName;
    public bool lineEnd = false, speaking = false;
    bool hasSign = false;
    string boolName = "";
    public AudioSource sound;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        display = "";
        content = "";
        contents = new string[1];
        contents[0] = "";
    }

    void Update()
    {
        if(lineEnd && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Return)) && speaking) {
            l ++;
            if(hasSign) {
                hasSign = false;
            }
            if(!isEnd && l < contents.Length) {
                SpeakLine(l);
            }else {
                EndDialog();
            }
        }

        if (!speaking) gameObject.SetActive(false);

        time += Time.deltaTime;
        if(time > interval) {
            if(display.Length < content.Length && p <content.Length) {
                display += content[p];
                p ++;
                displayLine.text = display;
                if(sound != null) {
                    sound.volume = GameManager.sfxVolume;
                    sound.Play();
                }
            }else {
                lineEnd = true;
            }
            time = 0.0f;
        }
    }

    public void Procceed()
    {
        if (lineEnd && speaking)
        {
            l++;
            if (hasSign)
            {
                hasSign = false;
            }
            if (!isEnd && l < contents.Length)
            {
                SpeakLine(l);
            }
            else
            {
                EndDialog();
            }
        }
    }

    void EndDialog()
    {
        isStart = false;
        isEnd = false;
        time = 0.0f;
        content = "";
        speaker = "";
        PlayerBehaviour.instance.inConversation = false;
        GameManager.instance.state = 0;
        display = "";
        speaking = false;
    }

    public void SpeakLine(int i)
    {
        displayLine.text = "";
        speakerName.text = "";

        lineEnd = false;
        p = 0;
        content = contents[i];
        display = "";
        Speak(content);
        speakerName.text = speaker;
        if(isStart) {
            gameObject.SetActive(true);
        }
    }

    void Speak(string s)
    {
        string newSpeaker = "";

        if(s.Contains("/s")) {
            s = s.Substring(2);
            isStart = true;
        }else {
            isStart = false;
        }
        if(s.Contains("/e")) {
            s = s.Substring(0, s.Length - 2);
            isEnd = true;
        }else {
            isEnd = false;
        }
        if(s.Contains("/n") && s.Contains("/m")) {
            int nameEnd = s.IndexOf("/m");
            newSpeaker = s.Substring(2, nameEnd - 2);
            s = s.Substring(nameEnd + 2);
        }
        if(s.Contains("/b") && s.Contains("/v")) {
            int nameEnd = s.IndexOf("/v");
            int nameStart = s.IndexOf("/b");
            boolName = s.Substring(nameStart + 2, nameEnd - nameStart - 2);
            hasSign = true;
            s = s.Substring(0, nameStart);
        }
        content = s;
        speaker = newSpeaker + ":";
        Debug.Log(speaker+":\""+s+"\""+"\nIs End:"+isEnd+" Is Start:"+isStart);
    }

    /// <summary>
    /// To speak a line:
    /// /s at beginning means this is the first sentence.
    /// /e at end means this is the end.
    /// /n follows /s and should indicate the speaker's name.
    /// /m is followed by the message being said.
    /// </summary>
    public void StartDialog(string[] _contents)
    {
        if (speaking) return;

        speaking = true;
        l = 0;
        this.contents = _contents;
        PlayerBehaviour.instance.inConversation = true;
        GameManager.instance.state = 1;
        SpeakLine(l);
    }

}
