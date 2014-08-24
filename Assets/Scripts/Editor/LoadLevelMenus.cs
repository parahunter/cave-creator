using UnityEngine;
using System.Collections;
using UnityEditor;

public static class LoadLevelMenus  
{
	[MenuItem("Utilities/Open scene/Menu #%m")]
	public static void Loadmenu()
	{
		EditorApplication.Beep();
		Debug.LogWarning("changing to menu scene");
		EditorApplication.OpenScene("Assets/MenuScreens/MenuController/MenuControllerScene.unity");	
	}
	
	[MenuItem("Utilities/Open scene/Game #%g")]
	public static void LoadGame()
	{
		EditorApplication.Beep();
		Debug.LogWarning("changing to game scene");
		EditorApplication.OpenScene("Assets/Levels/LevelGenerationAndPlayerIntegration.unity");	
	}
	
	[MenuItem("Utilities/Open scene/Beat Detection test #%b")]
	public static void LoadBeatScene()
	{
		EditorApplication.Beep();
		Debug.LogWarning("changing to beat detection test scene");
		EditorApplication.OpenScene("Assets/Levels/BeatDetection/BeatDetectionTest.unity");	
	}
}
