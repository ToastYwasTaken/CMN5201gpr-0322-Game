using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private float _range = 30f;
        private AIFSMAgent _aIFSM;
        private NavMeshAgent _agent;
        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            _aIFSM = GetComponent<AIFSMAgent>();
            _agent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag(_playerTag);
        }

        // Update is called once per frame
        void Update()
        {
            if (_aIFSM == null || _player == null) return;
        
            float distance = Vector3.Distance(_player.transform.position, gameObject.transform.position);

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

