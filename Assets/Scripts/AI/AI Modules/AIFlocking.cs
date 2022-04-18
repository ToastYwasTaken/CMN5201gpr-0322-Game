using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIFlocking
{

    private NavMeshAgent _navMeshAgent;
    
    public List<NavMeshAgent> Neighbors = new();
    
    public Vector3 OwnerPosition { get => _navMeshAgent.transform.position; }
    public Vector3 Velocity { get => _navMeshAgent.velocity; set => _navMeshAgent.velocity = value; }

    public AIFlocking(GameObject owner)
    {
        _navMeshAgent = owner.GetComponent<NavMeshAgent>();
        
        if (_navMeshAgent == null)
            Debug.LogError("AI Flocking: no NavMeshAgent found!");
    }

    public void AddNeighbor(NavMeshAgent navMeshAgent)
    {
        if (Neighbors.Contains(_navMeshAgent)) return;
        Neighbors.Add(navMeshAgent);
    }

    public void RemoveNeighbor(NavMeshAgent navMeshAgent)
    {
        if (!Neighbors.Contains(navMeshAgent)) return;
        Neighbors.Remove(navMeshAgent);
    }


    public Vector3 CalculateAlignment(float distance, float weight, float maxVelocity, float speed)
    {
        Vector3 alignmentVector3 = Vector3.zero;
        int numberOfNeighbors = 0;
        foreach (NavMeshAgent neighbor in from neighbor in Neighbors 
                 let separation = OwnerPosition - neighbor.transform.position 
                 let currentDistance = separation.magnitude 
                 where currentDistance > 0f && currentDistance < distance select neighbor)
        {
            alignmentVector3 += neighbor.transform.forward;
            numberOfNeighbors++;
        }

        if (numberOfNeighbors <= 0) return Vector3.zero;
        
        Vector3 averageAlignment = alignmentVector3 / Neighbors.Count;
        Vector3 steering = averageAlignment.normalized * speed;

        if (steering.sqrMagnitude > maxVelocity * maxVelocity)
        {
            averageAlignment = averageAlignment.normalized * maxVelocity;
        }
        return  alignmentVector3 * weight;
    }


    public Vector3 CalculateCohesion(float distance, float weight)
    {
        Vector3 cohesionVector3 = Vector3.zero;
        int numberOfNeighbors = 0;
        foreach (NavMeshAgent neighbor in from neighbor in Neighbors 
                 let separation = OwnerPosition - neighbor.transform.position 
                 let currentDistance = separation.magnitude 
                 where currentDistance > 0f && currentDistance < distance select neighbor)
        {
            cohesionVector3 += neighbor.transform.position;
            numberOfNeighbors++;
        }

        if (numberOfNeighbors <= 0) return Vector3.zero;
        cohesionVector3 /= Neighbors.Count;
        cohesionVector3 -= OwnerPosition;
        cohesionVector3.Normalize();
        return cohesionVector3 * weight;

    }

    public Vector3 CalculateSeparation(float distance, float weight, float maxVelocity, float speed)
    {
        Vector3 separationVector3 = Vector3.zero;
        Vector3 steering = Vector3.zero;
        int numberOfNeighbors = 0;
        foreach (Vector3 separation in from neighbor in Neighbors 
                 select OwnerPosition - neighbor.transform.position 
                 into separation 
                 let currentDistance = separation.magnitude 
                 where currentDistance > 0f && currentDistance < distance select separation / currentDistance)
        {
            separationVector3 += separation;
            separation.Normalize();
            numberOfNeighbors++;

            if (numberOfNeighbors <= 0) return Vector3.zero;
            
            Vector3 averangeSeparation = separationVector3 / numberOfNeighbors;
            averangeSeparation = separation.normalized * speed;

            steering = averangeSeparation - Velocity;
            if (steering.sqrMagnitude > maxVelocity * maxVelocity)
            {
                steering = steering.normalized * maxVelocity;
            }
        }
        return steering * weight;
    }
}
