using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class GradientText : BaseMeshEffect
{
	protected override void Start()
	{
		this.targetGraphic = base.GetComponent<Graphic>();
	}

	public void ModifyVertices(List<UIVertex> vertexList)
	{
		if (!this.IsActive() || vertexList.Count == 0)
		{
			return;
		}
		int count = vertexList.Count;
		UIVertex value = vertexList[0];
		if (this.gradientMode == global::GradientMode.Global)
		{
			if (this.gradientDir == GradientDir.DiagonalLeftToRight || this.gradientDir == GradientDir.DiagonalRightToLeft)
			{
				this.gradientDir = GradientDir.Vertical;
			}
			float num;
			if (this.gradientDir == GradientDir.Vertical)
			{
				num = vertexList.Min((UIVertex v) => v.position.y);
			}
			else
			{
				num = vertexList[vertexList.Count - 1].position.x;
			}
			float num2 = num;
			float num3;
			if (this.gradientDir == GradientDir.Vertical)
			{
				num3 = vertexList.Max((UIVertex v) => v.position.y);
			}
			else
			{
				num3 = vertexList[0].position.x;
			}
			float num4 = num3;
			float num5 = num4 - num2;
			for (int i = 0; i < count; i++)
			{
				value = vertexList[i];
				if (this.overwriteAllColor || !(value.color != this.targetGraphic.color))
				{
					value.color *= Color.Lerp(this.bottomVertex, this.topVertex, (((this.gradientDir != GradientDir.Vertical) ? value.position.x : value.position.y) - num2) / num5);
					vertexList[i] = value;
				}
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				value = vertexList[j];
				if (this.overwriteAllColor || this.CompareCarefully(value.color, this.targetGraphic.color))
				{
					switch (this.gradientDir)
					{
					case GradientDir.Vertical:
						value.color *= ((j % 4 != 0 && (j - 1) % 4 != 0) ? this.bottomVertex : this.topVertex);
						break;
					case GradientDir.Horizontal:
						value.color *= ((j % 4 != 0 && (j - 3) % 4 != 0) ? this.bottomVertex : this.topVertex);
						break;
					case GradientDir.DiagonalLeftToRight:
						value.color *= ((j % 4 != 0) ? (((j - 2) % 4 != 0) ? Color.Lerp(this.bottomVertex, this.topVertex, 0.5f) : this.bottomVertex) : this.topVertex);
						break;
					case GradientDir.DiagonalRightToLeft:
						value.color *= (((j - 1) % 4 != 0) ? (((j - 3) % 4 != 0) ? Color.Lerp(this.bottomVertex, this.topVertex, 0.5f) : this.bottomVertex) : this.topVertex);
						break;
					}
					vertexList[j] = value;
				}
			}
		}
	}

	private bool CompareCarefully(Color col1, Color col2)
	{
		return Mathf.Abs(col1.r - col2.r) < 0.003f && Mathf.Abs(col1.g - col2.g) < 0.003f && Mathf.Abs(col1.b - col2.b) < 0.003f && Mathf.Abs(col1.a - col2.a) < 0.003f;
	}

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive())
		{
			return;
		}
		List<UIVertex> list = new List<UIVertex>();
		vh.GetUIVertexStream(list);
		this.ModifyVertices(list);
		vh.Clear();
		vh.AddUIVertexTriangleStream(list);
	}

	public global::GradientMode gradientMode;

	public GradientDir gradientDir;

	public bool overwriteAllColor;

	public Color topVertex = Color.white;

	public Color bottomVertex = Color.black;

	private Graphic targetGraphic;
}
