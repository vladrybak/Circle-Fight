using UnityEngine;
using Simulation;

public class CircleView : MonoBehaviour {

    [SerializeField] private SpriteRenderer _rend;
    [SerializeField] private CircleColor _color;

    private Circle _circle;
    private bool _isDestroyed = false;
    private bool _isSpawned = false;
    private bool _isActive = false;

    private SimulationView _parent;
    

    private void Awake() {
        _rend.enabled = false;
    }

    public void Setup(Circle circle, SimulationView parent) {
        _parent = parent;
        _circle = circle;
        circle.Destroy += CircleDestroyHandler;
        circle.Spawn += CircleSpawnHandler;
    }

    private void CircleSpawnHandler() {
        _isSpawned = true;
    }

    private void Update() {
        if (_isDestroyed) {
            Destroy(gameObject);
            return;
        }
        if (_isActive) {
            UpdatePosAndScale();
            return;
        }
        if (_isSpawned) {
            _rend.enabled = true;
            _isActive = true;
            UpdatePosAndScale();
        }
    }

    private void UpdatePosAndScale() {
        transform.localPosition = _parent.ConvertToLocal(_circle.Position);
        transform.localScale = Vector3.one * (float)_circle.Radius * 2;
    }

    private void CircleDestroyHandler() {
        _isDestroyed = true;
    }

}
