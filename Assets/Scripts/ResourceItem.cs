using System;

[Serializable]
public class ResourceItem : NItem
{
	public ResourceItem(ResourceItem another) : base(another)
	{
		this.maxDrop = another.maxDrop;
		this.minDrop = another.minDrop;
	}

	public int minDrop;

	public int maxDrop;
}
