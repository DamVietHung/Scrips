using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToZoneState : IState
{

     public void OnEnter(Enemy enemy){
        if(enemy.FarCirCle() ){
            enemy.BackToCircle();
        }
     }

    public void OnExcute(Enemy enemy){
        if (!enemy.FarCirCle()){
            enemy.ChangeState(new PatrolState());
        }                                   
    }

    public void OnExit(Enemy enemy){

    }
}
