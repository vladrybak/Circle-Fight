using Simulation;
using System.IO;
using UnityEngine;

public class Configuration  {

    private const string SETTINGS_FILENAME = "data.txt";

	public static Settings LoadGameConfig() {
        var path = Path.Combine(Application.streamingAssetsPath, SETTINGS_FILENAME);
        var data = File.ReadAllText(path,System.Text.Encoding.UTF8);
        var config = JsonUtility.FromJson<Settings>(data);
        return config;
    }

}
