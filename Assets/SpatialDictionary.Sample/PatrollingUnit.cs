using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpatialDictionary.Sample
{
    public class PatrollingUnit : HighlightableObject
    {
        private const float MovementSpeed = 8;
        private const float PatrollingRadius = 16f;
        
        private Vector3 _origin;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _origin = _transform.position;
            StartCoroutine(UnitCoroutine());
        }

        private IEnumerator UnitCoroutine()
        {
            while (true)
            {
                var waitTime = Random.Range(2f, 6f);
                yield return new WaitForSeconds(waitTime);
                var patrolPoint = GetPatrollingPoint();
                yield return MoveTo(patrolPoint);
            }
        }

        private IEnumerator MoveTo(Vector3 point)
        {
            var maxMovementDelta = MovementSpeed * Time.deltaTime;

            while (Vector3.Distance(_transform.position, point) > maxMovementDelta)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, point, maxMovementDelta);
                yield return null;
            }

            _transform.position = point;
        }

        private Vector3 GetPatrollingPoint()
        {
            var direction = Random.insideUnitCircle * PatrollingRadius;
            var patrollingPoint = _origin;
            patrollingPoint.x += direction.x;
            patrollingPoint.z += direction.y;
            return patrollingPoint;
        }
    }
}