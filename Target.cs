
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class Target : MonoBehaviour
{
    
    private EnemyAnimator enemy_Anim;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    private Rigidbody rBody;
    public float health = 100f;
    private float healthRegen = 10;

    public int zombieKilledCount;
   

    public PlayerStats player_Stats;
    public bool is_Zombie, is_Player;
    private bool is_Dead;

    public void Update()
    {
       
        StartCoroutine(HealthRegen());
    }
    public void TakeDamage(float damageAmt)
    {
        if (is_Dead)
            return;

        health -= damageAmt;
        if (health <= 0f)
        {
            if (is_Zombie)
         {
      
     //           Die();
                
            }
            else
            {
                PlayerDie();
            }
        }
        if (is_Player)
        {
            player_Stats.Display_HealthStats(health);

            if (is_Zombie)
            {
                if (enemy_Controller.Enemy_State == EnemyState.PATROL)
                {
                    enemy_Controller.chase_Distance = 200f;

                }
            }

        }
    } //takeDamage
   
    void Awake()
    {
        /*
        if (is_Zombie)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            // get enemy audo
            //enemyAudio = GetComponentInChildren<EnemyAudio>();
            rBody = GetComponent<Rigidbody>();
        }
        */
        if (is_Player)
        {
            player_Stats = GetComponent<PlayerStats>();
        }
    }

 

   
    void PlayerDie()
    {
        RestartGame();
    }
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ZombieSurvival");
    }
  


    IEnumerator HealthRegen()
    {
       
        
        if (is_Player)
        {
            if (health < 100)
            {
                yield return new WaitForSeconds(2f);
                health += (healthRegen / 2f) * Time.deltaTime;
                player_Stats.Display_HealthStats(health);
            }
        }
    }


}


