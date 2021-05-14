using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a singleton class.
// Based on the code on the Unify wiki:
// https://wiki.unity3d.com/index.php/Singleton
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttingDown = false;
    private static T instanceObject;

    private static object lockObject = new object();

    public static T Instance
    {
        get
        {
            // Do stuff to prevent singleton being accessed during game shutdown.
            if (shuttingDown)
            {

                Debug.LogWarning("Shutting down");
                return null;
            }

            lock (lockObject)
            {
                if (instanceObject == null)
                {
                    instanceObject = (T)FindObjectOfType<T>();

                    //Make a new instance if null
                    if (instanceObject == null) 
                    {
                        var singletonObject = new GameObject();
                        instanceObject = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        //Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }

            return(instanceObject);
        }
    }

    
    // called on application quit
    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }

    // called on object destruction.
    private void OnDestroy() 
    {
        shuttingDown = true;
    }
}
