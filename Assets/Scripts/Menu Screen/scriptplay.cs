

using UnityEngine;
using System.Collections;

public class scriptplay : MonoBehaviour
{


	
	public GameObject p_playbutton;
	public Camera myCamera;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (!ClickedNext)
       // {
            Vector3 MousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = this.collider.bounds.center.z;

            

            if (Input.GetMouseButtonUp(0))
                    if (p_playbutton.collider.bounds.Contains(MousePos))
                        OnButtonClick();
                    
			
		
        

    }
	
    private void OnButtonClick()
    {

		Application.LoadLevel("World Selection"); 
		
    }
}

