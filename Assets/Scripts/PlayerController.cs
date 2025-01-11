using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MyCharacterController
{
    [SerializeField] private ScreenTouchController input;
    [SerializeField] private ShootController shootController;

    private bool _isShooting;
    private List<Transform> enemies = new();
    
    void FixedUpdate()
    {
        var direction = new Vector3(input.Direction.x, 0, input.Direction.y);
        Move(direction);
    }

    private void Update()
    {
        if (enemies.Count > 0)
        {
            transform.LookAt(enemies[0]);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Dead();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag($"Enemy"))
        {
            if (!enemies.Contains(other.transform))
            {
                enemies.Add(other.transform);
            }

            AutoShoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag($"Enemy"))
        {
            enemies.Remove(other.transform);
        }
    }

    private void AutoShoot()
    {
        IEnumerator Do()
        {
            while (enemies.Count > 0)
            {
                var enemy = enemies[0];
                var direction = enemy.transform.position - transform.position;
                direction.y = 0;
                direction = direction.normalized;
                shootController.Shoot(direction, transform.position);
                enemies.RemoveAt(0);
                yield return new WaitForSeconds(shootController.Delay);
            }
            _isShooting = false;
        }
        
        if (!_isShooting)
        {
            _isShooting = true;
            StartCoroutine(Do());
        }
    }
    private void Dead()
    {
        Debug.Log("Dead");
        Time.timeScale = 0;
    }
}