using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

	public Material[] Materials = new Material[3];
	int CurrentIndex = 0;
	public bool Clicked = false;
	public Camera myCamera;
	public bool Disabled;

	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (!Disabled) {
			if (!Clicked) {
				Vector3 MousePos = myCamera.ScreenToWorldPoint (Input.mousePosition);
				MousePos.z = this.collider.bounds.center.z;

				if (Input.GetMouseButtonDown (0))
				if (this.collider.bounds.Contains (MousePos))
				if (CurrentIndex == 0)
					CurrentIndex++;
				else
					CurrentIndex = 0;


				if (Input.GetMouseButtonUp (0))
				if (CurrentIndex > 0)
				if (this.collider.bounds.Contains (MousePos))
					OnButtonClick ();
				else
					CurrentIndex = 0;
			}
		} else {
			CurrentIndex = 2;
		}
		
		this.renderer.material = Materials [CurrentIndex];

	}

	private void OnButtonClick ()
	{
		CurrentIndex = 0;
		Clicked = true;
	}
}
