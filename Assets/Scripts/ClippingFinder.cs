using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClippingFinder
{
    public bool PositionIsFree(Vector3 position, MonoBehaviour reference)
    {
        BoxCollider[] solidColliders = GetAllBoxColliders(reference)
                                      .Where(collider => collider.isTrigger == false)
                                      .ToArray();
        bool isFree = true;

        foreach (var collider in solidColliders)
        {
            Vector3 colliderSize = collider.size.Multiply(collider.transform.localScale);
            Vector3 halfSize = colliderSize / 2;
            Collider[] colliders = Physics.OverlapBox(position + collider.center, halfSize, collider.transform.rotation);

            if (colliders.Where(collider => collider.isTrigger == false).Count() != 0)
            {
                isFree = false;
            }
        }

        return isFree;
    }

    private List<BoxCollider> GetAllBoxColliders(MonoBehaviour reference)
    {
        List<BoxCollider> colliders = reference.GetComponents<BoxCollider>().ToList();
        colliders.AddRange(reference.GetComponentsInChildren<BoxCollider>());
        return colliders;
    }
}
