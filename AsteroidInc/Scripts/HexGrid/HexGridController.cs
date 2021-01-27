using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridController : MonoBehaviour
{
    PlayManager pManager;

    [Header("Grid Attributes")]
    public int GridX;
    public int gridZ;

    public float offSetX;
    public float offSetZ;

    float currentX = 0;
    float currentZ = 0;

    public float startX;
    public float startZ;

     public Hexagon[,] GridObjects;

    [Header("Hexagon Attributes")]
    public GameObject HexagonGameobject;
    public Material[] GridMaterials;

    [Header("Mineable Objects")]
    public List<GameObject> Suns;
    public List<GameObject> Planets;
    public List<GameObject> Asteroids;

    [Header("Player Attributes")]
    public int p1HQStartX;
    public int p1HQStartZ;

    public int p2HQStartX;
    public int p2HQStartZ;

    [Header("Enviroment Attributes:")]
    public int numberOfPlanets;
    public int numberOfAsteroids;

    //InitiatedSurrounding
    public int surroundingFoundCount = 0;
    bool hasInitiatedSurrounding = false;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (surroundingFoundCount == 140)
        {
            if (hasInitiatedSurrounding == false)
            {
                Debug.Log("Generated planets");
                hasInitiatedSurrounding = true;
                getSurrounding();
                generateEnviroment();
                resetBoard();
            }
        }

        AttackHighlight();
    }

    public void startProtocal()
    {
        InitializeGridObjects();
        GenerateGrid();

        defaultSpawnPlayers();

        pManager.fleetController.SpawnStartingFleet();

        //pManager.Start2v2StateMachine();
    }

    //GRID CREATION
    void InitializeGridObjects()
    {
        GridObjects = new Hexagon[GridX, gridZ];
    }

    void GenerateGrid()
    {
        currentZ = startZ;

        for (int z = 0; z < gridZ; z++)
        {
            if (z % 2 == 0)
            {
                currentX = startX;
            }
            else currentX = startX + 21;

            for (int x = 0; x < GridX; x++)
            {
                //if (z == gridZ / 2 && x == GridX / 2)
                //{
                //    Instantiate(Suns[0], new Vector3(currentX, 0, currentZ), Quaternion.identity);
                //    GridObjects[x, z] = null;
                //}
                //else
                //{
                    GameObject tempHexagonGameobject = Instantiate(HexagonGameobject, new Vector3(currentX, 0, currentZ), Quaternion.Euler(-90, 0, 0));
                    string HexagonName = "H_" + x + "_" + z;
                    HexCollision hCollision = tempHexagonGameobject.GetComponent<HexCollision>();

                    Hexagon tempHexClass = new Hexagon(HexagonName, tempHexagonGameobject, currentX, currentZ);
                    GridObjects[x, z] = tempHexClass;
                //}
                currentX += offSetX;
            }

            currentZ += offSetZ;
        }
    }

    void getSurrounding()
    {
        foreach(Hexagon h in GridObjects)
        {
            if (h != null)
            {
                HexCollision tempHCollision = h.HexagonObject.GetComponent<HexCollision>();
                List<Hexagon> tempSurrounding = new List<Hexagon>();
                foreach (GameObject g in tempHCollision.surroundingHexagons)
                {
                    tempSurrounding.Add(FindHexagonBasedOnGameobject(g));
                }
                h.surroundingHex = tempSurrounding;
            }
        }
    }

    void generateEnviroment()
    {
        //PHASE 1 OF PLANETS
        for (int p1 = 0; p1 < numberOfPlanets / 2; p1++)
        {
            int ranX = Random.Range(0, GridX - 1);
            int ranZ = Random.Range(0, gridZ/2 - 1);

            while ((GridObjects[ranX, ranZ] == null || 
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ + 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ + 1] ||
                inSurroundingHex(GridObjects[p1HQStartX, p1HQStartZ - 1], ranX, ranZ) ||
                inSurroundingHex(GridObjects[p2HQStartX, p2HQStartZ + 1], ranX, ranZ) ||
                isMineableInSurroundingHex(GridObjects[ranX, ranZ]) ||
                GridObjects[ranX, ranZ].MineableInHex != null))
            {
                ranX = Random.Range(0, GridX - 1);
                ranZ = Random.Range(0, gridZ/2 - 1);
            }

            GameObject tempPlanet = Instantiate(Planets[p1], new Vector3(GridObjects[ranX, ranZ].centerX, 0, GridObjects[ranX, ranZ].centerZ), Quaternion.identity);
            PlanetController pController = tempPlanet.GetComponent<PlanetController>();
            pController.baseClass = new Planet(pController.planetName, pController.resourcesAvailable, pController.resourcesperTurn, pController.inhabitanceDefence, pController.inhabitanceAttack);
            GridObjects[ranX, ranZ].MineableInHex = pController.baseClass;
        }
        //PHASE 2 OF PLANETS
        for (int p2 = 0; p2 < numberOfPlanets / 2; p2++)
        {
            int ranX = Random.Range(0, GridX - 1);
            int ranZ = Random.Range((gridZ / 2) - 1, gridZ - 1);

            while ((GridObjects[ranX, ranZ] == null || 
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ + 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ + 1] ||
                inSurroundingHex(GridObjects[p1HQStartX, p1HQStartZ - 1], ranX, ranZ) ||
                inSurroundingHex(GridObjects[p2HQStartX, p2HQStartZ + 1], ranX, ranZ) ||
                isMineableInSurroundingHex(GridObjects[ranX, ranZ]) ||
                GridObjects[ranX, ranZ].MineableInHex != null))
            {
                ranX = Random.Range(0, GridX - 1);
                ranZ = Random.Range(gridZ / 2  - 1, gridZ - 1);
            }

            GameObject tempPlanet = Instantiate(Planets[p2 + numberOfPlanets/2], new Vector3(GridObjects[ranX, ranZ].centerX, 0, GridObjects[ranX, ranZ].centerZ), Quaternion.identity);
            PlanetController pController = tempPlanet.GetComponent<PlanetController>();
            pController.baseClass = new Planet(pController.planetName, pController.resourcesAvailable, pController.resourcesperTurn, pController.inhabitanceDefence, pController.inhabitanceAttack);
            GridObjects[ranX, ranZ].MineableInHex = pController.baseClass;
        }
        //PHASE 1 OF ASTEROIDS
        for (int a1 = 0; a1 < numberOfAsteroids / 2; a1++)
        {
            int ranX = Random.Range(0, GridX - 1);
            int ranZ = Random.Range(0, gridZ/2 - 1);

            while ((GridObjects[ranX, ranZ] == null || 
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ + 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ + 1] ||
                inSurroundingHex(GridObjects[p1HQStartX, p1HQStartZ - 1], ranX, ranZ) ||
                inSurroundingHex(GridObjects[p2HQStartX, p2HQStartZ + 1], ranX, ranZ) ||
                isMineableInSurroundingHex(GridObjects[ranX, ranZ]) ||
                GridObjects[ranX, ranZ].MineableInHex != null))
            {
                ranX = Random.Range(0, GridX - 1);
                ranZ = Random.Range(0, gridZ/2 - 1);
            }

            GameObject tempAsteroid = Instantiate(Asteroids[0], new Vector3(GridObjects[ranX, ranZ].centerX, 0, GridObjects[ranX, ranZ].centerZ), Quaternion.identity);
            AsteroidController aController = tempAsteroid.GetComponent<AsteroidController>();
            aController.baseClass = new Asteroid(aController.AsteroidNamingConvetion(), aController.resourcesAvailable, aController.resourcesperTurn);
            GridObjects[ranX, ranZ].MineableInHex = aController.baseClass;
        }
        //PHASE 2 OF ASTEROIDS
        for (int a2 = 0; a2 < numberOfAsteroids / 2; a2++)
        {
            int ranX = Random.Range(0, GridX - 1);
            int ranZ = Random.Range(gridZ/2 - 1, gridZ - 1);

            while ((GridObjects[ranX, ranZ] == null || 
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ + 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p1HQStartX, p1HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ - 1] ||
                GridObjects[ranX, ranZ] == GridObjects[p2HQStartX, p2HQStartZ + 1] ||
                inSurroundingHex(GridObjects[p1HQStartX, p1HQStartZ - 1], ranX, ranZ) ||
                inSurroundingHex(GridObjects[p2HQStartX, p2HQStartZ + 1], ranX, ranZ) ||
                isMineableInSurroundingHex(GridObjects[ranX, ranZ]) ||
                GridObjects[ranX, ranZ].MineableInHex != null))
            {
                ranX = Random.Range(0, GridX - 1);
                ranZ = Random.Range(gridZ/2 - 1, gridZ - 1);
            }

            GameObject tempAsteroid = Instantiate(Asteroids[0], new Vector3(GridObjects[ranX, ranZ].centerX, 0, GridObjects[ranX, ranZ].centerZ), Quaternion.identity);
            AsteroidController aController = tempAsteroid.GetComponent<AsteroidController>();
            aController.baseClass = new Asteroid(aController.AsteroidNamingConvetion(), aController.resourcesAvailable, aController.resourcesperTurn);
            GridObjects[ranX, ranZ].MineableInHex = aController.baseClass;
        }
    }

    //GRID MOVEMENT
    public List<Hexagon> getShortestPath(GameObject nStart, GameObject nDestination)
    {
        List<Hexagon> HexagonsToTravelTo = new List<Hexagon>();

        Hexagon currentHexagon = FindHexagonBasedOnGameobject(nStart);
        Hexagon destinationHexagon = FindHexagonBasedOnGameobject(nDestination);

        HexagonsToTravelTo.Add(currentHexagon);

        for (int i = 0; i < 20; i++)
        {
            if (Vector3.Distance(currentHexagon.HexagonObject.transform.position, destinationHexagon.HexagonObject.transform.position) != 0)
            {
                foreach (Hexagon h in currentHexagon.surroundingHex)
                {
                    if (Vector3.Distance(h.HexagonObject.transform.position, destinationHexagon.HexagonObject.transform.position) < Vector3.Distance(currentHexagon.HexagonObject.transform.position, destinationHexagon.HexagonObject.transform.position))
                    {
                        currentHexagon = h;
                    }
                }
                HexagonsToTravelTo.Add(currentHexagon);
            }
            else break;
        }

        return HexagonsToTravelTo;
    }

    public Hexagon FindHexagonBasedOnGameobject(GameObject hexGameobject)
    {
        Hexagon tempHex = null;

        foreach (Hexagon h in GridObjects)
        {
            if (h != null)
            {
                if (h.HexagonObject == hexGameobject)
                    tempHex = h;
            }
        }

        return tempHex;
    }

    public void changeSelectedMaterial(List<Hexagon> path , int numberOfMoves)
    {
        int numberMoved = 1;

        for (int i = 0; i < path.Count; i++)
        {
            MeshRenderer m = path[i].HexagonObject.GetComponent<MeshRenderer>();

            if (i == 0)
            {
                m.material = GridMaterials[1];
            }
            else
            {
                if (numberMoved <= numberOfMoves)
                {
                    m.material = GridMaterials[2];
                }
                else
                {
                    m.material = GridMaterials[3];
                }

                numberMoved++;
            }
        }
    }

    public void resetBoard()
    {
        foreach (Hexagon h in GridObjects)
        {
            if (h != null)
            {
                if (h.MineableInHex != null)
                {
                    if (h.MineableInHex.Owned == null)
                    {
                        MeshRenderer m = h.HexagonObject.GetComponent<MeshRenderer>();
                        m.material = GridMaterials[4];
                    }
                    else if (h.MineableInHex.Owned == pManager.p1Base)
                    {
                        MeshRenderer m = h.HexagonObject.GetComponent<MeshRenderer>();
                        m.material = pManager.fleetController.p1Colour;
                    }
                    else if (h.MineableInHex.Owned == pManager.p2Base)
                    {
                        MeshRenderer m = h.HexagonObject.GetComponent<MeshRenderer>();
                        m.material = pManager.fleetController.p2Colour;
                    }
                }
                else
                {
                    MeshRenderer m = h.HexagonObject.GetComponent<MeshRenderer>();
                    m.material = GridMaterials[0];
                }
            }
        }
    }

    void HideOtherHex(List<Hexagon> path)
    {
        foreach (Hexagon h in GridObjects)
        {
            bool isInPath = false;

            for (int i = 0; i < path.Count; i++)
            {
                if (h == path[i])
                {
                    isInPath = true;
                    break;
                }
            }

            if (isInPath == false)
            {
                h.HexagonObject.SetActive(false);
            }
        }
    }

    public int[] getHexIndex(Hexagon Hex)
    {
        int[] hexIndex = new int[2];

        for (int z = 0; z < gridZ; z++)
        {
            for (int x = 0; x < GridX; x++)
            {
                if (GridObjects[x, z] == Hex)
                {
                    hexIndex[0] = x;
                    hexIndex[1] = z;
                    break;
                }
            }
        }

        return hexIndex;
    }

    //UTILITY FUNCTIONS
    bool isMineableInSurroundingHex(Hexagon currentHex)
    {
        bool isMinable = false;

        if (currentHex != null)
        {
            foreach (Hexagon hex in currentHex.surroundingHex)
            {
                if (hex != null)
                {
                    if (hex.MineableInHex != null)
                    {
                        isMinable = true;
                    }
                }
            }
        }

        return isMinable;
    }

    public void AttackHighlight()
    {
        if (pManager.fleetController.canAttack == true)
        {
            resetBoard();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Hexagon") //IF MOUSE OVER HEXAGON
                {
                    bool inSurrounding = inSurroundingHex(hit.transform.gameObject);

                    if (inSurrounding == true)
                    {
                        MeshRenderer hRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
                        
                        if (getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn) != null) //SHIP FOUND IN SURROUNDING
                        {
                            if (pManager.fleetController.shipOwned(getShipObjectInHexagon(hit.transform.gameObject, pManager.PlayersTurn), pManager.PlayersTurn) != null) //ALLY SHIP
                            {
                                if (pManager.PlayersTurn == 1)
                                {
                                    hRenderer.material = pManager.fleetController.p1Colour;
                                }
                                else if (pManager.PlayersTurn == 2)
                                {
                                    hRenderer.material = pManager.fleetController.p2Colour;
                                }
                            }
                        }
                        else
                        {
                            if (pManager.PlayersTurn == 1)
                            {
                                if (getShipObjectInHexagon(hit.transform.gameObject, 2) != null)
                                {
                                    hRenderer.material = pManager.fleetController.p2Colour;
                                }
                                else
                                {
                                    if (hit.transform.gameObject == GridObjects[pManager.p1Base.HQ.hexX, pManager.p1Base.HQ.hexZ].HexagonObject)
                                    {
                                        hRenderer = GridObjects[pManager.p1Base.HQ.hexX, pManager.p1Base.HQ.hexZ].HexagonObject.GetComponent<MeshRenderer>();
                                        hRenderer.material = pManager.fleetController.p1Colour;
                                    }
                                    else if (hit.transform.gameObject == GridObjects[pManager.p2Base.HQ.hexX, pManager.p2Base.HQ.hexZ].HexagonObject)
                                    {
                                        hRenderer = GridObjects[pManager.p2Base.HQ.hexX, pManager.p2Base.HQ.hexZ].HexagonObject.GetComponent<MeshRenderer>();
                                        hRenderer.material = pManager.fleetController.p2Colour;
                                    }
                                    else hRenderer.material = GridMaterials[1];
                                }
                            }
                            else if (pManager.PlayersTurn == 2)
                            {
                                if (getShipObjectInHexagon(hit.transform.gameObject, 1) != null)
                                {
                                    hRenderer.material = pManager.fleetController.p1Colour;
                                }
                                else
                                {
                                    if (hit.transform.gameObject == GridObjects[pManager.p2Base.HQ.hexX, pManager.p2Base.HQ.hexZ].HexagonObject)
                                    {
                                        hRenderer = GridObjects[pManager.p2Base.HQ.hexX, pManager.p2Base.HQ.hexZ].HexagonObject.GetComponent<MeshRenderer>();
                                        hRenderer.material = pManager.fleetController.p2Colour;
                                    }
                                    else if (hit.transform.gameObject == GridObjects[pManager.p1Base.HQ.hexX, pManager.p1Base.HQ.hexZ].HexagonObject)
                                    {
                                        hRenderer = GridObjects[pManager.p1Base.HQ.hexX, pManager.p1Base.HQ.hexZ].HexagonObject.GetComponent<MeshRenderer>();
                                        hRenderer.material = pManager.fleetController.p1Colour;
                                    }
                                    else hRenderer.material = GridMaterials[1];
                                }
                            }
                        }
                    }
                }
                else if (hit.transform.gameObject.tag == "Ship")
                {
                    if (pManager.fleetController.shipOwned(hit.transform.gameObject, pManager.PlayersTurn) != null) //ALLY SHIP
                    {
                        Hexagon tempHex = GridObjects[pManager.fleetController.shipOwned(hit.transform.gameObject, pManager.PlayersTurn).hexX, pManager.fleetController.shipOwned(hit.transform.gameObject, pManager.PlayersTurn).hexZ];
                        MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                        if (pManager.PlayersTurn == 1)
                        {
                            hRenderer.material = pManager.fleetController.p1Colour;
                        }
                        else if (pManager.PlayersTurn == 2)
                        {
                            hRenderer.material = pManager.fleetController.p2Colour;
                        }
                    }
                    else //ENEMY SHIP FOUND
                    {
                        if (pManager.PlayersTurn == 1)
                        {
                            Ship_Base tempShip = null;
                            
                            foreach (Ship_Base sBase in pManager.p2Base.Fleet)
                            {
                                if (hit.transform.gameObject == sBase.shipObject)
                                {
                                    tempShip = pManager.fleetController.shipOwned(hit.transform.gameObject, 2);
                                }
                            }
                            Hexagon tempHex = GridObjects[tempShip.hexX, tempShip.hexZ];
                            MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                            hRenderer.material = pManager.fleetController.p2Colour;
                        }
                        else if (pManager.PlayersTurn == 2)
                        {
                            Ship_Base tempShip = null;

                            foreach (Ship_Base sBase in pManager.p1Base.Fleet)
                            {
                                if (hit.transform.gameObject == sBase.shipObject)
                                {
                                    tempShip = pManager.fleetController.shipOwned(hit.transform.gameObject, 1);
                                }
                            }

                            Hexagon tempHex = GridObjects[tempShip.hexX, tempShip.hexZ];
                            MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                            hRenderer.material = pManager.fleetController.p1Colour;
                        }
                    }
                }
                else if (hit.transform.gameObject.tag == "HQ")
                {
                    if (pManager.PlayersTurn == 1)
                    {
                        if (hit.transform.gameObject == pManager.p1Base.HQ.shipObject)
                        {
                            Hexagon tempHex = GridObjects[pManager.p1Base.HQ.hexX, pManager.p1Base.HQ.hexZ];
                            MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                            hRenderer.material = pManager.fleetController.p1Colour;
                        }
                        else
                        {
                            Hexagon tempHex = GridObjects[pManager.p2Base.HQ.hexX, pManager.p2Base.HQ.hexZ];
                            MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                            hRenderer.material = pManager.fleetController.p2Colour;
                        }
                    }
                    else if (pManager.PlayersTurn == 2)
                    {
                        if (hit.transform.gameObject == pManager.p2Base.HQ.shipObject)
                        {
                            Hexagon tempHex = GridObjects[pManager.p2Base.HQ.hexX, pManager.p2Base.HQ.hexZ];
                            MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                            hRenderer.material = pManager.fleetController.p2Colour;
                        }
                        else
                        {
                            Hexagon tempHex = GridObjects[pManager.p1Base.HQ.hexX, pManager.p1Base.HQ.hexZ];
                            MeshRenderer hRenderer = tempHex.HexagonObject.GetComponent<MeshRenderer>();

                            hRenderer.material = pManager.fleetController.p1Colour;
                        }
                    }
                }
            }
        }
    }

    public GameObject getShipObjectInHexagon(GameObject HexagonObject, int playerTurn)
    {
        int tempHexX = 0;
        int tempHexZ = 0;

        for (int z = 0; z < gridZ; z++)
        {
            for (int x = 0; x < GridX; x++)
            {
                if (GridObjects[x, z] != null)
                {
                    if (HexagonObject == GridObjects[x, z].HexagonObject)
                    {
                        tempHexX = x;
                        tempHexZ = z;
                        break;
                    }
                }
            }
        }

        if (playerTurn == 1)
        {
            foreach (Ship_Base sBase in pManager.p1Base.Fleet)
            {
                if (sBase.hexX == tempHexX && sBase.hexZ == tempHexZ)
                {
                    return sBase.shipObject;
                }
            }
        }
        else if (playerTurn == 2)
        {
            foreach (Ship_Base sBase in pManager.p2Base.Fleet)
            {
                if (sBase.hexX == tempHexX && sBase.hexZ == tempHexZ)
                {
                    return sBase.shipObject;
                }
            }
        }

        return null;
    }

    public Ship_Base getAnyShipInHexagon(Hexagon hexagon)
    {
        Ship_Base tempShip = null;

        int tempHexX = 0;
        int tempHexZ = 0;

        for (int z = 0; z < gridZ; z++)
        {
            for (int x = 0; x < GridX; x++)
            {
                if (GridObjects[x, z] != null)
                {
                    if (hexagon == GridObjects[x, z])
                    {
                        tempHexX = x;
                        tempHexZ = z;
                        break;
                    }
                }
            }
        }

        foreach (Ship_Base ship in pManager.p1Base.Fleet)
        {
            if (ship.hexX == tempHexX && ship.hexZ == tempHexZ)
            {
                tempShip = ship;
            }
        }

        foreach (Ship_Base ship in pManager.p2Base.Fleet)
        {
            if (ship.hexX == tempHexX && ship.hexZ == tempHexZ)
            {
                tempShip = ship;
            }
        }

        return tempShip;
    }

    public bool inSurroundingHex(GameObject hexObject)
    {
        foreach (Hexagon h in GridObjects[pManager.fleetController.selectedUnit.hexX, pManager.fleetController.selectedUnit.hexZ].surroundingHex)
        {
            if (h != null)
            {
                if (h.HexagonObject == hexObject)
                    return true;
            }
        }

        return false;
    }

    public int GetNumberOfUnownedPlanets()
    {
        int unownedPlanetsCount = 0;

        foreach (Hexagon hex in GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (hex.MineableInHex.GetType() == typeof(Planet))
                    {
                        unownedPlanetsCount++;
                    }
                }
            }
        }

        return unownedPlanetsCount;
    }

    public int GetNumberOfEnemyOwnedMinables(PlayerBase opponent)
    {
        int NumberOfEnemyOwnedMinables = 0;

        foreach (Hexagon hex in GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (hex.MineableInHex.Owned == opponent)
                    {
                        NumberOfEnemyOwnedMinables++;
                    }
                }
            }
        }

        return NumberOfEnemyOwnedMinables;
    }

    public int GetNumberOfEnemyOwnedPlanets(PlayerBase opponent)
    {
        int enemyOwnedPlanetsCount = 0;

        foreach (Hexagon hex in GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (hex.MineableInHex.GetType() == typeof(Planet))
                    {
                        if (hex.MineableInHex.Owned == opponent)
                        {
                            enemyOwnedPlanetsCount++;
                        }
                    }
                }
            }
        }

        return enemyOwnedPlanetsCount;
    }

    public bool isClosestToHex(Hexagon hexWithMineable, Ship_Base selected, PlayerBase pPlayer)
    {
        bool isClosest = true;

        foreach (Ship_Base ship in pPlayer.Fleet)
        {
            if (ship != selected)
            {
                if (ship.GetType() == typeof(MiningShip))
                {
                    if (Mathf.Abs(pManager.hexGridController.GridObjects[selected.hexX, selected.hexZ].centerX - hexWithMineable.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[selected.hexX, selected.hexZ].centerZ - hexWithMineable.centerZ)
                        > Mathf.Abs(pManager.hexGridController.GridObjects[ship.hexX, ship.hexZ].centerX - hexWithMineable.centerX) + Mathf.Abs(pManager.hexGridController.GridObjects[ship.hexX, ship.hexZ].centerZ - hexWithMineable.centerZ))
                    {
                        isClosest = false;

                    }
                }
            }
        }

        return isClosest;
    }

    public Hexagon closestMineableInRange(PlayerBase pPlayer, Ship_Base selected, float range)
    {
        float distance = 0;
        Hexagon closestMineableHex = null;

        foreach (Hexagon hex in GridObjects)
        {
            if (hex != null)
            {
                if (hex.MineableInHex != null)
                {
                    if (hex.MineableInHex.Owned != pPlayer)
                    {
                        if (Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerX - hex.centerX) + Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerZ - hex.centerZ) <= range)
                        {
                            if (closestMineableHex == null)
                            {
                                closestMineableHex = hex;
                                distance = Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerX - hex.centerX) + Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerZ - hex.centerZ);
                            }
                            else if (Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerX - hex.centerX) + Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerZ - hex.centerZ) < distance)
                            {
                                closestMineableHex = hex;
                                distance = Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerX - hex.centerX) + Mathf.Abs(GridObjects[selected.hexX, selected.hexZ].centerZ - hex.centerZ);
                            }
                        }
                    }
                }
            }
        }

        return closestMineableHex;
    }

    bool inSurroundingHex(Hexagon hex, int x, int z)
    {
        bool inSurrounding = false;
        foreach (Hexagon h in hex.surroundingHex)
        {
            if (h == GridObjects[x, z])
            {
                inSurrounding = true;
            }
        }

        return inSurrounding;
    }

    //PLAYER ASSIGNMENT
    void defaultSpawnPlayers()
    {
        p1HQStartX = GridX - 1;
        p1HQStartZ = 1;

        p2HQStartX = 0;
        p2HQStartZ = gridZ - 2;
    }
}
