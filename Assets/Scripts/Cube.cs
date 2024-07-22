using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _chancesDecreaseCoefficient = 2;
    [SerializeField] private float _scaleDecreaseCoefficient = 2;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosive _explosive;

    private Renderer _renderer;

    private float _chancesForExplode = 1f;

    public Rigidbody Body { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        Body = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log(Random.value);

        if (Random.value <= _chancesForExplode)
        {
            int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            for (int i = 0; i < count; i++)
            {
                _spawner.Spawn(MakeCell());
            }
        }
        else
        {
            Debug.Log("AddedForce");
            _explosive.Explode(this);
        }

        Destroy(gameObject);
    }

    public void Initialize(Cell cell)
    {
        _chancesForExplode = cell.ChancesForExplode;
        transform.localScale = cell.Scale;
        _spawner = cell.CubeSpawner;
        _explosive = cell.ExplosiveObject;

        _renderer.material.color = Random.ColorHSV();
    }

    private Cell MakeCell()
    {
        float newChances = _chancesForExplode / _chancesDecreaseCoefficient;
        Vector3 newScale = transform.localScale / _scaleDecreaseCoefficient;

        return new(transform.position, newScale, newChances, _spawner, _explosive);
    }
}
