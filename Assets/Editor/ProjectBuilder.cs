using UnityEditor;
using UnityEngine;

public class ProjectBuilder : MonoBehaviour
{
    static void PerformBuild() {
        var scenes = EditorBuildSettings.scenes;
        string[] sceneNames = new string[scenes.Length];
        for(int i=0;i<scenes.Length;i++) {
            sceneNames[i] = scenes[i].path;
            Debug.Log("Scene added to build:" + sceneNames[i]);
        }
        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = sceneNames;
        options.locationPathName = "Assets/Builds";
        options.target = EditorUserBuildSettings.activeBuildTarget;
        Debug.Log("Start building player");
        BuildPipeline.BuildPlayer(options);
        Debug.Log("Building complete");
    }
}
