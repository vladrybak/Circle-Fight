using UnityEngine;

[CreateAssetMenu(fileName = "UIFactory", menuName = "Create UI Factory", order = 51)]
public class UIFactory : ScriptableObject {

    [SerializeField] private MainScreen _mainScreenPrefab;

    
    public MainScreen CreatemainScreen() {
        return Instantiate(_mainScreenPrefab);
    }
}
