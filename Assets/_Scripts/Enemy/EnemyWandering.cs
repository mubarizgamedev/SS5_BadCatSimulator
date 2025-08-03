using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyWandering : MonoBehaviour
{
    [SerializeField] private Transform currentWaypointTransform;
    [SerializeField, Tooltip("Current destination waypoint the granny is moving toward (for debugging only)")]
    private Vector3 currentDestination;



    bool canWander;
    public bool waiting = false;
    NavMeshAgent agent;
    List<Vector3> wayPoints;
    [SerializeField] Transform[] firstFloorWayPoints;
    [SerializeField] Transform[] secondFloorWayPoints;
    int currentWayPointIndex;
    [SerializeField] float distBeforeWaypoint = 0.5f;
    public Animator grannyAnimator;
    public RuntimeAnimatorController grannyWanderingAnimator;
    public RuntimeAnimatorController waitAnimController;
    public float waitAtWayPoint;
    #region UNITY FUNCTIONS

    private void Start()
    {
        wayPoints = new List<Vector3>();
        AddFirstFloorWayPoints();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Invoke(nameof(WanderTrue), 0.5f);
        waiting = true;
    }

    void WanderTrue()
    {
        canWander = true;
        waiting = false;
        grannyAnimator.runtimeAnimatorController = grannyWanderingAnimator;
    }

    public void Update()
    {

        if (canWander && !waiting)
        {
            if (!agent.pathPending && agent.remainingDistance <= distBeforeWaypoint)
            {
                StartCoroutine(MoveToNextWaypointWithDelay());
            }
        }
    }


    IEnumerator MoveToNextWaypointWithDelay()
    {
        ChangeAnimator();
        waiting = true; // Block further movement until done

        yield return new WaitForSeconds(waitAtWayPoint);
        grannyAnimator.runtimeAnimatorController = grannyWanderingAnimator;
        MoveToNextWaypoint();
        waiting = false;
    }

    #endregion


    #region HELPER FUNCTIONS

    void MoveToNextWaypoint()
    {
        
        if (wayPoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to the granny");
            return;
        }

        currentWayPointIndex = Random.Range(0, wayPoints.Count);

        // Find the original Transform (for inspector)
        currentDestination = wayPoints[currentWayPointIndex];
        currentWaypointTransform = FindOriginalTransform(currentDestination); // <-- helper method
        agent.SetDestination(currentDestination);
    }

    #endregion

    public void AddFirstFloorWayPoints()
    {
        foreach (var item in firstFloorWayPoints)
        {
            wayPoints.Add(item.position);
        }
    }


    public void UpdateWayPoints(List<Vector3> newWaypoints)
    {
        wayPoints.Clear();

        foreach (var item in newWaypoints)
        {
            wayPoints.Add(item);
        }
        Debug.Log("Granny waypoints updated");
    }


    Transform FindOriginalTransform(Vector3 pos)
    {
        foreach (var t in firstFloorWayPoints)
            if (t.position == pos)
                return t;

        foreach (var t in secondFloorWayPoints)
            if (t.position == pos)
                return t;

        return null;
    }


    int lastIndex = -1; // Declare this at the class level

    void ChangeAnimator()
    {
        grannyAnimator.runtimeAnimatorController = waitAnimController;

        int randomIndex;

        // Repeat until a different index is selected
        do
        {
            randomIndex = Random.Range(0, 4);
        } while (randomIndex == lastIndex);

        lastIndex = randomIndex;

        grannyAnimator.SetInteger("RandomAnimIndex", randomIndex);
    }
}
