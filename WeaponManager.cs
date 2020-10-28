using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GunScript[] weapons;


    private int current_Weapon_Index;
    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
    }
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) // if you push down 1 on keyboard
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // if you push down 2 on keyboard, etc..
        {
            TurnOnSelectedWeapon(1);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)

    {
        if (current_Weapon_Index == weaponIndex)
            return;


        weapons[current_Weapon_Index].gameObject.SetActive(false); //deactivates current weapon

        weapons[weaponIndex].gameObject.SetActive(true); // turns on selected weapon
        WeaponSwap();
        current_Weapon_Index = weaponIndex; // store current selected weapon index
      
    }

    public GunScript GetCurrentSelectedWeapon() // tells us what weapon we using 
    {
        return weapons[current_Weapon_Index];
    }

    void WeaponSwap()
    {
        anim.SetBool("SwappingWeapons",true);
    }

}
