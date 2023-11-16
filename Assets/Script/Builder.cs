using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
	public Renderer MainRenderer;
	public Vector2Int Size = Vector2Int.one;

	public void SetTransperant(bool avalible)
	{
		if(avalible)
		{
			MainRenderer.material.color = Color.green;
		}
		else
		{
			MainRenderer.material.color = Color.red;
		}
	}
	public void SetNormal()
	{
		MainRenderer.material.color = Color.white;
	}
	private void OnDrawGizmosSelected()
	{
		for(int x = 0; x < Size.x; x++)
		{
			for(int y = 0; y < Size.y; y++)
			{
				if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
				else Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
				Gizmos.DrawCube(transform.position + new Vector3(x*10, 0, y*10), new Vector3(10, 0.1f, 10));
			}
		}
	}
}
