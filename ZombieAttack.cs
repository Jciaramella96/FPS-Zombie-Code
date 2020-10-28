using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{

    public float damage = 2f;
    public float radius = 1f;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hits.Length > 0)
        {
            print("zombie touched" + hits[0].gameObject.tag);
            hits[0].gameObject.GetComponent<Target>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
