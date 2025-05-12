using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class ZZ_AimAndShoot : MonoBehaviour
{
    [SerializeField] private Vector3 defaultRot = Vector3.zero;
    [SerializeField] private bool PcInRange = false;
    [SerializeField] private float shootCooldown = 2;

    [SerializeField] private ZZ_Projectile projectile;
    private ZZ_Projectile instantiatedProjectile;

    [SerializeField] private Vector3 projectileSpawnPos = Vector3.zero;
    [SerializeField] private Quaternion projectileSpawnRot;
    [SerializeField] private float projectileSpeed = 0.1f;
    [SerializeField] private float projectileLifeTime = 1f;
    [SerializeField] private float projectileDamage = 1f;

    private float shootTimer;
    private GameObject PC;

    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindWithTag("Player");

        shootTimer = shootCooldown;

        //match projectile pos to turret pos
        projectileSpawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PcInRange)
        {
            //look at PC
            transform.LookAt(PC.transform);

            //decrement cooldown timer
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0)
            {
                //match projectile rot to turret rot
                projectileSpawnRot = transform.rotation;

                //instantiate projectile
                instantiatedProjectile =
                    Instantiate(projectile,
                    projectileSpawnPos,
                    projectileSpawnRot);

                //set projectile params
                instantiatedProjectile.SetSpeed(projectileSpeed);
                instantiatedProjectile.SetLifeTime(projectileLifeTime);
                instantiatedProjectile.SetDamage(projectileDamage);

                //reset cooldown timer
                shootTimer = shootCooldown;

                //reset instantiatedProjectile var
                instantiatedProjectile = null;
            }
        }
    }

    //----------------------------------------

    public void SetPCInRange(bool newValue)
    {
        PcInRange = newValue;
    }
}
