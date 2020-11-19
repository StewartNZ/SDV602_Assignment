using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    private struct CanvasHelpCombo
    {
        public CanvasHelpCombo(GameObject _canvas, string[] _helpCommands)
        {
            Canvas = _canvas;
            HelpCommands = _helpCommands;
        }

        public GameObject Canvas { get; }
        public string[] HelpCommands { get; }
    }

    public enum GameView
    {
        Login,
        MainGame,
        Inventory,
        Map
    }

    public GameObject loginCanvas;
    public GameObject mainCanvas;
    public GameObject mapCanvas;
    public GameObject inventoryCanvas;

    public GameObject helpCommandsCanvas;
    public Text helpCommands;

    private static ViewController instance;

    public static GameView currentView;

    private static Dictionary<GameView, CanvasHelpCombo> views = new Dictionary<GameView, CanvasHelpCombo>();

    public void Awake()
    {
        instance = this;

        views.Add(GameView.Login, new CanvasHelpCombo(loginCanvas, new string[0]));
        views.Add(GameView.MainGame, new CanvasHelpCombo(mainCanvas, mainGameHelpCommands));
        views.Add(GameView.Map, new CanvasHelpCombo(mapCanvas, mapHelpCommands));
        views.Add(GameView.Inventory, new CanvasHelpCombo(inventoryCanvas, inventoryHelpCommands));
    }

    private static string[] mainGameHelpCommands = new string[]
    {
        "- go (north/south/east/west)",
        "- map",
        "- inventory",
        "- story"
    };

    private static string[] inventoryHelpCommands = new string[]
    {
        "- back"
    };

    private static string[] mapHelpCommands = new string[]
    {
        "- back"
    };

    public static void ShowView(GameView prGameView)
    {
        foreach (var view in views)
        {
            if (view.Key == prGameView)
            {
                view.Value.Canvas.SetActive(true);
                UpdateHelpCommands(view.Value.HelpCommands);

                InputField currentInput = view.Value.Canvas.GetComponentInChildren<InputField>();
                currentInput.Select();
                currentInput.ActivateInputField();

                currentView = prGameView;
            }
            else
            {
                view.Value.Canvas.SetActive(false);
            }
        }
    }

    public static void ToggleHelp()
    {
        instance.helpCommandsCanvas.SetActive(instance.helpCommandsCanvas.activeInHierarchy ? false : true);
    }

    private static void UpdateHelpCommands(string[] commands)
    {
        StringBuilder combinedCommands = new StringBuilder();
        foreach (string line in commands)
        {
            combinedCommands.AppendLine(line);
        }

        instance.helpCommands.text = combinedCommands.ToString();
    }
}
