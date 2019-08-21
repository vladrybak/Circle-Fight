using UnityEngine;

[CreateAssetMenu(fileName = "UIFactory", menuName = "Create UI Factory", order = 51)]
public class UIFactory : ScriptableObject {

    [SerializeField] private MainScreen _mainScreenPrefab;
    [SerializeField] private ResultsPanel _resultsPanel;

    private Transform _canvas;


    public void Setup(Transform canvas) {
        _canvas = canvas;
    }

    public MainScreen CreatemainScreen() {
        return Instantiate(_mainScreenPrefab);
    }

    public ResultsPanel CreateResultsPanel() {
        return Instantiate(_resultsPanel, _canvas);
    }

}
