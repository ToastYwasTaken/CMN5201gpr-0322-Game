using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossVictory : MonoBehaviour
{
    private void Awake()
    {
        RefLib.Player.gameObject.GetComponent<EntityStats>().OnDeath += OnDeath;
        gameObject.GetComponent<EntityStats>().OnDeath += OnDeath;
    }
    public void OnDeath()
    {
        Invoke("ResetScene", 3f);
    }
    void ResetScene()
    {
        RefLib.sPlayerCtrl.SwitchRestartMenu();
    }
}
