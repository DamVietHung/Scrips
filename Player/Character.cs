using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float hp;
    public bool IsDead => hp <=0;
    private string currentAnimName;
    void Start()
    {
        OnInit();
    }

    public virtual void OnInit(){
         hp = 100;
    }

    public virtual void OnDespawn(){
        
    }

public virtual void OnDeath(){
         ChangeAnim("die");
         Invoke(nameof(OnDespawn),2.5f);
    }

    protected void ChangeAnim(string animName){
        if (currentAnimName != animName){
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }
    // Update is called once per frame
    public void OnHit(float damage)
    {
        if(!IsDead){
            hp-= damage;
            if(IsDead){
                OnDeath();
            }
        }
    }
}
