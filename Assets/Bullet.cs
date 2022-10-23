using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject

{
    public string type;
    public new string name;
    public float bulletSpeed;
    public int damage;
    public int forceMultiplier = 10;
    public GameObject hitEffect;
    public GameObject bulletObject;
    public AudioClip shootSfx;
    public AudioClip hitSfx;

}
