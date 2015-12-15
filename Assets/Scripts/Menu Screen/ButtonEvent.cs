

using UnityEngine;
using System.Collections;

public class ButtonEvent : MonoBehaviour
{


    public NoImageButton p_fita;
    public NoImageButton p_legenda;
    public Button p_butnext;
    public Button p_butprev;
    public Texture[] Fitas;
    public Texture[] Legendas;
    public Texture Blocked;
    int frameCounter = 0;
    public bool ClickedNext = false;
    public bool ClickedPrev = false;
    public Camera myCamera;
    SaveState save = new SaveState();
    int AllStars = 0;
    void Start()
    {
        AllStars = save.GetAllStars();
    }

    void Update()
    {
        if (p_butnext.Clicked)
        {
            p_butnext.Clicked = false;
            OnButtonClickNext();

        }
        if (p_butprev.Clicked)
        {
            p_butprev.Clicked = false;
            OnButtonClickPrev();
        }
        if (p_fita.Clicked)
        {
            p_fita.Clicked = false;
            LoadLevel();

        }
        if (p_legenda.Clicked)
        {
            p_legenda.Clicked = false;
            LoadLevel();
        }

    }
    private void LoadLevel()
    {
        if (CheckOpen())
        {
            if (Fitas[frameCounter].name.Contains("rock"))
            {
                Application.LoadLevel("Rock Level Select");
            }
            else if (Fitas[frameCounter].name.Contains("classical"))
            {
                Application.LoadLevel("Classic Level Select");
            }
            else if (Fitas[frameCounter].name.Contains("games"))
            {
                Application.LoadLevel("Game Level Select");
            }
            else if (Fitas[frameCounter].name.Contains("pop"))
            {
                Application.LoadLevel("Pop Level Select");
            }
            else // (Fitas[frameCounter].name.Contains("movies"))
            {
                Application.LoadLevel("Movie Level Select");
            }
        }
    }
    private void OnButtonClickPrev()
    {
        if (frameCounter > 0)
        {
            frameCounter -= 1;
        }


        if (!CheckOpen())
        {

            p_fita.renderer.material.mainTexture = Blocked;
            p_legenda.renderer.material.mainTexture = Legendas[frameCounter];
            return;
        }
        p_fita.renderer.material.mainTexture = Fitas[frameCounter];
        p_legenda.renderer.material.mainTexture = Legendas[frameCounter];


    }

    private bool CheckOpen()
    {
        switch (frameCounter)
        {
            case 0: return true;
            case 1: if (save.GetAllStars() >= SaveState.UnlockLevel1) return true; return false;
            case 2: if (save.GetAllStars() >= SaveState.UnlockLevel2) return true; return false;
            case 3: if (save.GetAllStars() >= SaveState.UnlockLevel3) return true; return false;
        }
        return false;
    }
    private void OnButtonClickNext()
    {
        if (frameCounter < Fitas.Length - 1)
        {
            frameCounter += 1;
        }
        if (!CheckOpen())
        {
            p_fita.renderer.material.mainTexture = Blocked;
            p_legenda.renderer.material.mainTexture = Legendas[frameCounter];
            return;
        }
        p_fita.renderer.material.mainTexture = Fitas[frameCounter];
        p_legenda.renderer.material.mainTexture = Legendas[frameCounter];
    }
}

