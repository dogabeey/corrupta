using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class CreateNewInstanceButtonDrawer
    : OdinAttributeDrawer<CreateNewInstanceButtonAttribute>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var property = Property;
        var valueEntry = property.ValueEntry;

        SirenixEditorGUI.BeginIndentedHorizontal();

        // Draw the actual field
        CallNextDrawer(label);

        // Only show button if null and type is ScriptableObject
        bool isMissingOrNull =
        valueEntry.WeakSmartValue == null ||
        (valueEntry.WeakSmartValue is UnityEngine.Object uObj && uObj == null);

        if (isMissingOrNull &&
            typeof(ScriptableObject).IsAssignableFrom(valueEntry.TypeOfValue))

        {
            if (GUILayout.Button(Attribute.buttonLabel, GUILayout.Width(22)))
            {
                CreateAndAssignAsset(valueEntry);
            }
        }

        SirenixEditorGUI.EndIndentedHorizontal();
    }

    private void CreateAndAssignAsset(IPropertyValueEntry entry)
    {
        var type = entry.TypeOfValue;

        ScriptableObject asset = ScriptableObject.CreateInstance(type);

        string folder = Attribute.GetPathFromEnum(Attribute.pathEnum);
        if (!AssetDatabase.IsValidFolder(folder))
        {
            System.IO.Directory.CreateDirectory(folder);
            AssetDatabase.Refresh();
        }

        string path = AssetDatabase.GenerateUniqueAssetPath(
            $"{folder}/New {type.Name}.asset"
        );

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        entry.WeakSmartValue = asset;
        EditorUtility.OpenPropertyEditor(asset);
    }
}
