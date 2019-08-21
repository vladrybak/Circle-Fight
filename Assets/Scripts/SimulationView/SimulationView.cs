using Simulation;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class SimulationView : MonoBehaviour {

    private readonly float _offsetSize = 3;

    private static Vector2 _areaOffset;
    private static Vector2 _areaSize;

    private ViewFactory _factory;
    private List<CircleView> _circleViews = new List<CircleView>();


    public void Setup(ViewFactory factory) {
        _factory = factory;
    }

    public void UpdateSimulationArea(Settings settings) {
        _areaSize = new Vector2(settings.GameAreaWidth, settings.GameAreaHeight);
        _areaOffset = _areaSize / 2f;

        var rend = GetComponent<SpriteRenderer>();
        rend.size = _areaSize;

        SetupCamera();
    }

    private void SetupCamera() {
        if (_areaSize.x > _areaSize.y) {
            Camera.main.orthographicSize = _areaSize.x / (Camera.main.aspect * 2);
        } else {
            Camera.main.orthographicSize = _areaSize.y / 2f;
        }
        Camera.main.orthographicSize += _offsetSize;
    }

    public void ShowCircles(Circle[] circles) {
        InstantiateCircleViews(circles);
    }

    private void InstantiateCircleViews(Circle[] circles) {
        for (int i = 0; i < circles.Length; i++) {
            var circleView = _factory.CreateCircleView(circles[i], transform);
            circleView.Setup(circles[i], this);
            _circleViews.Add(circleView);
        }
    }

    public Vector2 ConvertToLocal(Simulation.Vector2 pos) {
        return new Vector2((float)pos.X - _areaOffset.x, (float)pos.Y - _areaOffset.y);
    }

    public void Clear() {
        if (_circleViews.Count == 0)
            return;

        foreach (var c in _circleViews)
            if (c) Destroy(c.gameObject);
        _circleViews.Clear();
    }

}
