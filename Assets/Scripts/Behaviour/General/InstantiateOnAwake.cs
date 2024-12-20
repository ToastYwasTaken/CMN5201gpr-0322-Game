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
*   22.4.22 JA created 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnAwake : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    private void Awake()
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.position = transform.position;
        go.transform.SetParent(transform.root);
    }
}
