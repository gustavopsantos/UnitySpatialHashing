using System.Collections.Generic;
using System.Linq;
using SpatialDictionary.Sample;
using UnityEngine;

public class InterestArea : MonoBehaviour
{
    private const int InterestAreaSize = 128; // Width (x) and Depth (z)
    
    [SerializeField] private World _world;
    [SerializeField] private Camera _camera;

    private Vector3 _mousePositionWorldSpace;

    private HashSet<HighlightableObject> _objectsInsideInterestArea = new(); 

    private void LateUpdate() // Late update ensure all unit position updates already ran
    {
        _mousePositionWorldSpace = GetMousePositionWorldSpace();

        var interestArea = new Bounds(_mousePositionWorldSpace, new Vector3(InterestAreaSize, 0, InterestAreaSize));
        var objectsInsideInterestAreaThisFrame = _world.WorldObjects.Where(worldObject => interestArea.Intersects(worldObject.Bounds)).ToHashSet();
        var objectsInsideInterestAreaLastFrame = _objectsInsideInterestArea;

        var objectsThatEnteredInterestArea = objectsInsideInterestAreaThisFrame.Except(objectsInsideInterestAreaLastFrame);
        var objectsThatLeavedInterestArea = objectsInsideInterestAreaLastFrame.Except(objectsInsideInterestAreaThisFrame);

        foreach (var obj in objectsThatEnteredInterestArea)
        {
            obj.Highlight(activate:true);
        }
        
        foreach (var obj in objectsThatLeavedInterestArea)
        {
            obj.Highlight(activate:false);
        }

        _objectsInsideInterestArea = objectsInsideInterestAreaThisFrame;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_mousePositionWorldSpace, new Vector3(InterestAreaSize, 0, InterestAreaSize));
    }

    private Vector3 GetMousePositionWorldSpace()
    {
        var mousePositionScreenSpace = Input.mousePosition;
        var mousePositionWorldSpace = _camera.ScreenToWorldPoint(mousePositionScreenSpace);
        mousePositionWorldSpace.y = 0;
        return mousePositionWorldSpace;
    }
}