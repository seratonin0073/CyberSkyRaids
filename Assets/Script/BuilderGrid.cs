using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BuilderGrid : MonoBehaviour
{

	// Start is called before the first frame update
	public Vector2Int GridSize = new Vector2Int(20, 20);
	private Builder[,] grid;
	private Builder flyingBuilding;
	[SerializeField] private int xBaze;
	[SerializeField] private int yBaze;
	[SerializeField] private float[,] heights;
	[SerializeField] private Terrain myTerr;
	[SerializeField] private Camera mainCamera;


	private void Awake()
	{
		grid = new Builder[GridSize.x, GridSize.y];
	}

	private void Start()
	{
		myTerr = Terrain.activeTerrain;
	}

	public void StartPlacingBuilding(Builder builderPrefab)
	{
		if (flyingBuilding != null)
		{
			Destroy(flyingBuilding.gameObject);
		}

		flyingBuilding = Instantiate(builderPrefab);
	}

	// Update is called once per frame
	void Update()
	{
		if (flyingBuilding != null)
		{
			var groundPlane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

			if (groundPlane.Raycast(ray, out float posistion))
			{
				Vector3 worldPosistion = ray.GetPoint(posistion);
				int y = Mathf.RoundToInt(worldPosistion.z);
				int x = Mathf.RoundToInt(worldPosistion.x);
				bool avalible = true;

				if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) avalible = false;
				if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) avalible = false;

				if (avalible && IsPlaceTaken(x, y)) avalible = false;

				flyingBuilding.transform.position = new Vector3(x, 0, y);
				flyingBuilding.SetTransperant(avalible);
				if (avalible && Input.GetMouseButtonDown(0))
				{
					PlaceFlyingBuilding(x, y);
				}
			}
		}
	}

	private bool IsPlaceTaken(int placeX, int placeY)
	{
		for (int x = 0; x < flyingBuilding.Size.x * 10; x++)
		{
			for (int y = 0; y < flyingBuilding.Size.y * 10; y++)
			{
				if (grid[placeX + x, placeY + y] != null) return true;
			}
		}
		return false;
	}
	private void PlaceFlyingBuilding(int placeX, int placeY)
	{
		for (int x = 0; x < flyingBuilding.Size.x * 10; x++)
		{
			for (int y = 0; y < flyingBuilding.Size.y * 10; y++)
			{

				grid[placeX + x , placeY + y ] = flyingBuilding;
			}
		}
		flyingBuilding.SetNormal();
		flyingBuilding = null;
	}
}
