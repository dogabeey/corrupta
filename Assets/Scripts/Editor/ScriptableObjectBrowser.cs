#if UNITY_EDITOR
using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectBrowser : OdinMenuEditorWindow
{
    [MenuItem("Corrupta/ScriptableObject Browser")]
    private static void Open()
    {
        var window = GetWindow<ScriptableObjectBrowser>();
        window.titleContent = new GUIContent("SO Browser");
        window.Show();
    }

    // Optional: control menu width like a “tabs” sidebar
    public override float MenuWidth => 230f;

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree
        {
            DefaultMenuStyle = OdinMenuStyle.TreeViewStyle
        };
        tree.Config.DrawSearchToolbar = true;

        // Get all non-abstract ScriptableObject types in the project
        var soTypes = TypeCache.GetTypesDerivedFrom<ManageableScriptableObject>()
            .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
            .OrderBy(t => t.Name);

        foreach (var type in soTypes)
        {
            string typeName = type.Name;
            string[] guids = AssetDatabase.FindAssets($"t:{typeName}");

            if (guids.Length == 0)
            {
                tree.Add($"{typeName}", new EmptyTypePage(type));
                continue;
            }

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (asset == null) continue;
                var item = tree.AddObjectAtPath($"{typeName}/{asset.name}", asset);
                item.AddIcon(AssetPreview.GetMiniThumbnail(asset));
                
            }
        }

        return tree;
    }

    // Little page shown when a type has no assets
    private class EmptyTypePage
    {
        [ShowInInspector, ReadOnly]
        public Type Type { get; }

        [ShowInInspector, ReadOnly, MultiLineProperty]
        public string Info => $"No ScriptableObject assets of type '{Type.Name}' were found in the project.";

        public EmptyTypePage(Type type)
        {
            Type = type;
        }

        [Button("Add New Instance", Icon = SdfIconType.Newspaper)]
        public void AddNewInstance(string folderName, string objectName)
        {
            if (folderName.IsNullOrWhitespace())
                folderName = Type.Name + "s";

            var instance = ScriptableObject.CreateInstance(Type);

            string rootFolder = "Assets/Resources/ScriptableObjects";

            // Ensure root folder exists (optional safety)
            if (!AssetDatabase.IsValidFolder(rootFolder))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "ScriptableObjects");
            }

            // Create subfolder and get its actual path:
            string guid = AssetDatabase.CreateFolder(rootFolder, folderName);
            string folderPath = AssetDatabase.GUIDToAssetPath(guid);  // <- real path

            // Build full asset path
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(
                $"{folderPath}/{objectName}.asset"
            );

            // Create asset
            AssetDatabase.CreateAsset(instance, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = instance;

            EditorUtility.DisplayDialog(
                "Asset Created",
                $"{objectName} has been created in {folderName}.",
                "OK"
            );

        }
    }

    protected override void OnBeginDrawEditors()
    {
        base.OnBeginDrawEditors();

        var sel = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if (sel.SelectedValue is ScriptableObject so)
            {
                if (SirenixEditorGUI.ToolbarButton("Ping"))
                {
                    EditorGUIUtility.PingObject(so);
                }

                if (SirenixEditorGUI.ToolbarButton("Select"))
                {
                    Selection.activeObject = so;
                }
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
    private void AddContextMenu(OdinMenuItem item, ScriptableObject asset)
    {
        item.OnRightClick += i =>
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("Ping"), false, () =>
            {
                EditorGUIUtility.PingObject(asset);
            });

            menu.AddItem(new GUIContent("Select"), false, () =>
            {
                Selection.activeObject = asset;
            });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Open Inspector"), false, () =>
            {
                Selection.activeObject = asset;
                EditorGUIUtility.PingObject(asset);
            });

            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Delete"), false, () =>
            {
                string path = AssetDatabase.GetAssetPath(asset);
                if (EditorUtility.DisplayDialog("Delete?", $"Delete {asset.name}?", "Yes", "No"))
                {
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                    ForceMenuTreeRebuild();
                }
            });

            menu.ShowAsContext();
        };
    }

}
#endif