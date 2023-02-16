using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command//sanal sınıf oluşturarak imza sınıfımızla classlarımza imzalarını vereceğiz.
{
    public abstract void Execute(Animator anim,bool forward);  

}
public class MoveForward : Command
{
    public override void Execute(Animator anim, bool forward)//sınıflar bu şekilde abstract metodu override ederek soyut bi şekilde
    {                                                        //input handler sınıfımızda bu yapıları kullanacağız ve bu sınıflarımızı 
        if (forward)                                         //listeyle oluşturacağımız veri yapımızla saklayabileceğiz.
            anim.SetTrigger("isWalking");
        else
            anim.SetTrigger("isWalkingR");
    }
}

public class PerformJump : Command
{
    public override void Execute(Animator anim, bool forward)
    {        
        if (forward)
            anim.SetTrigger("isJumping");
        else
            anim.SetTrigger("isJumpingR");
    }
}
public class PerformPunch : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("isPunching");
        else
            anim.SetTrigger("isPunchingR");
    }
}
public class PerformKick : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        if (forward)
            anim.SetTrigger("isKicking");
        else
            anim.SetTrigger("isKickingR");
    }
}

public class DoNothing : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        
    }
}
