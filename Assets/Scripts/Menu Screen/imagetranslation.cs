using UnityEngine;
using System.Collections;

public class imagetranslation : MonoBehaviour {
	
	public GameObject p_object;
	public GameObject p_Botoes;
	public GameObject p_log;
	
	public Texture[] p_texture;
	private int indexImage;
	private int frameCounter = 0;  
	// Use this for initialization
	void Start () {
	

	}
	

	 IEnumerator PlayLoop(float delay)  
	{
		
		
        //wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);    
        //Wait for the time defined at the delay parameter  
          
  		if (frameCounter != p_texture.Length - 1)
		{
        //Advance one frame  
        frameCounter = (++frameCounter) % p_texture.Length; 
		
		Texture dd = p_texture[frameCounter];
		
	
		renderer.materials[0].mainTexture = dd;
		p_Botoes.transform.position = Vector3.MoveTowards(p_Botoes.transform.position, new Vector3(0,-50), 150 * Time.deltaTime);
		p_log.transform.position = Vector3.MoveTowards(p_log.transform.position, new Vector3(-45,85), 150 * Time.deltaTime);

			//-50

			
		}
  
        //Stop this coroutine  
        StopCoroutine("PlayLoop");  
		
    }    
  
	// Update is called once per frame
	void Update () {
		
        StartCoroutine("PlayLoop",0.018f ) ;  
        //Set the material's texture to the current value of the frameCounter variable  
	
	
	 
     
	
	}
}
