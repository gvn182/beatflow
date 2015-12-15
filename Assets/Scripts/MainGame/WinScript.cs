using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WinScript : MonoBehaviour
{

	public AudioClip MyWinSound;
	List<string> Sequencia;
	List<GameObject> Selecionados;
	int Stars;
	float Points;
	private bool appearing;
	private bool goingDown;
	private bool goingUp;
	public float MaxY;
	public float FinalPosition;
	public float AnimationVelocity;
	public GameObject WinScreen;
	public Button RetryButtonScript;
	public Button NextButtonScript;
	public Button LevelSelectScript;
	private bool animateStars;
	private int MaxLoop;
	private int CurrentStarAnimation;
	public GameObject StarObject;
	public List<Material> StarsMaterial = new List<Material> ();
	DateTime OldTime = DateTime.Now;
	float angleFrameHate = 70;
	private bool animateText;
	public List<Material> GoodText = new List<Material> ();
	public List<Material> GreatText = new List<Material> ();
	public List<Material> PerfectText = new List<Material> ();
	private int TextCurrentAnimation;
	private int MaxTextLoop;
	public GameObject TextObject;
	private bool PlayedWinSound;
	public int MyWorld;
	SaveState save;

	public void OnGameEnded (List<string> Sequencia, List<GameObject> Selecionados)
	{
		save = new SaveState ();
		this.gameObject.SetActive (true);
		this.Sequencia = Sequencia;
		this.Selecionados = Selecionados;
		Stars = CheckPoints ();
		save.OnLevelComplete (Application.loadedLevel, Stars);
		
		MaxLoop = (7 * Stars) - 1;
		MaxTextLoop = PerfectText.Count - 1;

      
	}

	private int CheckPoints ()
	{
		int PontosAtuais = 0;

		for (int i = 0; i < Selecionados.Count; i++) {
			if (i <= Sequencia.Count) {
				PontosAtuais += Selecionados [i].name.Equals (Sequencia [i]) ? 1 : -1;
			} else {
				PontosAtuais += -1;
			}

		}


		int PercentRight = PontosAtuais * 100 / Sequencia.Count;

		if (PercentRight >= 40 && PercentRight <= 70)
			return 1;
		else if (PercentRight >= 71 && PercentRight <= 99)
			return 2;
		else if (PercentRight >= 100)
			return 3;
		else
			return 0;
	}

	void Start ()
	{
		save = new SaveState ();
		appearing = true;
		goingDown = true;
		

	}

	void GoUp ()
	{
		if (WinScreen.transform.position.y <= FinalPosition) {
			WinScreen.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + (AnimationVelocity * Time.deltaTime)); 
		} else {
			goingUp = false;
			appearing = false;
		}
	}

	void GoDown ()
	{

		if (WinScreen.transform.position.y >= MaxY) {
			WinScreen.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - (AnimationVelocity * Time.deltaTime));
		} else {
			goingUp = true;
			goingDown = false;
			animateStars = true;
		}
	}

	void CheckWorldClick ()
	{
		if (LevelSelectScript.Clicked) {
			Resources.UnloadUnusedAssets ();
			switch (MyWorld) {
			case 1:
				Application.LoadLevel ("Classic Level Select");
				break;
			case 2:
				Application.LoadLevel ("Game Level Select");
				break;
			case 3:
				Application.LoadLevel ("Movie Level Select");
				break;
			case 4:
				Application.LoadLevel ("Pop Level Select");
				break;
			case 5:
				Application.LoadLevel ("Rock Level Select");
				break;
			}
		}
	
	}
	
	void Update ()
	{
		MakeAppear ();
		AnimateStars ();
		AnimateText ();
		CheckRetryClick ();
		CheckNextClick ();
		CheckWorldClick ();
		PlayWinSound ();

	}

	private void PlayWinSound ()
	{
		if (!PlayedWinSound) {
			audio.PlayOneShot (MyWinSound);
			PlayedWinSound = true;
		}

	}

	private void CheckNextClick ()
	{
		if (NextButtonScript.Clicked) {
			Resources.UnloadUnusedAssets ();
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}

	private void AnimateText ()
	{
		if (animateText) {
          
			if ((DateTime.Now - OldTime).TotalMilliseconds > angleFrameHate) {
				OldTime = DateTime.Now;
				if (TextCurrentAnimation < MaxTextLoop)
					TextCurrentAnimation++;
				else
					animateText = false;


				if (Stars == 1) {
					TextObject.renderer.material = GoodText [TextCurrentAnimation];
				}
				if (Stars == 2) {
					TextObject.renderer.material = GreatText [TextCurrentAnimation];
				}
				if (Stars == 3) {
					TextObject.renderer.material = PerfectText [TextCurrentAnimation];
				}

			}
          
		}
	}

	private void AnimateStars ()
	{
		if (animateStars) {

			if ((DateTime.Now - OldTime).TotalMilliseconds > angleFrameHate) {
				OldTime = DateTime.Now;
				if (CurrentStarAnimation < MaxLoop) {
					CurrentStarAnimation++;
					StarObject.renderer.material = StarsMaterial [CurrentStarAnimation];
				} else {
					animateStars = false;
					animateText = true;
				}
			}
		}
	}

	private void CheckRetryClick ()
	{
		if (RetryButtonScript.Clicked)
			Application.LoadLevel (Application.loadedLevel);
            
	}

	private void MakeAppear ()
	{
		if (appearing) {
			if (goingDown)
				GoDown ();

			if (goingUp)
				GoUp ();
		}
	}
}
