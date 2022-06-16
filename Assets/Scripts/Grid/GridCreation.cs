using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using FishNet.Object;
using FishNet;
using FishNet.Component.Spawning;

public class GridCreation : NetworkBehaviour
{
    private GridXZ<GridObject> grid;
    [SerializeField] public TeamSpawner ts;
    [SerializeField] public PlayerTeam playerTeam;
    GameObject ch1;
    GameObject ch2;
    GameObject ch3;
    GameObject ch4;

    GridObject selectedPlayerGridObject;
    private void Awake()
    {
        int gridWidth = 50;
        int gridHeight = 50;
        float cellSize = 2f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));

        SelectCharacters();

        //gc.selectedUnit = ch1;
    }
    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private GameObject player;


        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetPlayer(GameObject player)
        {
            this.player = player;
        }

        public GameObject GetPlayer()
        {
            return this.player;
        }

        public void ClearPlayer()
        {
            player = null;
        }

        public bool CanMove()
        {
            return player == null;
        }
    }

    public GridXZ<GridObject> getGrid()
    {
        return grid;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
            GridObject gridObject = grid.GetGridObject(x, z);

            if(gridObject.GetPlayer() != null)
            {
                gc.selectedUnit = gridObject.GetPlayer();
                selectedPlayerGridObject = gridObject;
            }

            if (gridObject.CanMove())
            {
                MoveUnit(gc.selectedUnit, grid.GetWorldPosition(x, z), selectedPlayerGridObject);
                gridObject.SetPlayer(gc.selectedUnit);
                selectedPlayerGridObject = gridObject;
            }

            else
            {
                Debug.Log("Player already here!");
            }*/
        }
    }

    //Spawns the initial squad of characters
    private void SelectCharacters()
    {
        /*GridObject gridObject = grid.GetGridObject(25,1);

        //ch1 = Instantiate(ts.GetPlayer(0));
        //InstanceFinder.ServerManager.Spawn(ch1, base.Owner);
        //gc.Character1 = ch1;
        //ch1.transform.position = grid.GetWorldPosition(25, 1);
        //gridObject.SetPlayer(gc.Character1);

        gridObject = grid.GetGridObject(25, 0);
        ch2 = Instantiate(gc.Character2);
        InstanceFinder.ServerManager.Spawn(ch2, base.Owner);
        //gc.Character2 = ch2;
        //ch2.transform.position = grid.GetWorldPosition(25, 0);
        //gridObject.SetPlayer(gc.Character2);

        gridObject = grid.GetGridObject(24, 0);
        ch3 = Instantiate(gc.Character3);
        InstanceFinder.ServerManager.Spawn(ch3, base.Owner);
        //gc.Character3 = ch3;
        //ch3.transform.position = grid.GetWorldPosition(24, 0);
        //gridObject.SetPlayer(gc.Character3);

        gridObject = grid.GetGridObject(26, 0);
        ch4 = Instantiate(gc.Character4);
        InstanceFinder.ServerManager.Spawn(ch4, base.Owner);
        //gc.Character4 = ch4;
        //ch4.transform.position = grid.GetWorldPosition(26, 0);
        //gridObject.SetPlayer(gc.Character4);*/
    }

    //Moves a unit to a new grid position
    public void MoveUnit(GameObject unit, Vector3 position, GridObject selectedPlayerGridObject)
    {

        if (unit.GetComponent<Unit>().movement > 0)
        {

            selectedPlayerGridObject.ClearPlayer();

            unit.transform.position = position;
            unit.GetComponent<Unit>().ReduceMovement();
        }

        else
        {
            Debug.Log("Out of movement");
            return;
        }
    }
}
