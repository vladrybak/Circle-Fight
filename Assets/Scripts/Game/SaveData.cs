using Simulation;
using System;

[Serializable]
public class SaveData {

    public CircleData[] Spawned;
    public CircleData[] WaitingForSpawn;
    public Settings Settings;
    public float Speed;
    public int Iterations;


    public SaveData(CircleData[] waitingForSpawn, CircleData[] spawned, Settings settings, float speed, int iterations) {
        WaitingForSpawn = waitingForSpawn;
        Spawned = spawned;
        Settings = settings;
        Iterations = iterations;
        Speed = speed;
    }

}