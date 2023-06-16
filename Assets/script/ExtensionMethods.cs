using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    public static bool Raycast(this Rigidbody2D rigidbody2D , Vector2 direction)
    {
        if (rigidbody2D.isKinematic) return false;
        float radius = 0.24f;
        float distance = 0.09f;
        RaycastHit2D hit = Physics2D.CircleCast(rigidbody2D.position, radius, direction, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody2D;
    }
    public static bool DotTest(this Transform transform ,Transform other , Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.24f;
    }
}
