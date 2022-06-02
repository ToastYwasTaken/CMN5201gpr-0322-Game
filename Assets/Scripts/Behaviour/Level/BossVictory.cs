/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Rotateable.cs
* Date   : 17.04.22
* Author : Jan Apsel (JA)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*   25.4.22 JA created 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossVictory : MonoBehaviour
{
    private void Awake()
    {
        RefLib.sPlayer.gameObject.GetComponent<EntityStats>().OnDeath += OnDeath;
        gameObject.GetComponent<EntityStats>().OnDeath += OnDeath;
    }
    public void OnDeath()
    {
        GlobalValues.sIsEnemyAggro = false;
        Invoke("ResetScene", 3f);
    }
    public void OnVictory()
    {
        GlobalValues.sIsEnemyAggro = false;
        Invoke("Victory", 3f);
    }
    void ResetScene()
    {
        RefLib.sPlayerCtrl.SwitchRestartMenu();
    }
    void Victory()
    {
        RefLib.sPlayerCtrl.SwitchVictoryMenu();
    }
}
