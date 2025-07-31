using UnityEditor;

public class WebGLBuilder
{
    [MenuItem("Build/Build WebGL")]
    public static void Build()
    {
        string buildPath = "webgl_build";

        BuildPipeline.BuildPlayer(
            //new[] { "Assets/Scenes/Bootstrap.unity", "Assets/Scenes/Menu.unity", "Assets/Scenes/Gameplay.unity" },
            new[] { "Assets/Scenes/Test.unity" },
            buildPath,
            BuildTarget.WebGL,
            BuildOptions.None
        );
    }
}