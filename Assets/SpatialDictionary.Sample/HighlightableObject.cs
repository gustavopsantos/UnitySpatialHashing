using UnityEngine;

namespace SpatialDictionary.Sample
{
    public class HighlightableObject : MonoBehaviour, IHasBounds
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _normalMaterial;
        [SerializeField] private Material _highlightMaterial;

        public Bounds Bounds => _renderer.bounds;

        public void Highlight(bool activate)
        {
            var material = activate ? _highlightMaterial : _normalMaterial;
            _renderer.sharedMaterial = material;
        }
    }
}