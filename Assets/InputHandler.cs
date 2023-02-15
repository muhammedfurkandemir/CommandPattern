using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //handler(işleyici)
    public GameObject actor;
    Animator anim;
    Command keyQ, keyW, keyE,upArrow;//command sınıfı türünde verilerimizi oluşturuyoruz.bu veri yapısı miras verdiği sınıfları tür olarak tutabilir.
    List<Command> oldCommand = new List<Command>();
    Coroutine replayCoroutine;
    bool shouldStartReplay;
    bool isReplaying;
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
        if (!isReplaying)
            HandleInput();
        StartReplay();
        
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyQ.Execute(anim);
            oldCommand.Add(keyQ);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            keyW.Execute(anim);
            oldCommand.Add(keyW);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            keyE.Execute(anim);
            oldCommand.Add(keyE);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upArrow.Execute(anim);
            oldCommand.Add(upArrow);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldStartReplay = true;
        }
    }
    void StartReplay()
    {
        if (shouldStartReplay&&oldCommand.Count>0)
        {
            shouldStartReplay = false;
            if (replayCoroutine!=null)
            {
                StopCoroutine(replayCoroutine);
            }
            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }
    IEnumerator ReplayCommands()
    {
        isReplaying = true;
        for (int i = 0; i < oldCommand.Count; i++)
        {
            oldCommand[i].Execute(anim);
            yield return new WaitForSeconds(1f);
        }
        isReplaying = true;
    }
}
