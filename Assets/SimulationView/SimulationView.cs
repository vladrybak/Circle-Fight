using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class SimulationView : MonoBehaviour {

    private static Vector2 _areaOffset;


    public void Setup(Vector2 size) {
        _areaOffset = size / 2f;
    }

    public static Vector2 ConvertToLocal(Simulation.Vector2 pos) {
        return new Vector2((float)pos.X - _areaOffset.x, (float)pos.Y - _areaOffset.y);
    }

}
