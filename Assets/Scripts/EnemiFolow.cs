using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiFolow : MonoBehaviour
{
    public Transform player; 
    public float followDistance = 10f; 
    public float moveSpeed = 2f; 
    public float stopDistance = 1.5f;
    public LayerMask groundLayer;
    public float groundCheck = 1f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        
        if (distanceToPlayer <= followDistance)
        {
            FollowPlayer();
            animator.SetBool("isFollowing", true); 
        }
        else
        {
            animator.SetBool("isFollowing", false); 
        }
    }

    private void FollowPlayer()
    {
       
        Vector3 direction = (player.position - transform.position).normalized;

        
        transform.position += direction * moveSpeed * Time.deltaTime;

        
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheck, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}

