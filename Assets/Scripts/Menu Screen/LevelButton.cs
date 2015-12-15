	using System;
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

namespace AssemblyCSharp
{
    public class LevelButton : MonoBehaviour
    {
        public LevelSelect LvlSelect;
        public int MyLevelNumber;
        public Material MyLockedMaterial;
        public Material MyZeroStar;
        public Material MyZeroStar_Pressed;
        public Material MyOneStar;
        public Material MyOneStar_Pressed;
        public Material MyTwoStar;
        public Material MyTwoStar_Pressed;
        public Material MyThreeStar;
        public Material MyThreeStar_Pressed;
        private Material MyLevelNumberMaterial;
        Material[] mats;
        int MyStars;
        int CurrentIndex = 0;
        public bool Clicked = false;
        public Camera myCamera;



        void Start()
        {
            mats = new Material[2];
            MyLevelNumberMaterial = renderer.materials[1];
            mats[1] = MyLevelNumberMaterial;
            MyStars = LvlSelect.save.GetLevelStarsBy(MyLevelNumber);
            
        }
        void Update()
        {
            CheckClick();
            ChangeState();
           
        }

        void CheckClick()
        {
            if (!Clicked)
            {
                Vector3 MousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
                MousePos.z = this.collider.bounds.center.z;

                if (Input.GetMouseButtonDown(0))
                    if (this.collider.bounds.Contains(MousePos))
                        if (CurrentIndex == 0)
                            CurrentIndex++;
                        else
                            CurrentIndex = 0;


                if (Input.GetMouseButtonUp(0))
                    if (CurrentIndex > 0)
                        if (this.collider.bounds.Contains(MousePos))
                            OnButtonClick();
                        else
                            CurrentIndex = 0;
            }
        }

        void ChangeState()
        {
            Debug.Log(LvlSelect.CurrentLevel);
            if (MyLevelNumber <= LvlSelect.CurrentLevel )
            {
                switch (MyStars)
                {
                    case 0:
                        mats[0] = CurrentIndex > 0 ? MyZeroStar_Pressed : MyZeroStar;
                        break;
                    case 1:
                        mats[0] = CurrentIndex > 0 ? MyOneStar_Pressed : MyOneStar;
                        break;
                    case 2:
                        mats[0] = CurrentIndex > 0 ? MyTwoStar_Pressed : MyTwoStar;
                        break;
                    case 3:
                        mats[0] = CurrentIndex > 0 ? MyThreeStar_Pressed : MyThreeStar;
                        break;

                }
            }
            else
            {
                mats[0] = MyLockedMaterial;
                mats[1] = MyLockedMaterial;
            }
            renderer.materials = mats;
        }

        private void OnButtonClick()
        {
            CurrentIndex = 0;
            Clicked = false;
            if (!renderer.materials[0].name.ToUpper().Contains("LOCKED"))
            {
                Application.LoadLevel(MyLevelNumber);
            }
        }
    }
}
	
