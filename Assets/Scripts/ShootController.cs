﻿using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;
    
    public float Delay => delay;
    [SerializeField] private float delay;
    
    public void Shoot(Vector3 direction, Vector3 position)
    {
        var bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.Fire(direction);
    }
}