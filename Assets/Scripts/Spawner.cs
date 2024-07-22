using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explosive))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    public Rigidbody Spawn(float _chancesForExplode, Vector3 scale)
    {
        Cube cube = Instantiate(_prefab, transform.position, Quaternion.identity);

        cube.Initialize(_chancesForExplode, scale);

        if (cube.TryGetComponent(out Rigidbody rigidbody))
            return rigidbody;

        return null;
    }
}
