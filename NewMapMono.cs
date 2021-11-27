using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NewMap
{
    class NewMapMono : MonoBehaviour
    {
        Scene oldMapScene;
        Scene newMapScene;

        public const string MAP_SCENE_PATH = "Assets/Scenes/Windridge City Demo Scene.unity";

        public readonly Vector3[] TABLES_POSITIONS =
        {
            new Vector3(404.1f, 353.2f, 568.4f),
            new Vector3(399.1f, 353.2f, 576.1f),
            new Vector3(401.4f, 353.2f, 575.1f),
            new Vector3(403.7f, 353.2f, 574.1f),
            new Vector3(36.8f, 343.2f, -5.9f),
            new Vector3(175.4f, 284.3f, 4.8f)
        };

        public readonly Vector3[] SHELFS_POSITIONS =
        {
            new Vector3(406.5f, 353.6f, 573.0f),
            new Vector3(408.0f, 353.6f, 572.3f),
            new Vector3(340.3f, 368.2f, 513.3f),
            new Vector3(304.6f, 331.7f, 438.5f),
            new Vector3(197.0f, 312.3f, 324.2f),
            new Vector3(86.5f, 346.8f, -72.2f),
            new Vector3(178.9f, 499.7f, -272.4f), // EASTER EGG.
            new Vector3(301.7f, 319.7f, -78.0f),
            new Vector3(290.5f, 277.0f, 46.3f),
            new Vector3(607.0f, 321.5f, 54.6f),
            new Vector3(238.7f, 344.8f, -130.7f)
        };

        public readonly Vector3[] TROLLEYS_POSITIONS =
        {
            new Vector3(408.4f, 353.8f, 567.0f),
            new Vector3(408.9f, 353.8f, 567.9f)
        };

        public readonly Vector3[] SPAWN_POINTS_BARRELS =
        {
            new Vector3(399.9f, 353.2f, 565.0f),
            new Vector3(400.3f, 353.2f, 565.7f)
        };

        public readonly Vector3[] SPAWN_POINTS_TOW =
        {
            new Vector3(390.1f, 353.8f, 573.3f),
            new Vector3(389.3f, 353.8f, 571.6f),
            new Vector3(388.1f, 353.8f, 569.1f),
            new Vector3(387.0f, 353.8f, 566.6f),
            new Vector3(386.0f, 353.8f, 564.5f),
            new Vector3(385.1f, 353.8f, 562.6f)
        };

        public readonly Vector3[] TRASH_BINS_POSITION = // The first one is the big one, the other 3 the smalls.
        {
            new Vector3(395.8f, 352.9f, 577.0f), // Big - Office
            //new Vector3(408f, 353.1f, 565.3f), // Small - office - Removed in moving update.
            new Vector3(-421.3f, 288.2f, 117.5f), // Gas station
            new Vector3(-331.2f, 304.2f, 344.1f), // Junkyard
            new Vector3(373.0f, 341.9f, -233.1f) // Shop
        };

        public readonly Vector3[] BOXES_POSITION =
        {
            new Vector3(397.6f, 353.8f, 576.9f),
            new Vector3(433.7f, 359.5f, 529.8f),
            new Vector3(257.7f, 305.2f, -38.5f),
            new Vector3(-336.7f, 305.7f, 303.9f),
            new Vector3(496.6f, 357.9f, 565.3f),
        };

        public readonly Vector3 TIRE_MOUNTER = new Vector3(406.1f, 353.5f, 567.9f);
        public readonly Vector3 ENGINE_HOIST_POSITION = new Vector3(409.5f, 353.8f, 570.4f);
        public readonly Vector3 DEFAULT_PLAYER_SPAWN = new Vector3(398.9f, 353.8f, 558.6f);
        public readonly Vector3 ITEM_SPAWN_POSITION = new Vector3(403.6f, 354.6f, 568.7f);
        
        public void OnLoad()
        {
            oldMapScene = SceneManager.GetActiveScene();

            // A way for adding our listener into the ESC menu
            NewMap.PlayerTools.EscMenu.SetActive(true);
            GameObject.Find("disabletrees").GetComponent<Button>().onClick.AddListener(NewMap.HandleVegetation);
            GameObject.Destroy(GameObject.Find("disablewater"));
            NewMap.PlayerTools.EscMenu.SetActive(false);

            StartCoroutine(AsyncSceneLoad());
        }

        IEnumerator AsyncSceneLoad()
        {
            Utils.Delete("Road Network");
            Utils.Delete("SurfaceMap");
            Utils.Delete("Gaia Terrains");
            Utils.Delete("Gaia Water");
            Utils.Delete("VegetationStudioPro");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(MAP_SCENE_PATH, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            newMapScene = SceneManager.GetSceneByName("Windridge City Demo Scene");
            SceneManager.MergeScenes(oldMapScene, newMapScene);

            // Collectible spawners
            GameObject collectibleSpawners = GameObject.Find("Randomitemspavni");
            for (int i = 0; i < collectibleSpawners.transform.childCount; i++)
            {
                Transform child = collectibleSpawners.transform.GetChild(i);
                child.position = new Vector3(child.position.x, child.position.y + 300f, child.position.z);
            }

            Utils.ModifyCollectibleSpawner(10, new Vector3(-479.8f, 300.2f, 186.7f));
            Utils.ModifyCollectibleSpawner(16, new Vector3(-460.0f, 313.2f, 294.8f));
            Utils.ModifyCollectibleSpawner(29, new Vector3(-30.5f, 333.8f, 430.0f));
            Utils.ModifyCollectibleSpawner(26, new Vector3(260.3f, 344.1f, -294.8f));
            Utils.ModifyCollectibleSpawner(25, new Vector3(261.7f, 344.1f, -283.6f));
            Utils.ModifyCollectibleSpawner(28, new Vector3(263.7f, 343.1f, -312.3f));
            Utils.ModifyCollectibleSpawner(27, new Vector3(-21.4f, 349.6f, -228.6f));
            Utils.ModifyCollectibleSpawner(23, new Vector3(-31.9f, 349.0f, -219.3f));
            Utils.ModifyCollectibleSpawner(24, new Vector3(42.1f, 348.0f, -83.8f));
            Utils.ModifyCollectibleSpawner(21, new Vector3(54.2f, 357.4f, 12.8f));
            Utils.ModifyCollectibleSpawner(20, new Vector3(32.3f, 357.4f, 6.6f));
            Utils.ModifyCollectibleSpawner(19, new Vector3(215.0f, 342.7f, -402.0f));
            Utils.ModifyCollectibleSpawner(18, new Vector3(262.4f, 343.2f, -399.9f));
            Utils.ModifyCollectibleSpawner(17, new Vector3(-106.2f, 308.7f, 372.4f));
            Utils.ModifyCollectibleSpawner(16, new Vector3(-272.9f, 297.3f, 319.2f));
            Utils.ModifyCollectibleSpawner(15, new Vector3(-258.4f, 283.9f, 205.7f));
            Utils.ModifyCollectibleSpawner(9, new Vector3(-479.4f, 291.4f, 164.9f));

            // Ruined cars
            GameObject ruinedCarSpawner = GameObject.Find("RuineCarSpawner");
            for (int i = 0; i < ruinedCarSpawner.transform.childCount; i++)
            {
                Transform child = ruinedCarSpawner.transform.GetChild(i);
                child.position = new Vector3(child.position.x, child.position.y + 350f, child.position.z);
            }

            Utils.ModifyRuinedSpawner(1, new Vector3(480.5f, 333.3f, 430.6f));
            Utils.ModifyRuinedSpawner(2, new Vector3(523.3f, 314.6f, 247.9f));
            Utils.ModifyRuinedSpawner(3, new Vector3(415.3f, 353.3f, -130.4f));

            // Traffic disable
            GleyTrafficSystem.Manager.SetTrafficDensity(0);

            // Loop in all game objects.
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            int tableCount = 0, shelfCount = 0, barrelCount = 0, trolleyCount = 0, trashCount = 0, boxCount = 0;
            foreach(GameObject go in rootObjects)
            {
                switch (go.name)
                {
                    // BUILDINGS
                    case "PawnShop":
                        if (go.transform.childCount == 3)
                        {
                            go.transform.position = new Vector3(-45.0f, 344.8f, 618.7f);
                            go.transform.eulerAngles = new Vector3(0.0f, 171.6f, 4.9f);
                        }
                        
                        break;

                    case "JunkYard":
                        go.transform.position = new Vector3(-337.0f, 304.1f, 329.1f);
                        go.transform.eulerAngles = new Vector3(0f, 200f, 0f);
                        break;

                    case "GasStation":
                        go.transform.position = new Vector3(-435.5f, 288.01f, 120.1f);
                        go.transform.eulerAngles = new Vector3(0.0f, 304.1f, 0.0f);
                        break;

                    case "Props":
                        for(int i = 0; i < go.transform.childCount; i++)
                        {
                            GameObject goChild = go.transform.GetChild(i).gameObject;
                            switch (goChild.name)
                            {
                                case "garaga2":
                                    GameObject table = GameObject.Find("Table");
                                    GameObject sofa = GameObject.Find("Sofa");
                                    GameObject chair = GameObject.Find("LoungeChair-002-Modern");

                                    chair.transform.SetParent(goChild.transform);  // Set garage as parent
                                    table.transform.SetParent(goChild.transform);
                                    sofa.transform.SetParent(goChild.transform);

                                    goChild.transform.position = new Vector3(393.3f, 353.14f, 573.0f);
                                    goChild.transform.eulerAngles = new Vector3(0f, 204.3f, 0f);

                                    chair.transform.SetParent(null); // Set back to null
                                    table.transform.SetParent(null);
                                    sofa.transform.SetParent(null);
                                    for (int j = 0; j < goChild.transform.childCount; j++)
                                    {
                                        Transform child = goChild.transform.GetChild(j);
                                        if (child.name.StartsWith("Sidewalk"))
                                        {
                                            GameObject.Destroy(child.gameObject);
                                        }

                                        if (child.name.StartsWith("Map"))
                                        {
                                            //child.GetComponent<Renderer>().material.mainTexture = NewMap.TextureBundle().LoadAsset("MAP.png") as Texture;
                                        }
                                    }
                                    break;


                                case "Hangar_v2_7": // Barn
                                    goChild.transform.position = new Vector3(391.0f, 352.7f, 545.0f);
                                    goChild.transform.eulerAngles = new Vector3(0f, 204.3f, 0f);
                                    break;
                            }
                        }
                        break;

                    case "shop":
                        go.transform.position = new Vector3(353.8f, 342.3f, -252.6f);
                        go.transform.rotation = new Quaternion(0f, 0.9f, 0f, 0.5f);

                        GameObject _go = null;
                        for (int i = 0; i < go.transform.childCount; i++)
                        {
                            Transform child = go.transform.GetChild(i);
                            if(child.name == "Shop")
                            {
                                _go = child.gameObject;
                                break;
                            }
                        }

                        for (int i = 0; i < _go.transform.childCount; i++)
                        {
                            Transform child = _go.transform.GetChild(i);
                            if (child.name.StartsWith("Sidewalk"))
                            {
                                GameObject.Destroy(child.gameObject);
                                break;
                            }
                        }
                        break;

                    case "AUtoPlacis":
                        go.transform.position = new Vector3(17.3f, 341.6f, -227.1f);
                        go.transform.rotation = new Quaternion(0f, 0.8f, 0f, -0.6f);

                        for (int i = 0; i < go.transform.childCount; i++)
                        {
                            Transform child = go.transform.GetChild(i);
                            if (child.name.StartsWith("wirefence") || child.name.StartsWith("Sidewalk"))
                                GameObject.Destroy(child.gameObject);
                        }
                        break;
                    // FURNITURE - OTHERS
                    case "Player":
                        go.transform.position = DEFAULT_PLAYER_SPAWN;
                        break;

                    case "MoveTool":
                        go.transform.position = new Vector3(400.2f, 353.8f, 570.1f);
                        break;

                    case "ClientSpawn":
                        go.transform.position = new Vector3(401.0f, 353.8f, 558.9f);
                        break;

                    case "CarLift (1)":
                        go.transform.position = new Vector3(397.8f, 354.0f, 571.7f);
                        go.transform.eulerAngles = new Vector3(359.6f, 25.0f, 0.0f);
                        break;

                    case "Table":
                        if (go.transform.childCount != 2)
                            continue;

                        go.transform.position = TABLES_POSITIONS[tableCount];
                        if (tableCount == 0)
                            go.transform.eulerAngles = new Vector3(0f, 24.3f, 0f);
                        else if (tableCount <= 3)
                            go.transform.eulerAngles = new Vector3(0f, 204.7f, 0f);

                        tableCount++;
                        break;

                    case "Shelf":
                        go.transform.position = SHELFS_POSITIONS[shelfCount];
                        if (tableCount <= 1)
                            go.transform.eulerAngles = new Vector3(0f, 24f, 0f);

                        shelfCount++;
                        break;

                    case "Barrel":
                        go.transform.position = SPAWN_POINTS_BARRELS[barrelCount];
                        barrelCount++;
                        break;

                    case "Trolley":
                        go.transform.position = TROLLEYS_POSITIONS[trolleyCount];
                        trolleyCount++;
                        break;

                    case "TireMounter":
                        go.transform.position = TIRE_MOUNTER;
                        go.transform.eulerAngles = new Vector3(0f, 294f, 0f);
                        break;

                    case "TrashBin":
                        go.transform.position = TRASH_BINS_POSITION[trashCount];
                        if (trashCount == 0)
                            go.transform.eulerAngles = new Vector3(270.0f, 22.9f, 0.0f);
                        trashCount++;
                        break;

                    case "Box":
                        go.transform.position = BOXES_POSITION[boxCount];
                        boxCount++;
                        break;

                    case "EngineStand":
                        go.transform.position = ENGINE_HOIST_POSITION;
                        break;

                    
                    case "CarInfo":
                        CarInformation ci = go.GetComponent<CarInformation>();
                        ci.GarageSpawn.transform.position = SPAWN_POINTS_TOW[0];
                        ci.GarageSpawn1.transform.position = SPAWN_POINTS_TOW[1];
                        ci.GarageSpawn2.transform.position = SPAWN_POINTS_TOW[2];
                        ci.GarageSpawn3.transform.position = SPAWN_POINTS_TOW[3];
                        ci.GarageSpawn4.transform.position = SPAWN_POINTS_TOW[4];
                        ci.GarageSpawn5.transform.position = SPAWN_POINTS_TOW[5];
                        break;
                }
            }
        }

    }
}
