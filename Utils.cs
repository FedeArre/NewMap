using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NewMap
{
    class Utils
    {
        public static void Delete(string name)
        {
            GameObject go = GameObject.Find(name);
            if (go)
                GameObject.Destroy(go);
            else
                Debug.LogError("[NewMap-UTILS]: Tried to delete invalid object " + name);
        }

        /*public static void ChangePosition(string name, Vector3 newPosition, Quaternion newRot)
        {
            GameObject go = GameObject.Find(name);
            if (go)
            {
                go.transform.position = newPosition;
                go.transform.rotation = newRot;
            }
            else
                Debug.LogError("[NewMap-UTILS]: Tried to change pos of invalid object " + name);
        }*/

        public static void ModifyCollectibleSpawner(int number, Vector3 newPosition)
        {
            GameObject go = GameObject.Find("Randomitemspavni");
            if (!go)
            {
                Debug.LogError("[NewMap-UTILS]: Collectible parent not found");
                return;
            }

            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (child.name.Contains($"({number})"))
                {
                    child.position = newPosition;
                    break;
                }
            }
        }
        public static void ModifyRuinedSpawner(int number, Vector3 newPosition)
        {
            GameObject go = GameObject.Find("RuineCarSpawner");
            if (!go)
            {
                Debug.LogError("[NewMap-UTILS]: Ruined parent not found");
                return;
            }

            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (child.name.Contains($"({number})"))
                {
                    child.position = newPosition;
                    break;
                }
            }
        }
    }
}
