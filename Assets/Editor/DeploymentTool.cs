using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using System.Globalization;

public class DeploymentTool {
	[MenuItem("Deploy/Build GitHub Page")]
	static void BuildProductionPage () {
		string[] scenes = new string[EditorBuildSettings.scenes.Length];
		for (int i = 0; i < scenes.Length; i++) {
			scenes[i] = EditorBuildSettings.scenes[i].path;
		}

		var destPath = EditorUtility.SaveFolderPanel("Build a page to directory", "", "");
		var tempPath = Path.Combine(destPath, "main");

		BuildPipeline.BuildPlayer(scenes, tempPath, BuildTarget.WebPlayer, BuildOptions.None);

		System.IO.File.Move(Path.Combine(tempPath, "main.unity3d"), Path.Combine(destPath, "main.unity3d"));
		System.IO.Directory.Delete(tempPath, true);

		string[] lines = {
			"---",
			"layout: webplayer",
			"title: Preview",
			"---",
			"",
			"(Built on " + System.DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("ja-JP")) + ")",
			"",
			"このゲーム「ピラニアン」は「第２回 京都インディーズゲームセミナー Unity 入門講座」のライブコーディングセッションにおいて作成されたゲームです。このページでは、完成を目指して開発している途中のバージョンをプレイできます。",
			"",
			"- [トップページへ戻る](/piranhan)"
		};

		System.IO.File.WriteAllLines(Path.Combine(destPath, "index.md"), lines, new UTF8Encoding(false));
	}
}
