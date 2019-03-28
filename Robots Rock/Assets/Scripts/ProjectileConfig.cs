using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Objects/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    public Rigidbody prefab;
    public float baseSpeed;
}
