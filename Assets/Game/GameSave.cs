using Simulation;
using System;
using UnityEngine;

public class GameSave {

    public const string SAVE_KEY = "save";
    public static bool HasSave { get { return PlayerPrefs.HasKey(SAVE_KEY); } }

    private Simulator _simulator;
    private Action<SaveData> _loadHandler;


    public GameSave(Simulator simulator, Action<SaveData> loadHandler) {
        _simulator = simulator;
        _loadHandler = loadHandler;
    }

	public void Save(float speed) {
        var circles = _simulator.GetCirclesData();
        var saveJson = JsonUtility.ToJson(new SaveData(circles, _simulator.SimulationSettings, speed));
        PlayerPrefs.SetString("save", saveJson);
    }

    public void Load() {
        if (!HasSave)
            return;

        string saveJson = PlayerPrefs.GetString("save");
        SaveData save = JsonUtility.FromJson<SaveData>(saveJson);
        _loadHandler(save);
     }

}
