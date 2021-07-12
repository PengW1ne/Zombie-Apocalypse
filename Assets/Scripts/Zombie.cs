using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Zombie : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;
    HealthBar healthBar;
    public int minDam;
    public int maxDam;
    
    CapsuleCollider capsuleCollider;
    Animator animator;
    public MovementAnimator movementAnimator;

    private float dist;
    bool dead;

    public AudioSource damageSound;
    public AudioSource zombieDieSound;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        healthBar = FindObjectOfType<HealthBar>();
        navMeshAgent.updateRotation = false;
        
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        movementAnimator = GetComponent<MovementAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (dist < 1.2)
        {
            animator.SetTrigger("atack");
        }
        if (dead)
            return;
        navMeshAgent.SetDestination(player.transform.position);
        if (navMeshAgent.velocity.normalized == Vector3.zero)
        {
            return;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }
        
    }
    
    public void Kill()
    {
        if (!dead) {
            dead = true;
            Destroy(capsuleCollider);
            Destroy(movementAnimator);
            Destroy(navMeshAgent);
            GetComponentInChildren<ParticleSystem>().Play();
            animator.SetTrigger("died");
            zombieDieSound.Play();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dist < 1.2)
            {
                healthBar.TakeDamage(Random.Range(minDam,maxDam));
                damageSound.Play();
            }
        }
    }
}
