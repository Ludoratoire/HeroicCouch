using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum TargetType : int
{
    Flee = 0,
    Enemy = 1,
    Treasure = 2,
    Destination = 3
}

public class CharacterCustomController : MonoBehaviour {

    public List<List<GameObject>> targets;
    public float attackRange = 3f;
    public float losingRange = 8f;
    public bool attackEnabled = false;

    private SphereCollider trackingZone;
    private NavMeshAgent agent;
    private Rigidbody rigidBody;
    private Animator animator;

    private bool trackingEnemy = false;
    private bool acquiredEnemy = false;

    public void AddTarget(GameObject target, TargetType priority = TargetType.Destination, bool preempt = false)
    {
        if (priority == TargetType.Destination)
            Debug.Log("Destination Added for ${gameObject.name}");

        var intPriority = (int)priority;

        if(targets[intPriority] == null)
            targets[intPriority] = new List<GameObject>();

        if(preempt)
            targets[intPriority].Insert(0, target);
        else
            targets[intPriority].Add(target);

        agent.isStopped = false;
        agent.SetDestination(CurrentTarget.transform.position);
    }

    public void RemoveTarget(GameObject target)
    {
        targets.ForEach(p => {
            if (p.Contains(target))
                p.Remove(target);
        });
    }

    public bool NoMoreTarget
    {
        get
        {
            return targets.Sum(t => t.Count) <= 0;
        }
    }

    public GameObject CurrentTarget
    {
        get
        {
            foreach(var list in targets)
            {
                foreach(var target in list)
                {
                    return target;
                }
            }

            return null;
        }
    }

    void Awake () {
        targets = new List<List<GameObject>>();
        foreach (var priority in Enum.GetValues(typeof(TargetType)))
            targets.Add(new List<GameObject>());

        trackingZone = GetComponent<SphereCollider>();
        agent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject)
            return;

        if (targets[(int)TargetType.Enemy].Contains(CurrentTarget))
            return;

        if (!attackEnabled)
            return;

        if(other is BoxCollider && other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            trackingEnemy = true;
            AddTarget(other.gameObject, TargetType.Enemy);
            agent.SetDestination(other.gameObject.transform.position);
        }
    }

    void Update()
    {
        if (CurrentTarget == null)
            return;

        if (!agent.isStopped)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        if (trackingEnemy)
        {
            var targetDistance = Vector3.Distance(gameObject.transform.position, CurrentTarget.transform.position);
            if (targetDistance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("attacking", true);
                acquiredEnemy = true;
                trackingEnemy = false;
            }
            else if (targetDistance > losingRange)
            {
                agent.isStopped = false;
                trackingEnemy = false;
                acquiredEnemy = false;
                RemoveTarget(CurrentTarget);
                if (NoMoreTarget)
                {
                    Debug.Log("Cant resume path");
                    return;
                }

                Debug.Log("Resume path");
                agent.SetDestination(CurrentTarget.transform.position);
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("attacking", false);
                agent.SetDestination(CurrentTarget.transform.position);
            }
        }
        else if (acquiredEnemy)
        {
            if (Vector3.Distance(gameObject.transform.position, CurrentTarget.transform.position) > attackRange)
            {
                agent.isStopped = false;
                agent.SetDestination(CurrentTarget.transform.position);
                acquiredEnemy = false;
                trackingEnemy = true;
            }
        }
        else if (Vector3.Distance(gameObject.transform.position, CurrentTarget.transform.position) <= 2f
            && targets[(int)TargetType.Destination].Contains(CurrentTarget))
        {
            RemoveTarget(CurrentTarget);
        }
    }
}

