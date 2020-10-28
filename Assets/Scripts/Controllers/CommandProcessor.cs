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

        if (parts.Length > 0)
        {// process the tokens
            if (ViewController.currentView == ViewController.GameView.MainGame)
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
                                if (GameModel.TryMove("North"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    output["story"] = "Sorry can't go north";
                                }

                                break;

                            case "south":
                                Debug.Log("Got go South");
                                if (GameModel.TryMove("South"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    output["story"] = "Sorry can't go south";
                                }
                                break;

                            case "east":
                                Debug.Log("Got go East");
                                if (GameModel.TryMove("East"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    output["story"] = "Sorry can't go east";
                                }
                                break;

                            case "west":
                                Debug.Log("Got go West");
                                if (GameModel.TryMove("West"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    output["story"] = GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    output["story"] = "Sorry can't go west";
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
                                ViewController.ShowView(ViewController.GameView.Inventory);
                                break;

                            case "map":
                                Debug.Log("Showing Map");
                                output["story"] = "Showing Map";
                                ViewController.ShowView(ViewController.GameView.Map);
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
                        ViewController.ToggleHelp();
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
                        if (ViewController.currentView == ViewController.GameView.MainGame)
                        {
                            Debug.Log("Cannot go back further");
                            output["story"] = "Cannot go back further";
                        }
                        else
                        {
                            Debug.Log("Returning to Main View");
                            output["story"] = GameModel.currentLocale.Story;
                            ViewController.ShowView(ViewController.GameView.MainGame);
                            ViewController.currentView = ViewController.GameView.MainGame;
                        }
                        break;

                    case "help":
                        Debug.Log("Toggling Help");
                        output["story"] = "Showing help";
                        ViewController.ToggleHelp();
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


