using UnityEngine;
using System.Collections;
using System;

public class NoteScript : MonoBehaviour
{

    // Use this for initialization
    public Material[] Sybol = new Material[2];
    public LevelScript levelScript;
    public Material[] Animations = new Material[2];
    NoteScript Scrt;
    public LineRenderer Line;
    private LineRenderer myLine;
    public Camera thisCamera;
    public bool OriginChecked = false;
    public bool DestinyChecked = false;
    GameObject[] Sphere;
    Vector3 Origin;
    Vector3 Destiny;
    private float theZ;
    public AudioClip mySound;
    public bool touched = false;
    private bool Boucing;
    private Vector3 MousePos;
    Material[] mats;
    private Vector3 LinePos;
    void Start()
    {
        Line = (LineRenderer)GameObject.FindObjectOfType(typeof(LineRenderer));
        thisCamera = Camera.main;
        levelScript = (LevelScript)thisCamera.gameObject.GetComponent(typeof(LevelScript));
        myLine = (LineRenderer)Instantiate(Line, new Vector3(0, 0, 0), Quaternion.identity);
        Sphere = GameObject.FindGameObjectsWithTag("Respawn");
        theZ = Sphere[0].collider.bounds.center.z;
        LinePos = new Vector3();
        mats = renderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelScript.Won)
            return;

        MousePos = thisCamera.ScreenToWorldPoint(Input.mousePosition);
        MousePos.z = theZ;

        if (Input.GetMouseButtonDown(0))
        {

            touched = true;

            if (!levelScript.firstTouch)
            {
                if (this.collider.bounds.Contains(MousePos))
                {
                    levelScript.firstTouch = true;
                    ClearAll();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            touched = false;
            if (!OriginChecked || !DestinyChecked)
            {
                OriginChecked = false;
                DestinyChecked = false;
                myLine.SetPosition(0, Vector3.zero);
                myLine.SetPosition(1, Vector3.zero);
                levelScript.LastPostion = Vector3.zero;
                levelScript.firstTouch = false;
            }
        }

        if (touched)
        {
            if (this.collider.bounds.Contains(MousePos))
            {
                if (levelScript.LastPostion.x == 0)
                {

                    Origin = this.renderer.bounds.center;
                    levelScript.LastPostion = this.transform.position;
                    LinePos.x = Origin.x;
                    LinePos.y = Origin.y;
                    LinePos.z = 10;
                    myLine.SetPosition(0, LinePos);
                    if (!OriginChecked)
                    {
                        levelScript.Selecionados.Add(this.gameObject);
                        PlayAnimation();
                    }

                    OriginChecked = true;
                }
                else
                {
                    if (levelScript.LastPostion.x == transform.position.x || levelScript.LastPostion.y == transform.position.y)
                    {
                        Origin = this.renderer.bounds.center;
                        levelScript.LastPostion = this.transform.position;
                        LinePos.x = Origin.x;
                        LinePos.y = Origin.y;
                        LinePos.z = 10;
                        myLine.SetPosition(0, LinePos);
                        if (!OriginChecked)
                        {
                            levelScript.Selecionados.Add(this.gameObject);
                            PlayAnimation();

                        }

                        OriginChecked = true;
                    }
                }

            }
            if (OriginChecked)
            {
                if (!DestinyChecked)
                {
                    Destiny = MousePos;

                    LinePos.x = Destiny.x;
                    LinePos.y = Destiny.y;
                    LinePos.z = 10;
                    myLine.SetPosition(1, LinePos);

                    for (int i = 0; i < Sphere.Length; i++)
                    {
                        if (Sphere[i].collider.bounds != this.collider.bounds) //regra nao ligar com ele mesmo
                        {

                            if (Sphere[i].collider.bounds.Contains(Destiny)) //Se esta dentro de algum quadrante
                            {
                                if (transform.position.x == Sphere[i].transform.position.x || transform.position.y == Sphere[i].transform.position.y) //regra estar em CRUZ
                                {
                                    NoteScript Script = (NoteScript)Sphere[i].GetComponent(typeof(NoteScript));
                                    if (!Script.DestinyChecked) //regra ligar na mesma linha os ja ligados
                                    {
                                        Destiny = Sphere[i].collider.bounds.center;
                                        Destiny.z = 10;
                                        myLine.SetPosition(1, Destiny);
                                        levelScript.LastPostion = Sphere[i].transform.position;
                                        DestinyChecked = true;

                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        if (OriginChecked)
        {
            
            mats[0] = Animations[1];
            mats[1] = Sybol[1];
            renderer.materials = mats;
        }
        else
        {
            mats[0] = Animations[0];
            mats[1] = Sybol[0];
            renderer.materials = mats;
        }
        
    }

    //private void Bounce()
    //{
    //    if (Boucing)
    //    {
    //        if ((DateTime.Now - OldAngleTime).TotalMilliseconds > angleFrameHate)
    //        {
    //            OldAngleTime = DateTime.Now;

    //            if (currentAnim < Animations.Length)
    //            {

    //                currentAnim++;
    //                this.renderer.material = Animations[currentAnim];
    //            }
    //            else
    //            {
    //                currentAnim = 0;
    //                this.renderer.material = Animations[currentAnim];
    //                Boucing = false;
    //            }
    //        }

    //    }
    //}

    private void PlayAnimation()
    {
        audio.PlayOneShot(mySound, 100);
        //Boucing = true;
    }

    private void ClearAll()
    {
        foreach (GameObject item in Sphere)
        {
            NoteScript Script = (NoteScript)item.GetComponent(typeof(NoteScript));

            Script.OriginChecked = false;
            Script.DestinyChecked = false;
            Script.myLine.SetPosition(0, Vector3.zero);
            Script.myLine.SetPosition(1, Vector3.zero);

        }
        levelScript.Selecionados.Clear();

    }


}
