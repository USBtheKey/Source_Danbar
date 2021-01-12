using GameSystem.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Utility
{
    public static class Util
    {
        public static Vector3 Direction(Vector3 origin, Vector3 destination) => destination - origin; 
        public static Vector2 RandomDirectionV2() => new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)).normalized;

        public static float FloatToVol(float num) => Mathf.Log10(num) * 20;

        //public static void LookAt2D(Transform obj, Vector3 target)
        //{
        //    obj.up = target - obj.position;
        //}

        public static Vector3 QuadLerp(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, float delta)
        {
            p1 = Vector3.Lerp(p1, p2, delta);
            p2 = Vector3.Lerp(p2, p3, delta);

            Vector3 result = Vector3.Lerp(p1, p2, delta);

            return result;
        }

        public static Vector3 CubicLerp(ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, ref Vector3 p4, float delta)
        {
            Vector3 prime1 = QuadLerp(ref p1, ref p2, ref p3, delta);
            Vector3 prime2 = QuadLerp(ref p2, ref p3, ref p4, delta);

            return Vector3.Lerp(prime1, prime2, delta);
        }

        public static void Log_NotImplemented()
        {
            Debug.LogWarning("Not Implemented.");
        }


        public static bool GetAllChildren(Transform obj, out List<Transform> children)
        {
            
            if(obj != null)
            {
                if(obj.transform.childCount > 0)
                {
                    List<Transform> objs = new List<Transform>();

                    for(int i = 0; i < obj.transform.childCount; i++)
                    {
                        objs.Add(obj.transform.GetChild(0));
                    }

                    children = objs;
                    return true;
                }
            }

            children = null;
            return false;
        }

        
    }


}

