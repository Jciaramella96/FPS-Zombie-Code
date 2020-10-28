using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
   private UnityEngine.AI.NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private EnemyState enemy_State;
    private EnemyManagerZombie enemy_Manager_Zombie;
    public float walk_Speed = 0.5f;

    public float health = 100f;

    public float run_Speed = 4f;

    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    private EnemyAudio enemy_Audio;


    private bool is_Dead;


    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemy_Controller = GetComponent<EnemyController>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enemy_Audio = GetComponentInChildren<EnemyAudio>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        //  enemy_State = EnemyState.PATROL;
        enemy_State = EnemyState.CHASE;
       

        //when enemy first gets to play attack right away
        attack_Timer = wait_Before_Attack;

        //memorize the value of chasedistance 
        // so that we can put it back
        current_Chase_Distance = chase_Distance;


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (enemy_State== EnemyState.CHASE)
        {
            Chase();
        }

      if(enemy_State== EnemyState.ATTACK)
        {
            Attack();
        }
    }


    void Chase()
    {
        //enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;
        //set the players position as teh destination
        // because running towards player
        navAgent.SetDestination(target.position);

        // if distance btween enemy and player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            // stop the animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chance distance to previous

        }

        if (navAgent.velocity.sqrMagnitude > 0 )
        {
            
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);

        }
        
        
        
    }// chase

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
                enemy_Anim.Attack();
            //reset attack timer
          
            //play attack sound
          //  enemy_Audio.Play_AttackSound();
        
        if(Vector3.Distance(transform.position, target.position)> attack_Distance)// + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }


    }//attack
    
   public void DivideStopping()
    {
        navAgent.stoppingDistance = navAgent.stoppingDistance / 2;
    }

    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }

    }


    public EnemyState Enemy_State
    {
        get; set;
    }

    public void  TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !is_Dead)
        {
            is_Dead = true;
            Die();
        }
    }

    void Die()
    {

        EnemyManagerZombie.totalZombies--;
        print("Zombies remaining " + EnemyManagerZombie.totalZombies);
        StartCoroutine(DeathAnim());

        //Destroy(gameObject);


    }

    IEnumerator DeathAnim()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        enemy_Controller.enabled = false;

        enemy_Anim.Dead();

        yield return new WaitForSeconds(1f);



        Destroy(gameObject);


    }
}// class
