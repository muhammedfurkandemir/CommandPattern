using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //handler(işleyici)
    public GameObject actor;
    Animator anim;
    Command keyQ, keyW, keyE,upArrow;//command sınıfı türünde verilerimizi oluşturuyoruz.bu veri yapısı miras verdiği sınıfları tür olarak tutabilir.
    void Start()
    {
        keyQ = new PerformJump();
        keyW = new PerformPunch();
        keyE = new PerformKick();
        upArrow = new MoveForward();
        anim = actor.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyQ.Execute(anim);            
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            keyW.Execute(anim);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            keyE.Execute(anim);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            upArrow.Execute(anim);
        }
    }
}
