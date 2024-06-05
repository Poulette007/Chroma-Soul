using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class LauncherUI : MonoBehaviour
{
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject accountCreationMenu;
    public void LaunchGame()
    {
        string path = Application.dataPath + "/../Build/GameBuild/My project.exe";
        Process.Start(path);
    }

    public void LoadAccountCreationMenu()
    {
        accountCreationMenu.SetActive(true);
        loginMenu.SetActive(false);
    }

    public void LoadLoginMenu()
    {
        loginMenu.SetActive(true);
        accountCreationMenu.SetActive(false);
    }
}