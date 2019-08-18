using UnityEngine;
using Simulation;

public class CircleView : MonoBehaviour {

    private Circle _circle;
    private bool _isDestroyed = false;


    public void Setup(Circle circle) {
        _circle = circle;
        circle.Destroy += CircleDestroyHandler;
    }

    private void Update() {
        if (_isDestroyed) {
            Destroy(gameObject);
            return;
        }
        transform.localPosition = SimulationView.ConvertToLocal(_circle.Position);
        transform.localScale = Vector3.one * (float)_circle.Radius;
    }

    private void CircleDestroyHandler() {
        _isDestroyed = true;
    }

}
