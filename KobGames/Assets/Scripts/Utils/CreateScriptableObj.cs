using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
#if UNITY_EDITOR
public static class ScriptableObjectUtility
{
    [MenuItem("Assets/Create/GameLevelData")]
    public static void Create_GameLevelData()
    {
        ScriptableObjectUtility.CreateAsset<GameLevelData>();
    }
    [MenuItem("Assets/Create/GameStateEventObj")]
    public static void Create_GameStateEventObj()
    {
        ScriptableObjectUtility.CreateAsset<GameStateEventObj>();
    }
    [MenuItem("Assets/Create/CharacterData")]
    public static void Create_CharacterData()
    {
        ScriptableObjectUtility.CreateAsset<CharacterData>();
    }
    [MenuItem("Assets/Create/LevelData")]
    public static void Create_LevelData()
    {
        ScriptableObjectUtility.CreateAsset<LevelData>();
    }

    [MenuItem("Assets/Create/YourClass")]
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName =
                AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
#endif