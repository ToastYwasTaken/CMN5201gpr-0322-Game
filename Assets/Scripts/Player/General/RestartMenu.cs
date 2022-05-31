using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public void RestartGame()
    {
        ResetStuff();
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        ResetStuff();
        SceneManager.LoadScene(0);
    }
    void ResetStuff()
    {
        BSPMap.s_allRooms.Clear();
        BSPMap.s_allHallWays.Clear();
        Destroy(transform.root.Find("Enemys"));
    }
}
