/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Projectile.cs
* Date   : 03.06.2022
* Author : Alexander Sigmund (AS)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*
******************************************************************************/
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