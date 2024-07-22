using System;
using UnityEngine;

public class Cell
{
    public Cell(Vector3 position, Vector3 scale, float chancesForExplode,
        Spawner spawner, Explosive explosive)
    {
        if (chancesForExplode < 0)
            throw new ArgumentOutOfRangeException(nameof(chancesForExplode));

        Position = position;
        Scale = scale;
        ChancesForExplode = chancesForExplode;

        CubeSpawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
        ExplosiveObject = explosive ?? throw new ArgumentNullException(nameof(explosive));
    }

    public Vector3 Position { get; private set; }
    public Vector3 Scale { get; private set; }
    public float ChancesForExplode { get; private set; }

    public Spawner CubeSpawner { get; private set; }
    public Explosive ExplosiveObject { get; private set; }
}
