using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIController.cs
* Date : 29.05.2022
* Author : René Kraus (RK)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
******************************************************************************/
namespace AISystem
{
    /// <summary>
    /// Activates or deactivates the AI FSM depending on the distance to the player.
    /// </summary>
    public class AIController : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private float _range = 30f;
        private AIFSMAgent _aIFSM;
        private NavMeshAgent _agent;
        private GameObject _player;

        // Start is called before the first frame update
        private void Start()
        {
            _aIFSM = GetComponent<AIFSMAgent>();
            _agent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag(_playerTag);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_aIFSM == null || _player == null) return;
        
            // Distance to Player
            float distance = Vector3.Distance(_player.transform.position, gameObject.transform.position);

            // Check distance to Player
            if (distance <= _range)
            {
                _agent.enabled =true;
                _aIFSM.enabled = true;
            }
            else
            {
                _agent.enabled = false;
                _aIFSM.enabled = false;
            }
            
        }
    }
}

