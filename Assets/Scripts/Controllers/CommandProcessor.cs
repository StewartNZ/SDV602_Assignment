using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

public class CommandProcessor
{

    public CommandProcessor()
    {
    }

    public Dictionary<string, string> Parse(String pCmdStr)
    {
        Dictionary<string, string> output = new Dictionary<string, string>()
        {
            {"name", GameModel.currentLocale.Name},
            {"story", "Do not understand"}
        };

        pCmdStr = pCmdStr.ToLower();
        String[] parts = pCmdStr.Split(' '); // tokenise the command
        Location nextLocale;

        if (parts.Length > 0)
        {// process the tokens
            if (GameModel.currentView == GameModel.GameView.MainGame)
            {
                switch (parts[0])
                {
                    case "current":
                        switch (parts[1])
                        {
                            case "story":
                                Debug.Log("Telling Current Story");
                                output["story"] = GameModel.currentLocale.Story;
                                break;

                            default:
                                break;
                        }
                        break;

                    case "go":
                        switch (parts[1])
                        {
                            case "north":
                                Debug.Log("Got go North");
                                nextLocale = GameModel.currentLocale.getLocation("North");
                                if (nextLocale == null)
                                    output["story"] = "Sorry can't go north";
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }

                                break;

                            case "south":
                                Debug.Log("Got go South");
                                nextLocale = GameModel.currentLocale.getLocation("South");
                                if (nextLocale == null)
                                {
                                    output["story"] = "Sorry can't go South";
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                break;

                            case "east":
                                Debug.Log("Got go East");
                                nextLocale = GameModel.currentLocale.getLocation("East");
                                if (nextLocale == null)
                                {
                                    output["story"] = "Sorry can't go East";
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                break;

                            case "west":
                                Debug.Log("Got go West");
                                nextLocale = GameModel.currentLocale.getLocation("West");
                                if (nextLocale == null)
                                {
                                    output["story"] = "Sorry can't go West";
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                break;

                            default:
                                Debug.Log("Do not know how to go there");
                                output["story"] = "Do not know how to go there";
                                break;
                        }// end switch
                        break;

                    case "show":
                        switch (parts[1])
                        {
                            case "inventory":
                                Debug.Log("Showing Inventory");
                                output["story"] = "Showing Inventory";
                                GameModel.ShowView(GameModel.GameView.Inventory);
                                break;

                            case "map":
                                Debug.Log("Showing Map");
                                output["story"] = "Showing Map";
                                GameModel.ShowView(GameModel.GameView.Map);
                                break;

                            default:
                                Debug.Log("Do not know how to show that");
                                output["story"] = "Do not know how to show that";
                                break;
                        }
                        break;

                    case "help":
                        Debug.Log("Toggling Help");
                        output["story"] = "Showing help";
                        GameModel.ToggleHelp();
                        break;

                    default:
                        Debug.Log("Do not understand");
                        output["story"] = "Do not understand";
                        break;

                }// end switch
            }
            else
            {
                switch (parts[0])
                {
                    case "back":
                        if (GameModel.currentView == GameModel.GameView.MainGame)
                        {
                            Debug.Log("Cannot go back further");
                            output["story"] = "Cannot go back further";
                        }
                        else
                        {
                            Debug.Log("Returning to Main View");
                            output["story"] = GameModel.currentLocale.Story;
                            GameModel.ShowView(GameModel.GameView.MainGame);
                            GameModel.currentView = GameModel.GameView.MainGame;
                        }
                        break;

                    case "help":
                        Debug.Log("Showing Help");
                        output["story"] = "Commands: \nback";
                        break;

                    default:
                        Debug.Log("165Do not understand");
                        output["story"] = "Do not understand";
                        break;
                }
            }
        }
        else
        {
            Debug.Log("173Do not understand");
            output["story"] = "Do not understand";
        }

        return output;

    }// Parse
}


