using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIFlocking
{

    private NavMeshAgent _navMeshAgent;
    
    public List<GameObject> Neighbors = new();

    public Vector3 OwnerPosition => _navMeshAgent.transform.position;
    public Vector3 Velocity { get => _navMeshAgent.velocity; set => _navMeshAgent.velocity = value; }

    public AIFlocking(NavMeshAgent agent)
    {
        _navMeshAgent = agent;
        
        if (_navMeshAgent == null)
            Debug.LogError("AI Flocking: no NavMeshAgent found!");
    }

    public void SetAllNeighborsWithTag(string neighborTag)
    {
        GameObject[] neighbors = GameObject.FindGameObjectsWithTag(neighborTag);

        foreach (GameObject neighbor in neighbors)
        {
            if (Neighbors.Contains(neighbor)) continue;
            Neighbors.Add(neighbor);
        }

        Debug.Log($"Flocking neighbors: {Neighbors.Count}");
    }

    public void AddNeighbor(GameObject neighbor)
    {
        if (Neighbors.Contains(neighbor)) return;
        Neighbors.Add(neighbor);
    }

    public void RemoveNeighbor(GameObject neighbor)
    {
        if (!Neighbors.Contains(neighbor)) return;
        Neighbors.Remove(neighbor);
    }

    public void ResetNeighbors()
    {
        Neighbors.Clear();
    }

    public Vector3 CalculateAlignment(float distance, float weight, float maxVelocity, float speed)
    {
        Vector3 alignmentVector3 = Vector3.zero;
        int numberOfNeighbors = 0;
        foreach (GameObject neighbor in Neighbors)
        {
            Vector3 separation = OwnerPosition - neighbor.transform.position;
            float currentDistance = separation.magnitude;

            if (!(currentDistance > 0f) || !(currentDistance < distance)) continue;
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
        foreach (GameObject neighbor in Neighbors)
        {
            Vector3 separation = OwnerPosition - neighbor.transform.position;
            float currentDistance = separation.magnitude;
            
            if (!(currentDistance > 0f) || !(currentDistance < distance)) continue;
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
        int numberOfNeighbors = 0;

        foreach (GameObject neighbor in Neighbors)
        {
            Vector3 separation = OwnerPosition - neighbor.transform.position;
            float currentDistance = separation.magnitude;

            if (!(currentDistance > 0f) || !(currentDistance < distance)) continue;
            separation /= distance;
            separationVector3 += separation;
            separation.Normalize();
            numberOfNeighbors++;
        }

        if (numberOfNeighbors <= 0) return Vector3.zero;

        Vector3 averageSeparation = separationVector3 / numberOfNeighbors;
        averageSeparation = averageSeparation.normalized * speed;

        Vector3 steering = averageSeparation - Velocity;
        if (steering.sqrMagnitude > maxVelocity * maxVelocity)
        {
            steering = steering.normalized * maxVelocity;
        }

        return steering * weight;
    }
}
