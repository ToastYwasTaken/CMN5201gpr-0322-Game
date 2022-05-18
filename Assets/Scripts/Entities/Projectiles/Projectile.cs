using UnityEngine;

class Projectile : MonoBehaviour
{
    private ProjectileStats _projectileStats;
    public ProjectileStats ProjectileStats
    {
        get => _projectileStats;
        set { _projectileStats = value; }
    }

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ProjectileMovement _projectileMovement = null;
    [SerializeField] private ProjectileCollision _projectileCollision = null;    

    private void Update()
    {
        if (_projectileMovement != null)
            transform.position += _projectileMovement.MovementVector(ProjectileStats.ProjectileSpeed, transform);
    }

    private void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_projectileMovement == null) Debug.LogWarning("Projectile Movement Behaviour isnt set!");
        if (_projectileCollision == null) Debug.LogWarning("Projectile Collision Behaviour isnt set!");

        _rb.velocity = Vector2.zero;

        Destroy(gameObject, _projectileStats.ProjectileLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_projectileCollision != null) _projectileCollision.OnCollision(collision, _projectileStats, this.gameObject);
    }
}