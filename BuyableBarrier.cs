using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BuyableBarrier : MonoBehaviour
{
    public Text buyText;
    public int buyPrice;
    
    private GunScript gunScript;
    // Start is called before the first frame update
    void Update()
    {
       
    }
    void Start()
    {
     gunScript = gameObject.GetComponent<GunScript>();
    }

         void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("playercolliding");
         
            if(GunScript.playerScore> buyPrice)
            {
                buyText.gameObject.SetActive(true);
                buyText.text = ("Press X and buy this door for " + buyPrice);
            }
         
        }
    }

    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.X) && GunScript.playerScore > buyPrice) 
        {
            print("buyingdoor");
            Destroy(this.gameObject);
            buyText.gameObject.SetActive(false);
            GunScript.playerScore -= buyPrice;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        buyText.gameObject.SetActive(false);
    }


    void Buy()
    {

    }
}
