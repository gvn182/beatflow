using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class LevelSelect : MonoBehaviour
	{
		public int MyWorldNumber;
		public SaveState save;
		public int CurrentLevel;
		public Button BackButton;

		void Awake ()
		{
			save = new SaveState ();
            CurrentLevel = save.GetCurrentLevel();
		}

		void Update ()
		{
			if (BackButton.Clicked) {
				
				Application.LoadLevel ("World Selection");
			}
		}
	}
}

