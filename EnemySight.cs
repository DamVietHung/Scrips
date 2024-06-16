using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;
    public bool CheckSight = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {  
        if(other.tag == "Player"){
           
            enemy.SetTarget(other.GetComponent<Character>());
        }   
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player"){
            enemy.SetTarget(null);
        }
    }

}
