using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterCustomController : MonoBehaviour {

    public List<GameObject> targets;
    public float attackRange = 3f;
    public float losingRange = 8f;
    public bool attackEnabled = false;

    public AnimationClip idleAnimation;
    public AnimationClip moveAnimation;

    private SphereCollider trackingZone;
    private NavMeshAgent agent;
    private Rigidbody rigidBody;
    private Animator animator;

    private bool trackingEnemy = false;
    private bool acquiredEnemy = false;

	void Start () {
        trackingZone = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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
        if (!agent.isStopped)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        if (trackingEnemy)
        {
            if (targets.Count == 0)
                return;

            var targetDistance = Vector3.Distance(gameObject.transform.position, targets[0].transform.position);
            if (targetDistance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("attacking", true);
                acquiredEnemy = true;
                trackingEnemy = false;
                Debug.Log("Je te tape !");
            }
            else if (targetDistance > losingRange)
            {
                agent.isStopped = false;
                targets.RemoveAt(0);
                if (targets.Count == 0)
                    return;

                agent.SetDestination(targets[0].transform.position);
                Debug.Log("T'es passé où ?");
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("attacking", false);
                agent.SetDestination(targets[0].transform.position);
                Debug.Log("Je vais t'avoir");
            }
        }

        if(acquiredEnemy)
        {
            if (targets.Count == 0)
                return;

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

