using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "Corrupta/Game Constant Settings...", order = 1)]
public class GameConstants : ListedScriptableObject<GameConstants>
{
    [Header("MAP DRAWER SETTINGS")]
    public float mapTextScaleFactor = 1.0f;
    public float mapMaxColorDistanceDetection = 0.1f;
    public string mapShaderProvinceColorString = "_ProvinceColor";
    public string mapShaderProvinceSelectString = "_ProvinceSelect";
    [Header("ELECTION")]
    public int defaultGeneralElectionsPeriodYear = 4;
    public int defaultLocalElectionsPeriodYear = 4;
    public int defaultGeneralElectionsMonthOffset = 12; // 10 months after the start of the game, then every 4 years after that.
    public int defaultLocalElectionsMonthOffset = 6; // 6 months after the start of the game, then every 4 years after that.
    [Header("ECONOMY")]
    public float baseStartingMoney = 1000f;
    public float baseAdvsiorCost = 100f;
    public static GameConstants Instance => GameManager.Instance.gameConstants;

    public struct GameEvents
    {
        public const string TURN_PASSED = "TURN_PASSED";

        public const string SELECTED_CITY = "SELECTED_CITY";

        public const string ADVISOR_POOL_UPDATED = "ADVISOR_POOL_UPDATED";
    }
    public struct Gfx
    {
        public struct Icons
        {
            public struct AdvisorIcons
            {
                public const string pr_manager = "advisor_icons[pr_manager]";
                public const string chief_of_staff = "advisor_icons[chief_of_staff]";
                public const string opposition_researcher = "advisor_icons[opposition_researcher]";
                public const string finance_chairman = "advisor_icons[finance_chairman]";
            }

            public const string advisor_portraits = "advisor_portraits[{0}]";
        }
    }
}