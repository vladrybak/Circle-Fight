#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;

public class PauseHandlerForEditor {

    private Slider _speedSlider;
    private float _speedBeforePause = 1;

    public PauseHandlerForEditor(Slider speedSlider) {
        _speedSlider = speedSlider;
        EditorApplication.pauseStateChanged += PauseStaetChangedHandler;
    }

    private void PauseStaetChangedHandler(PauseState state) {
        if (state == PauseState.Paused) {
            _speedBeforePause = _speedSlider.value;
            _speedSlider.value = 0;
        } else {
            _speedSlider.value = _speedBeforePause;
        }
    }

}
#endif
