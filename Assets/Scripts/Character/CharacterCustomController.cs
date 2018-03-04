using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterCustomController : MonoBehaviour {

    public List<GameObject> targets;
    public float attackRange = 3f;
    public float losingRange = 8f;
    public bool attackEnabled = false;

    private SphereCollider trackingZone;
    private NavMeshAgent agent;

    private bool trackingEnemy = false;
    private bool acquiredEnemy = false;

	void Start () {
        trackingZone = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.SetDestination(targets[0].transform.position);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject)
            return;

        if (!attackEnabled)
            return;

        if(other is BoxCollider && other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            trackingEnemy = true;
            targets.Insert(0, other.gameObject);
            agent.SetDestination(targets[0].transform.position);
            Debug.Log("Je t'ai vu connard !");
        }
    }

    void Update()
    {
        if (trackingEnemy)
        {
            var targetDistance = Vector3.Distance(gameObject.transform.position, targets[0].transform.position);
            if (targetDistance <= attackRange)
            {
                agent.isStopped = true;
                acquiredEnemy = true;
                trackingEnemy = false;
                Debug.Log("Je te tape !");
            }
            else if (targetDistance > losingRange)
            {
                agent.isStopped = false;
                targets.RemoveAt(0);
                agent.SetDestination(targets[0].transform.position);
                Debug.Log("T'es passé où ?");
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(targets[0].transform.position);
                Debug.Log("Je vais t'avoir");
            }
        }

        if(acquiredEnemy)
        {
            if (Vector3.Distance(gameObject.transform.position, targets[0].transform.position) > attackRange)
            {
                agent.SetDestination(targets[0].transform.position);
                agent.isStopped = false;
                acquiredEnemy = false;
                trackingEnemy = true;
            }
        }
    }
}

