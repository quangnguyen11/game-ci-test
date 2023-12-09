using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityBuilderAction
{
    public static class BuildAction
    {
        [MenuItem("File/Build WebGL")]
        public static void BuildWebGL()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenes = new string[sceneCount];
            for (var i = 0; i < sceneCount; i++) scenes[i] = SceneUtility.GetScenePathByBuildIndex(i);

            var buildPath = Path.Combine(Environment.CurrentDirectory, "Build/WebGL");

            BuildReport report = BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.WebGL, BuildOptions.DetailedBuildReport);
            BuildSummary summary = report.summary;
            if (summary.result == BuildResult.Succeeded) Debug.Log("Build Succeeded " + (summary.totalSize / 1_000_000) + " MB");
            if (summary.result == BuildResult.Failed) Debug.Log("Build Failed " + JsonUtility.ToJson(summary));
        }
    }
}
