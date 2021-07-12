using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public int health;
    private int clipSize = 12;
    public int ammoInClip;
    public float moveSpeed;

    private bool reloading = false;
    private bool shotbool = false;
    public bool dead;

    public CrossHair crossHair;
    public Shot shot;
    private GameController gameController;
    private HealthBar healthBar;
    private MovementAnimator movementAnimator;

    public AudioSource shotSound;
    public AudioSource reloadingPistolSound;
    

    public Transform gunBarrel;
    private Animator animator;

    public int score;
    public int dieZombieCounter;


    public TextMeshProUGUI AmmoCounter;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameController = FindObjectOfType<GameController>();
        crossHair = FindObjectOfType<CrossHair>();
        shot = FindObjectOfType<Shot>();
        animator = GetComponentInChildren<Animator>();
        healthBar = FindObjectOfType<HealthBar>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateRotation = false;

        healthBar.SetMaxHealth(health);
        ammoInClip = clipSize;
        score = 0;
        dieZombieCounter = 0;
    }

    void Update()
    {
        if (!dead)
        {
            Vector3 dir = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                dir.z = -1.0f;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                dir.z = 1.0f;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                dir.x = -1.0f;
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                dir.x = 1.0f;

            navMeshAgent.velocity = dir.normalized * moveSpeed;

            Vector3 forward = crossHair.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

            if (Time.timeScale == 1)
            {
                
                if (!reloading)
                {
                    if(!shotbool)
                        playerShot();
                }
            }
        }
    }


    public void playerShot()
    {
        if (ammoInClip == 0)
        {
            ReloadWeapon();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotbool = true;
                shotSound.Play();
                var from = gunBarrel.position;
                var target = crossHair.transform.position;
                var to = new Vector3(target.x, from.y, target.z);

                var direction = (to - from).normalized;
                RaycastHit hit;
                if (Physics.Raycast(from, to - from, out hit, 100))
                {
                    to = new Vector3(hit.point.x, from.y, hit.point.z);
                }

                if (hit.transform != null)
                {
                    var zombie = hit.transform.GetComponent<Zombie>();
                    if (zombie != null)
                    {
                        zombie.Kill();
                        score += 100;
                        dieZombieCounter += 1;
                    }
                        
                }
                else
                {
                    to = from + direction * 100;
                }

                shot.Show(from, to);

                ammoInClip -= 1;
                StartCoroutine(ShotDelay());
            }
        }

        AmmoCounter.text = ammoInClip + " / ∞";
    }

    void ReloadWeapon()
    {
        StartCoroutine(ReloadingTime());
        reloading = true;
    }

    public void PlayerDied()
    {
        if (!dead)
        {
            dead = true;
            GetComponentInChildren<ParticleSystem>().Play();
            animator.SetTrigger("died");
            navMeshAgent.avoidancePriority = 49;
            gameController.FinishGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Debug.Log("Finish");
            gameController.FinishGame();
        }
    }

    IEnumerator ReloadingTime()
    {
        reloadingPistolSound.Play();
        yield return new WaitForSeconds(2f);
        reloading = false;
        AmmoCounter.text = clipSize + " / ∞";
        ammoInClip = clipSize;
    }
    
    IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(.3f);
        shotbool = false;
    }
}