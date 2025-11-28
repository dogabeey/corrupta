using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "Corrupta/Game Constant Settings...", order = 1)]
public class GameConstants : ListedScriptableObject<GameConstants>
{
    [Header("MAP DRAWER SETTINGS")]
    public float mapTextScaleFactor = 1.0f;
    public float mapMaxColorDistanceDetection = 0.1f;
    public string mapShaderProvinceColorString = "_ProvinceColor";
    public string mapShaderProvinceSelectString = "_ProvinceSelect";

    public static GameConstants Instance => GameManager.Instance.gameConstants;

    public struct GameEvents
    {
        public static string SELECTED_CITY = "SELECTED_CITY";
    }
}