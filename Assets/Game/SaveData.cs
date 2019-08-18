using Simulation;
using System;

[Serializable]
public class SaveData {

    public CircleData[] Circles;
    public Settings Settings;
    public float Speed;


    public SaveData(CircleData[] circles, Settings settings, float speed) {
        Circles = circles;
        Settings = settings;
        Speed = speed;
    }

}