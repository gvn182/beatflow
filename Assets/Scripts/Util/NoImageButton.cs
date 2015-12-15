using UnityEngine;
using System.Collections;

public class NoImageButton : MonoBehaviour
{

    int CurrentIndex = 0;
    public bool Clicked = false;
    public Camera myCamera;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Clicked)
        {
            Vector3 MousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = this.collider.bounds.center.z;

            if (Input.GetMouseButtonDown(0))
                if (this.collider.bounds.Contains(MousePos))
                    if (CurrentIndex == 0) CurrentIndex++;
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

    private void OnButtonClick()
    {
        CurrentIndex = 0;
        Clicked = true;
    }
}
