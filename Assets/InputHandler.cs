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
        if (Input.GetKeyDown(KeyCode.Q))//zıplama
        {
            keyQ.Execute(anim,true);
            oldCommand.Add(keyQ);
        }
        else if (Input.GetKeyDown(KeyCode.W))//yumruk
        {
            keyW.Execute(anim,true);
            oldCommand.Add(keyW);
        }
        else if (Input.GetKeyDown(KeyCode.E))//tekme
        {
            keyE.Execute(anim,true);
            oldCommand.Add(keyE);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))//yürüme
        {
            upArrow.Execute(anim,true);
            oldCommand.Add(upArrow);
        }
        else if (Input.GetKeyDown(KeyCode.Space))//hareketin tekrar etmesini tetikler.
        {
            shouldStartReplay = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z))//hareketin tersini tetikler.
        {
            UndoLastCommand();
        }
    }
    void UndoLastCommand()//mevcut hareketin tersini yaptırır.bunuda kaydettiğimiz hareketin animatorde ters çalışmasını kullanarak yaptırırız.
        //ters çalışmasıda animasyonun speed değerini -1 yaptığımızda animasyon sondan başa doğru sarılır.
    {
        if (oldCommand.Count>0)
        {
            Command c = oldCommand[oldCommand.Count - 1];
            c.Execute(anim, false);
            oldCommand.RemoveAt(oldCommand.Count - 1);
        }       
    }
    void StartReplay()
    {
        if (shouldStartReplay&&oldCommand.Count>0)
        {
            shouldStartReplay = false;
            if (replayCoroutine!=null)//oynatılacak animasyon kalmadığında coroutine durdurur.
            {
                StopCoroutine(replayCoroutine);
            }
            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }
    IEnumerator ReplayCommands()
    {
        isReplaying = true;//bu şekilde klavyeden başka input girişini bu değişkenle kapatırız.
        for (int i = 0; i < oldCommand.Count; i++)//döngüyle baştan başlayarak kaydettiğimiz animasyonları oynatırız.
        {
            oldCommand[i].Execute(anim,true);
            yield return new WaitForSeconds(1f);//her animasyon arasına 1 saniye bekleme süresi verir.
        }
        isReplaying = true;
    }
}
