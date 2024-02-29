#if UNITY_IOS
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class ConfigureBitcodePostProcess
{
    [PostProcessBuild]
    public static void EnableBitcode(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            PBXProject project = new PBXProject();
            project.ReadFromFile(projectPath);

            string targetGuid = project.GetUnityFrameworkTargetGuid();

            project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

            string[] targetNames = project.GetTargetNames();
            foreach (string targetName in targetNames)
            {
                if (targetNsame.EndsWith("OneSignalNotificationServiceExtension"))
                {
                    string targetGuidExtension = project.GetTargetGuidByName(targetName);
                    project.SetBuildProperty(targetGuidExtension, "ENABLE_BITCODE", "NO");
                }
            }

            File.WriteAllText(projectPath, project.WriteToString());
        }
    }
}
#endif
