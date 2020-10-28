using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalHealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private Rigidbody rBody;
    public float health = 100f;

    public bool is_Player, is_Zombie;

    private bool is_Dead;

    // private PlayerStats player_Stats;


    private EnemyAudio enemyAudio;
    void Awake()
    {
        if (is_Zombie)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            // get enemy audo
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            rBody = GetComponent<Rigidbody>();
        }
        if (is_Player)
        {
          //  player_Stats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        //if we died dont execute the rest of code
        if (is_Dead)
            return;

        health -= damage;
        if (is_Player)
        {
            //show the stats(UI health value)
         //   player_Stats.Display_HealthStats(health);
        }
        if (is_Zombie)
        {

            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;

            }
        }

        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }

    }// apply damage

    void PlayerDied()
    {
        if (is_Zombie)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();
        }// zombie died


        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
        }// player died
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    } // PlayerDied

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FPS shooter");
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

}
