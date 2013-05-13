using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;

public class DeploymentTool {
	[MenuItem("Deploy/Build Production Page")]
	static void BuildProductionPage () {
		string[] scenes = new string[EditorBuildSettings.scenes.Length];
		for (int i = 0; i < scenes.Length; i++) {
			scenes[i] = EditorBuildSettings.scenes[i].path;
		}

		var path = EditorUtility.SaveFolderPanel("Build a page to directory", "", "");
		path = Path.Combine(path, "main");

		BuildPipeline.BuildPlayer(scenes, path, BuildTarget.WebPlayer, BuildOptions.None);

		System.IO.File.WriteAllText(Path.Combine(path, "main.md"),
		                            "---\n" +
									"layout: webplayer\n" +
		                            "title: Preview\n" +
		                            "---\n" +
		                            "このゲーム「ピラニアン」は「第２回 京都インディーズゲームセミナー Unity入門講座」のライブコーディングセッションにおいて作成されたゲームです。このページでは、完成を目指して開発している途中のバージョンをプレイできます。\n\n" +
		                            "- [トップページへ戻る](/piranhan)",
		                            Encoding.UTF8);
	}
}
