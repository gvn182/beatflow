using UnityEngine;
using System.Collections;



public class swipe : MonoBehaviour
{
	

    public GameObject p_object;
	
	public GameObject p_bloco1;
	public GameObject p_bloco2;
	public GameObject p_bloco3;
	public GameObject p_bloco4;
	public GameObject p_bloco5;
	
    private float oldMousePos = 0;
    private float newMousePos;
    private bool touched = false;
	private bool atualiza=false;
	private int p_posicaoatual = 0;
	public int Velocidade;
	private float posicaocamera;


    Camera myCamera;
    void Start()
    {
        myCamera = Camera.main;
    }

    
	 public int Max(float fase1, float fase2, float fase3, float fase4,float fase5)
    {
		
		float resultado =  Mathf.Min(fase1, Mathf.Min(fase2, Mathf.Min(fase3, Mathf.Min(fase4, fase5))));
		
		if (resultado == fase1)
			return 1;
		if (resultado == fase2)
			return 2;
		if (resultado == fase3)
			return 3;
		if (resultado == fase4)
			return 4;
		if (resultado == fase5)
			return 5;
		
		return 0;
         
    }
    void Update()
    {


		
        if (Input.GetMouseButtonUp(0))
        {	
			if (touched)
			{
				posicaocamera = p_object.transform.position.y  /  myCamera.ScreenToWorldPoint(Input.mousePosition).y;
				atualiza=true;
			}
            touched = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (touched)
            {
                newMousePos = myCamera.ScreenToWorldPoint(Input.mousePosition).y;
                p_object.transform.position = new Vector2(0,p_object.transform.position.y + (newMousePos - oldMousePos));
                oldMousePos = newMousePos;
				
            }
            else
            {
                touched = true;
                oldMousePos = myCamera.ScreenToWorldPoint(Input.mousePosition).y;
				
            }
        }else
		{
			
			float fase1=0,fase2=0,fase3=0,fase4=0,fase5=0;
			
			if (oldMousePos < 0)
				oldMousePos =  -1 * oldMousePos;
			
			
			if (p_bloco1.transform.position.y < 0)
				fase1 = (-1 * p_bloco1.transform.position.y) - oldMousePos;
			else
				fase1 = (p_bloco1.transform.position.y) - oldMousePos;
			
			if (p_bloco2.transform.position.y < 0)
				fase2 = (-1 * p_bloco2.transform.position.y) - oldMousePos;
			else
				fase2 = (p_bloco2.transform.position.y) - oldMousePos;
			
			
			if (p_bloco3.transform.position.y < 0)
				fase3 = (-1 * p_bloco3.transform.position.y) - oldMousePos;
			else
				fase3 = (p_bloco3.transform.position.y) - oldMousePos;
			
			if (p_bloco4.transform.position.y < 0)
				fase4 = (-1 * p_bloco4.transform.position.y) - oldMousePos;
			else
				fase4 = (p_bloco4.transform.position.y) - oldMousePos;
			
			
			if (p_bloco5.transform.position.y < 0)
				fase5 = (-1 * p_bloco5.transform.position.y) - oldMousePos;
			else
				fase5 = (p_bloco5.transform.position.y) - oldMousePos;
			
			
			switch (Max (fase1,fase2,fase3,fase4,fase5)) {
			case 1:
				p_object.transform.position = Vector3.MoveTowards(p_object.transform.position, new Vector3(0,0), Velocidade * Time.deltaTime);
				break;
			case 2:
				p_object.transform.position = Vector3.MoveTowards(p_object.transform.position, new Vector3(0,-160), Velocidade * Time.deltaTime);
				break;
			case 3:
				p_object.transform.position = Vector3.MoveTowards(p_object.transform.position, new Vector3(0,-320), Velocidade * Time.deltaTime);
				break;
			case 4:
				p_object.transform.position = Vector3.MoveTowards(p_object.transform.position, new Vector3(0,-480), Velocidade * Time.deltaTime);
				break;	
			case 5:
				p_object.transform.position = Vector3.MoveTowards(p_object.transform.position, new Vector3(0,-640), Velocidade * Time.deltaTime);
				break;
			}
			 
			
			
			return;
			
		}
		 
    }
    


}