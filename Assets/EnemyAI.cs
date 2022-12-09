using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    private Transform player;
    public GameObject gun;
    public AudioSource laserAudio;

    //Stats
    public int health;

    //Check for Ground/Obstacles
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attack Player
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public bool isDead;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Special
    public GameObject projectile;

    private void Awake()
    {
        player = GameObject.Find("XR Origin").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isDead && health > 0)
        {
            //Check if Player in sightrange
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            //Check if Player in attackrange
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            else if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            else if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (isDead) return;
        animator.SetBool("Run_guard_AR", true);
        animator.SetBool("Shoot_SingleShot_AR", false);

        if (!walkPointSet) SearchWalkPoint();

        //Calculate direction and walk to Point
        if (walkPointSet){
            agent.SetDestination(walkPoint);

            //Vector3 direction = walkPoint - transform.position;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }

        //Calculates DistanceToWalkPoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint,-transform.up, 2,whatIsGround))
        walkPointSet = true;
    }
    private void ChasePlayer()
    {
        if (isDead) return;
        animator.SetBool("Run_guard_AR", true);
        animator.SetBool("Shoot_SingleShot_AR", false);
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        if (isDead) return;
        animator.SetBool("Shoot_SingleShot_AR", true);
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot_SingleShot_AR")){

            //Attack
            GameObject laser = Instantiate(projectile, gun.transform.position, transform.rotation);

            laser.GetComponent<Rigidbody>().AddForce(transform.forward * 32f, ForceMode.Impulse);
            laserAudio.Play();
            Destroy(laser,5f);
            //rb.AddForce(transform.up * 8, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }

    }
    private void ResetAttack()
    {
        if (isDead) return;

        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 0){
            isDead = true;
            animator.SetTrigger("Die");
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 4f);
        }
    }
}