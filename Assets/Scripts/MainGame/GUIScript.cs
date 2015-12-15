using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour
{
    private TextAsset MyMusic;
    public GUIStyle TextStyle;
    public GameObject BarFill;
    public LevelScript levelScript;
    public Camera thisCamera;

    bool play = true;
    int currIndex = 0;
    List<GameObject> SoundList = new List<GameObject>();
    List<int> TimeList = new List<int>();
    DateTime OldTime = DateTime.Now;
    DateTime NowTime = DateTime.Now;
    public Button PlayButtonScript;
    Vector3 BarPosition;
    float MinBar;
	int PontosAtuais;

    void Start()
    {
        thisCamera = Camera.main;
        MinBar = BarFill.transform.position.x;
        BarPosition = new Vector3(MinBar, BarFill.renderer.transform.localPosition.y, BarFill.renderer.transform.localPosition.z);
        levelScript = (LevelScript)thisCamera.gameObject.GetComponent(typeof(LevelScript));
        LoadSounds();
    }
    //void OnGUI()
    //{
        
        
    //  GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.03f, 3,3),"LEVEL 0");
    //}

    private void LoadSounds()
    {
        MyMusic = Resources.Load(Application.loadedLevelName, typeof(TextAsset)) as TextAsset;
        char[] archDelim = new char[] { '\r', '\n' };
        string[] content = MyMusic.text.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);

        for (int loop = 0; loop < content.Length; loop++)
        {
            if (loop % 2 == 0)
            {
                //AudioClip clipObj = Resources.Load("Sounds/Notes/" + content[loop], typeof(AudioClip)) as AudioClip;
                GameObject Note = GameObject.Find(content[loop].Trim());
                SoundList.Add(Note);
            }
            else
            {
                TimeList.Add(Convert.ToInt32(content[loop]));
            }
        }
    }

	void EnableDisableSubmit ()
	{
		int PercentRight = PontosAtuais * 100 / levelScript.Sequence.Count;
		
		levelScript.SubmitScript.Disabled = PercentRight >= 40 ? false : true;
	}

    void Update()
    {
        CheckPlayClick();

        CheckProgressBar();
		
		//EnableDisableSubmit();
		
        PlaySound();
    }
 
    private void CheckProgressBar()
    {
        float SpaceBetween = Math.Abs(MinBar);
        float AmountToIncrese = SpaceBetween / levelScript.Sequence.Count;
        PontosAtuais = 0;

        for (int i = 0; i < levelScript.Selecionados.Count; i++)
        {
            if (i <= levelScript.Sequence.Count)
            {
                PontosAtuais += levelScript.Selecionados[i].name.Equals(levelScript.Sequence[i]) ? 1 : -1;
            }
            else
            {
                PontosAtuais += -1;
            }
        }

        BarPosition.x = MinBar + (PontosAtuais * AmountToIncrese);
        BarFill.transform.localPosition = BarPosition;
    }

    private void PlaySound()
    {
        if (play)
        {
            if (currIndex < SoundList.Count)
            {
                if ((DateTime.Now - OldTime).TotalMilliseconds > TimeList[currIndex])
                {
                    NoteScript TheNote = (NoteScript)(SoundList[currIndex].GetComponent(typeof(NoteScript)));
                    
                    audio.PlayOneShot(TheNote.mySound, 100);
                    currIndex++;
                    OldTime = DateTime.Now;
                }
            }

        }
        if (currIndex >= SoundList.Count)
        {

            play = false;
            currIndex = 0;
        }
    }

    private void CheckPlayClick()
    {
        if (PlayButtonScript.Clicked)
        {
            if (!play)
            {
                play = true;
            }
            PlayButtonScript.Clicked = false;
        }
    }
}
