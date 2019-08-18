using Simulation;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[CreateAssetMenu(fileName = "ViewFactory", menuName = "Create View Factory", order = 51)]
public class ViewFactory : ScriptableObject {

    [SerializeField] private GameObject _simulationAreaPrefab;

    [SerializeField] private CircleView _redCirclePrefab;
    [SerializeField] private CircleView _blueCirclePrefab;


    public SimulationView CreateSimulationView(Vector2 size, Circle[] circles) {
        var area = CreateSimulationArea(size);
        InstantiateCircleViews(circles, area.transform);
        var view = area.gameObject.AddComponent<SimulationView>();
        return view;
    }

    private Transform CreateSimulationArea(Vector2 size) {
        var area = Instantiate(_simulationAreaPrefab).transform;
        var rend = area.GetComponent<SpriteRenderer>();
        rend.size = size;
        return area;
    }

    private Transform[] InstantiateCircleViews(Circle[] circles, Transform parent) {
        var circleViews = new Transform[circles.Length];
        for (int i = 0; i < circles.Length; i++) {
            var circleView = Instantiate(GetCirclePrefab(circles[i].Color), parent, false);
            circleView.Setup(circles[i]);
            circleViews[i] = circleView.transform;
        }
        return circleViews;
    }

    private CircleView GetCirclePrefab(CircleColor color) {
        switch (color) {
            case CircleColor.Red:
                return _redCirclePrefab;
            default:
                return _blueCirclePrefab;
        }
    }

}
