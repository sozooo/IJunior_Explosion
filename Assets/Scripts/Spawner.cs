using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explosive))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Spawner _spawnerPrefab;
    [SerializeField] private float _decreaseCoefficient = 2;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 7;

    private Explosive _explosive;
    private float _chancesForExplode = 1f;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _explosive = GetComponent<Explosive>();
    }

    private void OnMouseUpAsButton()
    {
        if (Random.value <= _chancesForExplode)
        {
            List<Rigidbody> _bodies = new List<Rigidbody>();
            int count = Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < count; i++)
            {
                Spawner explosive = Instantiate(_spawnerPrefab, transform.position, Quaternion.identity);

                explosive.Initialize(_chancesForExplode / _decreaseCoefficient, transform.localScale / _decreaseCoefficient);

                if(explosive.TryGetComponent(out Rigidbody rigidbody))
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
