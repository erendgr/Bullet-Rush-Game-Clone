using UnityEngine;

public class EnemyController : MyCharacterController
{
    [SerializeField] private PlayerController player;
    
    void FixedUpdate()
    {
        var delta = player.transform.position - transform.position;
        delta.y = 0;
        var direction = delta.normalized;
        Move(direction);
        
        transform.LookAt(player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag($"Bullet"))
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}