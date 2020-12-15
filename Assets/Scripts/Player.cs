using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public CrossHair crossHair;
    public Shot shot;

    public Transform gunBarrel;
    

    public float moveSpeed;
    public int health;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        crossHair = FindObjectOfType<CrossHair>();
        navMeshAgent.updateRotation = false;
        shot = FindObjectOfType<Shot>();
        health = 100;
    }

    void Update()
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
        
        if (Input.GetMouseButtonDown(0))
        {
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
                    zombie.Kill();
            }
            else
            {
                to = from + direction * 100;
            }
            shot.Show(from, to);
        }

        
        
    }
    
    

    public void PlayerDied()
    {
        Debug.Log("Die");
    }
}