using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    public class CreateProjectStructure : MonoBehaviour
    {
        [MenuItem("Tools/Create Project Structure")]
        public static void CreateStructure()
        {
            CreateFolder("Assets", true);
            CreateFolder("Assets/Scenes");
            CreateFolder("Assets/AssetStore");
            CreateFolder("Assets/Scripts");
            CreateFolder("Assets/Scripts/Player");
            CreateFolder("Assets/Scripts/StateMachine");
            CreateFolder("Assets/Scripts/EventBus");
            CreateFolder("Assets/Scripts/EventBus/Signals");
            CreateFolder("Assets/Scripts/Player");
            CreateFolder("Assets/Scripts/Enemies");
            CreateFolder("Assets/Scripts/UI");
            CreateFolder("Assets/Scripts/Managers");
            CreateFolder("Assets/Scripts/Utilities");
            CreateFolder("Assets/Resources");
            CreateFolder("Assets/Resources/Prefabs");
            CreateFolder("Assets/Resources/Materials");
            CreateFolder("Assets/Resources/Textures");
            CreateFolder("Assets/Resources/Animations");
            CreateFolder("Assets/Resources/Audio");
            CreateFolder("Assets/Plugins");
            CreateFolder("Assets/Tools");
            CreateFolder("Assets/Tools/Editor");
            CreateFolder("Assets/Tools/External");
            CreateFolder("Assets/ExternalTools");
            CreateFolder("Assets/FutureAssets");
            CreateFolder("Assets/FutureAssets/FutureCharacterModels");
            CreateFolder("Assets/FutureAssets/FutureEnvironmentAssets");

            AssetDatabase.Refresh();
            Debug.Log("Project structure created successfully.");
        }

        private static void CreateFolder(string path, bool isRoot = false)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parentFolder = Path.GetDirectoryName(path);
                string newFolderName = Path.GetFileName(path);

                if (!isRoot)
                    CreateFolder(parentFolder);

                AssetDatabase.CreateFolder(parentFolder, newFolderName);
            }
        }
    }
}