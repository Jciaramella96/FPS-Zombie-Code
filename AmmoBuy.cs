using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBuy : BuyableBarrier
{

    private GunScript gunScript;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("playercolliding");

            if (GunScript.playerScore > buyPrice)
            {
                buyText.gameObject.SetActive(true);
                buyText.text = ("Press X and refill ammo " + buyPrice);
            }

         
        }
    }

    void OnTriggerStay()
    {

        if (Input.GetKeyDown(KeyCode.X) && GunScript.playerScore > buyPrice)
        {
            AmmoRefill();
        }
    }

   void AmmoRefill()
    {
      
        print("buyingammo");
        GunScript.maxAmmo = GunScript.startingAmmo;
        //  GunScript.currentAmmo = GunScript.currentAmmoClipSize;
        // buyText.gameObject.SetActive(false);
        GunScript.playerScore -= buyPrice;
    }
}
