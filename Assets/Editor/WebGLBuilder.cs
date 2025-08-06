using UnityEditor;

public class WebGLBuilder
{
    [MenuItem("Build/Build WebGL")]
    public static void Build()
    {
        string buildPath = "webgl_build";

        BuildPipeline.BuildPlayer(
            //new[] { "Assets/Scenes/Bootstrap.unity", "Assets/Scenes/Menu.unity", "Assets/Scenes/Gameplay.unity" },
            new[] { "Assets/Scenes/Menu.unity",
                "Assets/Scenes/LevelSelect.unity",
                "Assets/Scenes/Level_1.unity",
                "Assets/Scenes/Level_2.unity",
                "Assets/Scenes/Level_3.unity",
                "Assets/Scenes/Level_4.unity",
                "Assets/Scenes/Level_5.unity",
                "Assets/Scenes/EndScreen.unity" },
            buildPath,
            BuildTarget.WebGL,
            BuildOptions.None
        );
    }
}