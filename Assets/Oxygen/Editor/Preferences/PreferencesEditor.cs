using UnityEditor;
using UnityEngine;

namespace Oxygen.Editor
{
	[InitializeOnLoad]
	public static class PreferencesEditor
	{
		static PreferencesEditor()
		{
			Load();
		}

		public static void Load()
		{
			Preferences.ClonerDistance = EditorPrefs.GetFloat("CLONER_DISTANCE");
			Preferences.ClonerDatabasePath = EditorPrefs.GetString("CLONER_DATABASEPATH");
			Preferences.ClonerDefaultIconPath = EditorPrefs.GetString("CLONER_DEFAULTICONPATH");
			Preferences.ClonerToggleTo2D = EditorPrefs.GetBool("CLONER_TOGGLETO2D");

			Preferences.TouchTriggerColor = new Color(EditorPrefs.GetFloat("TOUCHTRIGGER_COLORR"),
				                                      EditorPrefs.GetFloat("TOUCHTRIGGER_COLORG"),
													  EditorPrefs.GetFloat("TOUCHTRIGGER_COLORB"),
													  EditorPrefs.GetFloat("TOUCHTRIGGER_COLORA"));

			Preferences.KillZoneColor = new Color(EditorPrefs.GetFloat("KILLZONE_COLORR"),
										          EditorPrefs.GetFloat("KILLZONE_COLORG"),
										          EditorPrefs.GetFloat("KILLZONE_COLORB"),
										          EditorPrefs.GetFloat("KILLZONE_COLORA"));
			
			Preferences.PlayerStartSize = new Vector3(EditorPrefs.GetFloat("PLAYERSTART_SIZEX", 1f),
				                                      EditorPrefs.GetFloat("PLAYERSTART_SIZEY", 2f),
				                                      EditorPrefs.GetFloat("PLAYERSTART_SIZEZ", 1f));

			Preferences.DefaultLauncherPath = EditorPrefs.GetString("DEFAULT_LAUNCHERPATH");
			Preferences.DefaultLevelsPath = EditorPrefs.GetString("DEFAULT_LEVELSPATH");
		}

		public static void Save()
		{
			EditorPrefs.SetFloat("CLONER_DISTANCE", Preferences.ClonerDistance);
			EditorPrefs.SetString("CLONER_DATABASEPATH", Preferences.ClonerDatabasePath);
			EditorPrefs.SetString("CLONER_DEFAULTICONPATH", Preferences.ClonerDefaultIconPath);
			EditorPrefs.SetBool("CLONER_TOGGLETO2D", Preferences.ClonerToggleTo2D);
			
			EditorPrefs.SetFloat("TOUCHTRIGGER_COLORR", Preferences.TouchTriggerColor.r);
			EditorPrefs.SetFloat("TOUCHTRIGGER_COLORG", Preferences.TouchTriggerColor.g);
			EditorPrefs.SetFloat("TOUCHTRIGGER_COLORB", Preferences.TouchTriggerColor.b);
			EditorPrefs.SetFloat("TOUCHTRIGGER_COLORA", Preferences.TouchTriggerColor.a);

			EditorPrefs.SetFloat("KILLZONE_COLORR", Preferences.KillZoneColor.r);
			EditorPrefs.SetFloat("KILLZONE_COLORG", Preferences.KillZoneColor.g);
			EditorPrefs.SetFloat("KILLZONE_COLORB", Preferences.KillZoneColor.b);
			EditorPrefs.SetFloat("KILLZONE_COLORA", Preferences.KillZoneColor.a);
			
			EditorPrefs.SetFloat("PLAYERSTART_SIZEX", Preferences.PlayerStartSize.x);
			EditorPrefs.SetFloat("PLAYERSTART_SIZEY", Preferences.PlayerStartSize.y);
			EditorPrefs.SetFloat("PLAYERSTART_SIZEZ", Preferences.PlayerStartSize.z);
			
			EditorPrefs.SetString("DEFAULT_LAUNCHERPATH", Preferences.DefaultLauncherPath);
			EditorPrefs.SetString("DEFAULT_LEVELSPATH", Preferences.DefaultLevelsPath);
		}
	}
}