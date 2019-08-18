#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;

public class PauseHandlerForEditor {

    private MainScreen _mainScreen;
    private Simulation.Simulator _simulator;


    public PauseHandlerForEditor(Simulation.Simulator simulator, MainScreen mainScreen) {
        _mainScreen = mainScreen;
        _simulator = simulator;
        EditorApplication.pauseStateChanged += PauseStaetChangedHandler;
    }

    private void PauseStaetChangedHandler(PauseState state) {
        if (state == PauseState.Paused) {
            _simulator.SetSpeed(0);
        } else {
            _simulator.SetSpeed(_mainScreen.GetSpeedSliderValue());
        }
    }

}
#endif
