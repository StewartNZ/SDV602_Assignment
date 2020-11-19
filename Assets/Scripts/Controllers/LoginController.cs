using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public Text output;

    public InputField usernameField;
    public InputField passwordField;

    public TextInput mainGameInput;

    public void LoginButton()
    {
        GameModel.ds.GetPlayer(usernameField.text.ToLower(), TryLogin, GameModel.ds.jsnReceiverDel);
    }

    public void TryLogin(List<Player> players)
    {
        //Player player = GameModel.ds.GetPlayer(usernameField.text.ToLower());
        GameModel.waitingOn -= 1;
        Player player = players[0];

        if (player != null)
        {
            if (player.Password == passwordField.text)
            {
                GameModel.ChangePlayer(player);
                ViewController.ShowView(ViewController.GameView.MainGame);
                mainGameInput.UpdateLocation();
            }
            else
            {
                output.text = "Incorrect password, please try again or register a new account";
            }
        }
        else
        {
            output.text = "Account does not exist, please register a new one";
        }
    }

    public void RegisterButton()
    {
        GameModel.ds.GetPlayer(usernameField.text.ToLower(), TryLogin, TryRegister);
    }

    public void TryRegister(JsnReceiver receiver)
    {
        GameModel.waitingOn -= 1;

        string username = usernameField.text.Trim();
        username = username.ToLower();

        string password = passwordField.text;

        if (username.Length <= 3 || password.Length < 7)
        {
            output.text = "Username must be more than 3 characters and password must be greater than 7 characters";
        }
        else
        {
            Player newPlayer = Player.New(username, password);
            GameModel.ChangePlayer(newPlayer);
            //GameModel.ds.StorePlayerToJsn(newPlayer);
            ViewController.ShowView(ViewController.GameView.MainGame);
            mainGameInput.UpdateLocation();
        }
    }
}
