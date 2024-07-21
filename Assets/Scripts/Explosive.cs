using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private float _explosionRange;
    [SerializeField] private float _explosionForce;

    public void Explode(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cube in cubes)
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRange);
    }
}
