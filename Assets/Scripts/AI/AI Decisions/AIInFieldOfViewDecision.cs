using UnityEngine;

namespace AISystem
{

    [CreateAssetMenu(menuName = "AI FSM/Decisions/In Field of View")]
    public class AIInFieldOfViewDecision : AIDecision
    {
        private Collider[] _colliders;
        private GameObject _gameObject;

        [Header("Scan Settings")]
        [SerializeField] private float _lookRadius = 5f;
        [SerializeField] private LayerMask _ignoreLayerForScan = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForScan = QueryTriggerInteraction.Ignore;

        [Header("FieldOfView Settings")]
        [SerializeField] private float _viewDistance = 10f;
        [SerializeField] private float _viewAngle = 120f;
        [SerializeField] private LayerMask _ignoreLayerForView = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForView = QueryTriggerInteraction.Ignore;

        [Header("Aura Settings")]
        [SerializeField] private bool _useAura = true;
        [SerializeField] private float _auraRadius = 5f;

        public override bool Decide(AIFSMAgent stateMachine)
        {
            Debug.Log($"AI Decision: {this.name}");
            var fov = new AIFieldOfView();

            _colliders = fov.LookAroundForColliders(stateMachine.transform.position, _lookRadius, _ignoreLayerForScan, _queryTriggerForScan);

            // Debug.Log($"Detected Colliders: {_colliders.Length}");

            _gameObject = fov.LookForGameObject(_colliders, stateMachine.PlayerTag);

            if (_gameObject == null) return false;

            return fov.InFieldOfView(stateMachine.transform, _gameObject.transform, stateMachine.PlayerTag,
                _viewDistance, _viewAngle, _ignoreLayerForView, _queryTriggerForView, _auraRadius, _useAura);
        }



    }
}

