using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float _defaultExplosionRange;
    [SerializeField] private float _defaultExplosionForce;
    [SerializeField] private LayerMask _cubeLayer;

    Vector3 position = Vector3.zero;
    float _explosionForce = 0;
    float _explosionRange = 0;

    public void Explode(Cube cube)
    {
        Debug.Log(cube.name);

        position = cube.transform.position;
        _explosionForce = _defaultExplosionForce / cube.transform.localScale.sqrMagnitude;
        _explosionRange = _defaultExplosionRange / cube.transform.localScale.sqrMagnitude;

        foreach (Rigidbody body in GetCollidedBodies())
        {
            body.AddExplosionForce(_explosionForce, position, _explosionRange);
        }
    }

    private List<Rigidbody> GetCollidedBodies()
    {
        Collider[] hits = Physics.OverlapSphere(position, _explosionRange, _cubeLayer);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(position, _explosionRange);
    }
}
