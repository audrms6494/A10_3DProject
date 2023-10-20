using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonWander : MonoBehaviour
{
    private NavMeshAgent agent;
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;
    private float blockedTime;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartCoroutine(WanderWithWaitTime());

    }
    private void WanderToNewLocation()
    {
        agent.SetDestination(GetWanderLocation());
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        return hit.position;
    }

    private IEnumerator WanderWithWaitTime()
    {
        while (true)
        {
            if (agent.remainingDistance < 0.5f)
            {
                yield return new WaitForSeconds(Random.Range(minWanderWaitTime, maxWanderWaitTime));
                WanderToNewLocation();
            }
            if (agent.destination != null && agent.velocity.sqrMagnitude < 0.3f)
            {
                blockedTime += Time.deltaTime;
                if (blockedTime > 3f)
                {
                    blockedTime = 0f;
                    WanderToNewLocation();
                }
            }
            yield return null;
        }
    }
}
