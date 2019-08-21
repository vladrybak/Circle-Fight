using Simulation;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver {

    public const string SAVE_KEY = "save";
    public static bool HasSave { get { return PlayerPrefs.HasKey(SAVE_KEY); } }

    private Simulator _simulator;
    private Action<SaveData> _loadHandler;


    public GameSaver(Simulator simulator) {
        _simulator = simulator;
    }

    public void WriteSave() {
        List<CircleData[]> data = _simulator.GetCirclesData();
        var waiting = data[0];
        var circles = data[1];
        var saveData = new SaveData(waiting, circles, _simulator.Settings, _simulator.Speed, _simulator.IterationsCount);
        var saveJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("save", saveJson);
    }

    public SaveData LoadSave() {
        if (!HasSave)
            return null;

        string saveJson = PlayerPrefs.GetString("save");
        return JsonUtility.FromJson<SaveData>(saveJson);
     }

}
