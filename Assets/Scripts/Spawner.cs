using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    public void Spawn(Cell cell)
    {
        Cube cube = Instantiate(_prefab, cell.Position, Quaternion.identity);

        cube.Initialize(cell);
    }
}
