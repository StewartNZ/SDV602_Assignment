using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Text;

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

        StringBuilder story = new StringBuilder();

        pCmdStr = pCmdStr.ToLower();
        String[] parts = pCmdStr.Split(' '); // tokenise the command

        if (parts.Length > 0)
        {// process the tokens
            if (ViewController.currentView == ViewController.GameView.MainGame)
            {
                switch (parts[0])
                {
                    case "story":
                        Debug.Log("Telling Current Story");
                        story.AppendLine(GameModel.currentLocale.Story); 
                        story.AppendLine("\nYour sensors tell you there are " + GameModel.currentLocale.NumOfPlayers + " ships in orbit");
                        //output["story"] = GameModel.currentLocale.Story;
                        break;

                    case "go":
                        switch (parts[1])
                        {
                            case "north":
                                Debug.Log("Got go North");
                                if (GameModel.TryMove("North"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    //output["story"] = GameModel.currentLocale.Story;
                                    story.AppendLine(GameModel.currentLocale.Story);
                                    story.AppendLine("\nAs you arrive you see " + GameModel.currentLocale.NumOfPlayers + " ships in orbit on your sensors");
                                }
                                else
                                {
                                    story.AppendLine("Sorry can't go north");
                                    //output["story"] = "Sorry can't go north";
                                }

                                break;

                            case "south":
                                Debug.Log("Got go South");
                                if (GameModel.TryMove("South"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    //output["story"] = GameModel.currentLocale.Story;
                                    story.AppendLine(GameModel.currentLocale.Story);
                                    story.AppendLine("\nAs you arrive you see " + GameModel.currentLocale.NumOfPlayers + " ships in orbit on your sensors");
                                }
                                else
                                {
                                    story.AppendLine("Sorry can't go south");
                                    //output["story"] = "Sorry can't go south";
                                }
                                break;

                            case "east":
                                Debug.Log("Got go East");
                                if (GameModel.TryMove("East"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    //output["story"] = GameModel.currentLocale.Story;
                                    story.AppendLine(GameModel.currentLocale.Story);
                                    story.AppendLine("\nAs you arrive you see " + GameModel.currentLocale.NumOfPlayers + " ships in orbit on your sensors");
                                }
                                else
                                {
                                    story.AppendLine("Sorry can't go east");
                                    //output["story"] = "Sorry can't go east";
                                }
                                break;

                            case "west":
                                Debug.Log("Got go West");
                                if (GameModel.TryMove("West"))
                                {
                                    output["name"] = GameModel.currentLocale.Name;
                                    //output["story"] = GameModel.currentLocale.Story;
                                    story.AppendLine(GameModel.currentLocale.Story);
                                    story.AppendLine("\nAs you arrive you see " + GameModel.currentLocale.NumOfPlayers + " ships in orbit on your sensors");
                                }
                                else
                                {
                                    story.AppendLine("Sorry can't go west");
                                    //output["story"] = "Sorry can't go west";
                                }
                                break;

                            default:
                                Debug.Log("Do not know how to go there");
                                output["story"] = "Do not know how to go there";
                                break;
                        }// end switch
                        break;

                    case "inventory":
                        Debug.Log("Showing Inventory");
                        story.AppendLine("Showing Inventory");
                        //output["story"] = "Showing Inventory";
                        ViewController.ShowView(ViewController.GameView.Inventory);
                        break;

                    case "map":
                        Debug.Log("Showing Map");
                        story.AppendLine("Showing Map");
                        //output["story"] = "Showing Map";
                        ViewController.ShowView(ViewController.GameView.Map);
                        break;

                    case "help":
                        Debug.Log("Toggling Help");
                        story.AppendLine("Showing Help");
                        //output["story"] = "Showing help";
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
                            //output["story"] = GameModel.currentLocale.Story;
                            story.AppendLine(GameModel.currentLocale.Story);
                            story.AppendLine("\nYour sensors tell you there are " + GameModel.currentLocale.NumOfPlayers + " ships in orbit");
                            ViewController.ShowView(ViewController.GameView.MainGame);
                            ViewController.currentView = ViewController.GameView.MainGame;
                        }
                        break;

                    case "help":
                        Debug.Log("Toggling Help");
                        story.AppendLine("Showing Help");
                        //output["story"] = "Showing help";
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

        if (story.Length > 0)
            output["story"] = story.ToString().Trim();

        return output;

    }// Parse
}


