using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewMap
{
    class MapEditor : MonoBehaviour
    {
        string coordsTextarea = "";

        GameObject go;

        bool multiplier;
        int actualIndex = 0;
        int lenghtIndex = 0;

        void Start()
        {
            

            lenghtIndex = GameObject.Find("Randomitemspavni").transform.childCount;
        }

        void Update()
        {
            multiplier = Input.GetKey(KeyCode.Z);
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                RaycastHit rcHit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rcHit, 2f, Physics.AllLayers))
                {
                    go = rcHit.transform.gameObject;
                }
            }

            if (go)
            {
                coordsTextarea = $"GO: {go.name}\nPOS: {go.transform.position.ToString()}ROT2: {go.transform.eulerAngles.ToString()}";

                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    go.transform.position = new Vector3(go.transform.position.x + (multiplier ? 0.02f : 0.7f), go.transform.position.y, go.transform.position.z);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    go.transform.position = new Vector3(go.transform.position.x - (multiplier ? 0.02f : 0.7f), go.transform.position.y, go.transform.position.z);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + (multiplier ? 0.02f : 0.7f));
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z - (multiplier ? 0.02f : 0.7f));
                }
                else if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + (multiplier ? 0.02f : 0.7f), go.transform.position.z);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - (multiplier ? 0.02f : 0.7f), go.transform.position.z);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y + (multiplier ? 0.5f : 2f), go.transform.eulerAngles.z);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y - (multiplier ? 0.5f : 2f), go.transform.eulerAngles.z);
                }
                else if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x - (multiplier ? 0.5f : 2f), go.transform.eulerAngles.y, go.transform.eulerAngles.z);
                }
                else if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x + (multiplier ? 0.5f : 2f), go.transform.eulerAngles.y, go.transform.eulerAngles.z);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, go.transform.eulerAngles.z - (multiplier ? 0.1f : 1f));
                }
                else if (Input.GetKeyDown(KeyCode.KeypadPeriod))
                {
                    go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, go.transform.eulerAngles.z + (multiplier ? 0.1f : 1f));
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameObject.Find("Player").transform.position = GameObject.Find("Table").transform.position;

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                actualIndex++;
                if (actualIndex >= lenghtIndex)
                    actualIndex = 0;


                List<GameObject> rootObjects = new List<GameObject>();
                Scene scene = SceneManager.GetActiveScene();
                scene.GetRootGameObjects(rootObjects);

                int count = 0;
                foreach (GameObject go in rootObjects)
                {
                    switch (go.name)
                    {
                        case "Table":
                            count++;
                            if (count == actualIndex)
                                GameObject.Find("Player").transform.position = go.transform.position;

                            break;
                    }
                }
            }
        }

        void OnGUI()
        {
            coordsTextarea = GUI.TextArea(new Rect(20, 100, 230, 150), coordsTextarea);
        }
    }
}
