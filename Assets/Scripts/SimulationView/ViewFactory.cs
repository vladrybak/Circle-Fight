using Simulation;
using UnityEngine;

[CreateAssetMenu(fileName = "ViewFactory", menuName = "Create View Factory", order = 51)]
public class ViewFactory : ScriptableObject {

    [SerializeField] private GameObject _simulationAreaPrefab;

    [SerializeField] private CircleView _redCirclePrefab;
    [SerializeField] private CircleView _blueCirclePrefab;


    public SimulationView CreateSimulationView() {
        var area = Instantiate(_simulationAreaPrefab).transform;
        return area.gameObject.AddComponent<SimulationView>();
    }

   public CircleView CreateCircleView(Circle circle, Transform parent) {
        return Instantiate(GetCirclePrefab(circle.Color), parent, false);
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
