using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public Text output;

    public InputField usernameField;
    public InputField passwordField;

    public void TryLogin()
    {
        Player player = GameModel.ds.GetPlayer(usernameField.text.ToLower());
        if (player != null)
        {
            if (player.Password == passwordField.text)
            {
                GameModel.ChangePlayer(player);
                ViewController.ShowView(ViewController.GameView.MainGame);
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

    public void TryRegister()
    {
        string username = usernameField.text.Trim();
        username = username.ToLower();

        string password = passwordField.text;

        if (username.Length <= 3 || password.Length < 7)
        {
            output.text = "Username must be more than 3 characters and password must be greater than 7 characters";
        }
        else
        {
            Player player = GameModel.ds.GetPlayer(username);
            if (player == null)
            {
                Player newPlayer = Player.New(username, password);
                GameModel.ChangePlayer(newPlayer);
                ViewController.ShowView(ViewController.GameView.MainGame);
            }
            else
            {
                output.text = "Username already taken";
            }
        }
    }
}
