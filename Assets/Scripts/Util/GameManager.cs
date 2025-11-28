using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameConstants gameConstants;
    public EventManager eventManager;

    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        InitGame();
    }

    private static void InitGame()
    {
        Country.InitCountry("Turkey", "stormwind");

        Country.RandomizeAll();
    }
}
