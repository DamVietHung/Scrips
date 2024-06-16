using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;
using System.Security.Cryptography;
using UnityEditor.Rendering;
using Random = UnityEngine.Random;
using System.IO;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject CenterCirCle;
    private IState currentState;
    private bool Zone = true;
    private Character target;

    public Character Target => target;
    private Vector2 waypoint;
    private Transform targetZone;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        if (FarCirCle())
        {
            Debug.LogWarning("okia");
            ChangeState(new BackToZoneState());
        }
        else
        if (Target != null)
        {
           
            if (IsTargetInRange())
            {
                ChangeState(new AttackState());
            }else{
                 ChangeState(new PatrolState());
            }
        }
        else
        {
            ChangeState(new PatrolState());
        }

    }
    public bool FarCirCle()
    {
        if (Vector2.Distance(transform.position, CenterCirCle.transform.position) >= 20f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Moving()
    {
        ChangeAnim("run");
        transform.position = Vector2.MoveTowards(transform.position, waypoint, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < 0.2f)
        {
            SetNewDestination();
        }
    }

    public void BackToCircle()
    {
        ChangeAnim("run");
        transform.position = Vector2.MoveTowards(transform.position, waypoint, moveSpeed * Time.deltaTime);
    }

    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void StopMoving()
    {
        ChangeAnim("dle");
        transform.position = Vector2.zero;
    }


    public void ChangeDirection(bool Zone)
    {
        this.Zone = Zone;
        transform.rotation = Zone ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
    private void SetNewDestination()
    {
        Vector3 Center = FindObjectOfType<Center>().transform.position;
        waypoint = (Vector2)Center + (Random.Range(1.25f, 5f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }


}
