using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using AngleExtension;

/// <summary>
/// liberated from https://www.youtube.com/watch?v=2zVwug_agr0
/// </summary>
public class TrajectoryPrediction : MonoBehaviour
{
    void Example()
    {
        Transform target = transform, self = transform;
        Vector2 targetVelocity = Vector2.zero;
        float projectileSpeed = 1;
        Rigidbody2D instance = new Rigidbody2D();
        if(InterceptionDirection(target.position.ToVector2(), self.position.ToVector2(), targetVelocity, projectileSpeed, out var direction))
            instance.velocity = direction * projectileSpeed;
        else { }
    }
    public bool InterceptionDirection(Vector2 a, Vector2 b, Vector2 vA, float sB, out Vector2 result)
    {
        var aToB = b-a;
        var dC = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, vA) * Mathf.Deg2Rad;
        var sA = vA.magnitude;
        var r = sA/sB;
        if (MyMath.SolveQuadratic(1-r*r, 2*r*dC*Mathf.Cos(alpha), -(dC*dC), out var root1, out var root2) == 0)
        {
            result = Vector2.zero;
            return false;
        }
        var dA = Mathf.Max(root1, root2);
        var t = dA/ sB;
        var c = a+ vA * t;
        result = (c - b).normalized;
        return true;
    }
}

public class MyMath
{
    public static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    {
        var discriminant = b * b - 4 * a * c;
        if(discriminant < 0)
        {
            root1 = math.INFINITY;
            root2 = -root1;
            return 0;
        }
        root1 = (-b + Mathf.Sqrt(discriminant)) / (2*a);
        root2 = (-b - Mathf.Sqrt(discriminant)) / (2*a);
        return discriminant > 0 ? 2 : 1;
    }
}
