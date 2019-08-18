using UnityEngine;
using Simulation;

public class CircleView : MonoBehaviour {

    [SerializeField] private SpriteRenderer _rend;

    private Circle _circle;
    private bool _isDestroyed = false;
    private bool _isSpawned = false;
    private bool _isActive = false;
    

    private void Awake() {
        _rend.enabled = false;
    }

    public void Setup(Circle circle) {
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
            transform.localPosition = SimulationView.ConvertToLocal(_circle.Position);
            transform.localScale = Vector3.one * (float)_circle.Radius;
            return;
        }
        if (_isSpawned) {
            _rend.enabled = true;
            _isActive = true;
            transform.localPosition = SimulationView.ConvertToLocal(_circle.Position);
            transform.localScale = Vector3.one * (float)_circle.Radius;
            return;
        }
    }

    private void CircleDestroyHandler() {
        _isDestroyed = true;
    }

}
