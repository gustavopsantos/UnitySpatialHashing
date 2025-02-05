using System.Collections.Generic;
using UnityEngine;

namespace SpatialDictionary.Sample
{
    public class World : MonoBehaviour
    {
        [SerializeField] private PatrollingUnit _patrollingUnitPrefab;
        [SerializeField] private StationaryObject _stationaryObjectPrefab;

        public readonly List<HighlightableObject> WorldObjects = new();

        private void Start()
        {
            WorldObjects.AddRange(Spawn(_patrollingUnitPrefab, count: 1000));
            WorldObjects.AddRange(Spawn(_stationaryObjectPrefab, count: 1000));
        }

        private static IEnumerable<T> Spawn<T>(T prefab, int count) where T : Object
        {
            for (var i = 0; i < count; i++)
            {
                var position = GetWorldRandomPosition();
                var instance = Instantiate(prefab, position, Quaternion.identity);
                yield return instance;
            }
        }

        private static Vector3 GetWorldRandomPosition()
        {
            var x = Random.Range(-500, +500);
            var z = Random.Range(-500, +500);
            return new Vector3(x, 0, z);
        }
    }
}