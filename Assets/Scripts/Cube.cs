using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Spawner), typeof(Explosive))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _decreaseCoefficient = 2;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;

    private Spawner _spawner;
    private Explosive _explosive;
    private Renderer _renderer;

    private float _chancesForExplode = 1f;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _explosive = GetComponent<Explosive>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseUpAsButton()
    {
        if (Random.value <= _chancesForExplode)
        {
            List<Rigidbody> _bodies = new();
            int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            for (int i = 0; i < count; i++)
            {
                Rigidbody rigidbody = _spawner.Spawn(_chancesForExplode / _decreaseCoefficient, transform.localScale / _decreaseCoefficient);

                if (rigidbody == null)
                    throw new InvalidOperationException(nameof(OnMouseUpAsButton));

                _bodies.Add(rigidbody);
            }

            _explosive.Explode(_bodies);
        }

        Destroy(gameObject);
    }

    public void Initialize(float chance, Vector3 size)
    {
        _chancesForExplode = chance;
        transform.localScale = size;

        _renderer.material.color = Random.ColorHSV();
    }
}
