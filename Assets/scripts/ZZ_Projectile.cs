using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZ_Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);

        Debug.Log("Projectile Created");
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += transform.TransformDirection(Vector3.forward);

    }

    //-----------------------------------------------

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void SetLifeTime(float newLifeTime)
    {
        lifeTime = newLifeTime;
    }
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
}
