using System;
using UnityEngine;

public class Grian : Enemies
{
	public override void attack()
	{
		base.attack();
		this._mesh.sortingOrder = 20;
	}

	public override void hit(int _damage)
	{
		base.hit(_damage);
		this._mesh.sortingOrder = -100;
	}

	public override void idle()
	{
		base.idle();
		this._mesh.sortingOrder = -100;
	}

	public override void skill()
	{
		base.attack();
	}

	public MeshRenderer _mesh;
}
