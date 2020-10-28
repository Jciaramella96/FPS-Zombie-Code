
using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}
public enum WeaponBulletType
{
    BULLET,
    GRENADE

}
public class GunScript : MonoBehaviour
{

    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;


    public float damage = 10f;
    public float range = 100f;
    public float impactForce=5f; // variable for impact on Rigidbody
    public float fireRate = 15f;
    public ParticleSystem muzzleFlash;
    public Camera fpsCam;
    public GameObject BloodimpactEffect;
    // public GameObject ConcreteimpactEffect;
   // private EnemyController enemyController;

    public Text ammoDisplay;
    public Text scoreDisplay;


    public static int playerScore = 0;
    private int points = 10;
    public static int startingAmmo = 360;
    public static  int maxAmmo = 360;
    public static int currentAmmo= 40;
    public static int currentAmmoClipSize = 20;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;
    private float nexttimeToFire = 0f;
    // Update is called once per frame

    void Update()
    {

        scoreDisplay.text = (playerScore.ToString());
        ammoDisplay.text = (currentAmmo.ToString() + "/" + maxAmmo.ToString());
        if (isReloading)
            return;
        if (maxAmmo <= 0 && currentAmmo<=0)
            return;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nexttimeToFire) // getButton vs Getbutton down, getbutton hold mouse butotn down to fire, getbuttondown click mouse button to fire
        {
            nexttimeToFire = Time.time + 1f / fireRate; // sets fire rate on gun so its not just how fast u click mouse
            Shoot();
        }    
    }  

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        print("reloading....");
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .1f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.1f);
        currentAmmo = 40;
        maxAmmo = maxAmmo - currentAmmo;
        isReloading = false;
    }
    void Start()
    {
        scoreDisplay.text = (playerScore.ToString());
        currentAmmo = 40;
      //  enemyController = GetComponent<EnemyController>();
    }
    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           // Debug.Log(hit.transform.name);

         // Target target =  hit.transform.GetComponent<Target>();
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
          //  if (target)
          if(enemy)
            {
                print("Zombiehit");
                playerScore += points;
                enemy.TakeDamage(damage);
                //hit.rigidbody.AddForce(-hit.normal * impactForce);
                GameObject impactGO = Instantiate(BloodimpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, .5f);
            }
            // code to apply force to target we shoot, target must have rigidbody however
           
         

      
        }

    }

    public  void MaxAmmo()
    {
        maxAmmo = startingAmmo;
    }
}
