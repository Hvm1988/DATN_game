using System;
using UnityEngine;

public abstract class Person : MonoBehaviour
{
	public abstract void hit(int _damage);

	public abstract void die();

	public string _name;

	public int damage;

	public int defend;

	public int HP;

	public int EXP;

	public float speed;

	public int level;

	public bool isDie;

	[HideInInspector]
	public Vector3 vectorMoveRotion = new Vector3(0f, 180f, 0f);

	public ScoreEffect hitEffect;

	public ScoreEffect oneHitEffect;

	[HideInInspector]
	public int damageHit;

	[HideInInspector]
	public int rdOneHit;
}
