using Simulation;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[CreateAssetMenu(fileName = "ViewFactory", menuName = "Create View Factory", order = 51)]
public class ViewFactory : ScriptableObject {

    [SerializeField] private GameObject _simulationAreaPrefab;

    [SerializeField] private CircleView _redCirclePrefab;
    [SerializeField] private CircleView _blueCirclePrefab;


    public SimulationView CreateSimulationView(Vector2 size, Circle[] circlesData) {
        var area = CreateSimulationArea(size);
        InstantiateCircleViews(circlesData, area.transform);
        var view = area.gameObject.AddComponent<SimulationView>();
        return view;
    }

    private Transform CreateSimulationArea(Vector2 size) {
        var area = Instantiate(_simulationAreaPrefab).transform;
        var rend = area.GetComponent<SpriteRenderer>();
        rend.size = size;
        return area;
    }

    private Transform[] InstantiateCircleViews(Circle[] circlesData, Transform parent) {
        var circleViews = new Transform[circlesData.Length];
        for (int i = 0; i < circlesData.Length; i++) {
            var circleView = Instantiate(GetCirclePrefab(circlesData[i].Color), parent, false);
            circleView.Setup(circlesData[i]);
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
