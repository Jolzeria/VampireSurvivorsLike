using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public float rotateSpeed;
    public Transform holder, fireballToSpawn;
    public float timeBetweenSpawn;
    private float spawnTimer;
    
    void Start()
    {

    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = timeBetweenSpawn;

            var obj = Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject;
            obj.SetActive(true);
        }
    }
}
