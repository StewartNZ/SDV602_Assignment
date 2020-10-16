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

    public String Parse(String pCmdStr)
    {
        string strResult = "Do not understand";
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
                            case "location":
                                Debug.Log("Telling Current Location");
                                strResult = GameModel.currentLocale.ToString();
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
                                    strResult = "Sorry can't go North\n" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }

                                break;

                            case "south":
                                Debug.Log("Got go South");
                                nextLocale = GameModel.currentLocale.getLocation("South");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go South\n" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                break;

                            case "east":
                                Debug.Log("Got go East");
                                nextLocale = GameModel.currentLocale.getLocation("East");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go East\n" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                break;

                            case "west":
                                Debug.Log("Got go West");
                                nextLocale = GameModel.currentLocale.getLocation("West");
                                if (nextLocale == null)
                                {
                                    strResult = "Sorry can't go West\n" + GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                else
                                {
                                    GameModel.currentLocale = nextLocale;
                                    strResult = GameModel.currentLocale.Name + " " + GameModel.currentLocale.Story;
                                }
                                break;

                            default:
                                Debug.Log("Do not know how to go there");
                                strResult = "Do not know how to go there";
                                break;
                        }// end switch
                        break;

                    case "show":
                        switch (parts[1])
                        {
                            case "inventory":
                                Debug.Log("Showing Inventory");
                                strResult = "Showing Inventory";
                                GameModel.ShowView(GameModel.GameView.Inventory);
                                break;

                            case "map":
                                Debug.Log("Showing Map");
                                strResult = "Showing Map";
                                GameModel.ShowView(GameModel.GameView.Map);
                                break;

                            default:
                                Debug.Log("Do not know how to show that");
                                strResult = "Do not know how to show that";
                                break;
                        }
                        break;

                    case "help":
                        Debug.Log("Showing Help");
                        strResult = "Commands: \ncurrent (location) \ngo (south/west/north/east) \nshow (inventory/map) \nhelp";
                        break;

                    default:
                        Debug.Log("134Do not understand");
                        strResult = "Do not understand";
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
                            strResult = "Cannot go back further";
                        }
                        else
                        {
                            Debug.Log("Returning to Main View");
                            strResult = GameModel.currentLocale.ToString();
                            GameModel.ShowView(GameModel.GameView.MainGame);
                            GameModel.currentView = GameModel.GameView.MainGame;
                        }
                        break;

                    case "help":
                        Debug.Log("Showing Help");
                        strResult = "Commands: \nback";
                        break;

                    default:
                        Debug.Log("165Do not understand");
                        strResult = "Do not understand";
                        break;
                }
            }
        }
        else
        {
            Debug.Log("173Do not understand");
            strResult = "Do not understand";
        }

        return strResult;

    }// Parse
}


