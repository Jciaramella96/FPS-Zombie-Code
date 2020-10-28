using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class EnemyManagerZombie : MonoBehaviour
{
    public static EnemyManagerZombie instance;

    [SerializeField]
    private GameObject zombie_Prefab;
 
    public Transform[] zombie_SpawnPoints;
    public  int roundCount = 1;
    [SerializeField]
    private Text roundTracker;
    private EnemyController enemy_Controller;
    public int zombie_Count;
    
    private int initial_ZombieCount;
    public float wait_BeforeSpawnZombies = 5f;
    public static int totalZombies;
    private Target target;

    void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_Controller = GetComponent<EnemyController>();
        initial_ZombieCount = zombie_Count;
        totalZombies = zombie_Count;
        SpawnEnemies();
     //   StartCoroutine("CheckToSpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        roundTracker.text = roundCount.ToString();
        if (totalZombies == 0)
        {
         
           NewRound();
        }
       
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SpawnEnemies()
    {
        SpawnZombies();
    }

   public void SpawnZombies()
    {
        int index = 0;
        for(int i=0; i<zombie_Count; i++)
        {
           
            if (index>= zombie_SpawnPoints.Length)
            {
                index = 0;
                
            }
            Instantiate(zombie_Prefab, zombie_SpawnPoints[index].position, Quaternion.identity);

            index++;
        }
        zombie_Count = 0;
    }// spawn zombies

        /*
    IEnumerator CheckToSpawnEnemies()
    {
        
            yield return new WaitForSeconds(wait_BeforeSpawnZombies);

            SpawnZombies();

            StartCoroutine("CheckToSpawnEnemies");
        
    }
        */
    public void EnemyDied()
    {
        
        if(zombie_Count > initial_ZombieCount)
        {
            zombie_Count = initial_ZombieCount;
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

    void NewRound()
    {
       

        int index = 0;
        roundCount++;
        zombie_Count = zombie_Count * roundCount +1;
        totalZombies = zombie_Count;
        for (int i = 0; i < zombie_Count; i++)
                {
            if (index >= zombie_SpawnPoints.Length)
            {
                index = 0;

            }
            Instantiate(zombie_Prefab, zombie_SpawnPoints[index].position, Quaternion.identity);
            index++;
        }
        
    }

  
   
}
