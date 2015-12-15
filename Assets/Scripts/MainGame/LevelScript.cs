using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelScript : MonoBehaviour {

    
    private TextAsset MyMusic;
    public GameObject Notes;
    public GameObject TelaWin;
    public Vector3 LastPostion;
    public bool firstTouch;
    public List<GameObject> Selecionados = new List<GameObject>();
    public List<string> Sequence = new List<string>();
    public int CurrentLevel = 0;
    public Button SubmitScript;
    public bool Won;
    
    //SaveState Saves = new SaveState();
	void Start () {
        
        LoadSequence();

	}

    private void LoadSequence()
    {
        MyMusic = Resources.Load(Application.loadedLevelName, typeof(TextAsset)) as TextAsset;
        char[] archDelim = new char[] { '\r', '\n' };
        string[] content = MyMusic.text.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);



        for (int loop = 0; loop < content.Length; loop++)
        {
            if (loop % 2 == 0)
            {
                Sequence.Add(content[loop]);
            }
        }
    }
	
	void Update () {

        CheckSubmitClick();
        CheckWin();
	}

    private void CheckSubmitClick()
    {
        if (SubmitScript.Clicked)
        {
            if (!Won)
            {
                WinScript Script = (WinScript)TelaWin.gameObject.GetComponent(typeof(WinScript));
                Script.OnGameEnded(Sequence, Selecionados);
                Won = true;
                

            }
        }

    }

    private void CheckWin()
    {
        if (!Won)
        {
            if (Selecionados.Count == Sequence.Count)
            {
                for (int i = 0; i < Selecionados.Count; i++)
                {

                    if (!Selecionados[i].name.Equals(Sequence[i]))
                        return;

                }

                WinScript Script = (WinScript)TelaWin.gameObject.GetComponent(typeof(WinScript));
                Script.OnGameEnded(Sequence, Selecionados);
                Won = true;
            }
        }
    }
}
