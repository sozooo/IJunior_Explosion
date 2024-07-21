using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private Explosive _explosivePrefab;
    [SerializeField] private float _explosionRange;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask _cubeLayer;

    private float _chancesForExplode = 1f;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnMouseUpAsButton()
    {
        if (Random.value <= _chancesForExplode)
        {
            int count = Random.Range(2, 6);

            for (int i = 0; i < count; i++)
            {
                Explosive explosive = Instantiate(_explosivePrefab, transform.position, Quaternion.identity);

                explosive.Initialize(_chancesForExplode / 2, transform.localScale / 2);
            }

            Explode();
        }

        Destroy(gameObject);
    }

    public void Initialize(float chance, Vector3 size)
    {
        _chancesForExplode = chance;
        transform.localScale = size;

        _renderer.material.color = Random.ColorHSV();
    }

    private void Explode()
    {
        foreach (Rigidbody cube in GetCollidedBodies())
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRange);
    }

    private List<Rigidbody> GetCollidedBodies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRange, _cubeLayer);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach(Collider hit in hits)
        {
            if(hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }
}
