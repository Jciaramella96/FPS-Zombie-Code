using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealthPotion : Target
{
   
    public float heal1 = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER_TAG)
        {
            print("PlayerEntered");
           // Heal(heal1);
            Destroy(this.gameObject);
        }
    }

    
}
