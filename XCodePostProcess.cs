using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
using System.IO;

public static class XCodePostProcess
{
	public static void Main()
	{
		// Not used, but required to build a DLL
	}

	[PostProcessBuild]
	public static void OnPostProcessBuild( BuildTarget target, string path )
	{
		Debug.LogWarning("Zynga XUPorter OnPostProcessBuild");

		if (target != BuildTarget.iPhone) {
			Debug.LogWarning("Zynga XUPorter OnPostProcessBuild > Target is not iPhone. XCodePostProcess will not run");
			return;
		}

		// Create a new project object from build target
		XCProject project = new XCProject( path );

		// Find and run through all projmods files to patch the project.
		//Please pay attention that ALL projmods files in your project folder will be excuted!
		string[] files = Directory.GetFiles( Application.dataPath, "*.projmods", SearchOption.AllDirectories );
		foreach( string file in files ) {
			project.ApplyMod( file );
		}

		// Finally save the xcode project
		project.Save();

		Debug.LogWarning("Zynga XUPorter OnPostProcessBuild Complete");
	}
}