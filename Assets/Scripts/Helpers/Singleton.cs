using System;
using UnityEngine;

namespace CardProject.Helpers
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                        throw new Exception(string.Format("An instance of {0} is needed in the scene, but there is none.", typeof(T)));
                }

                return instance;
            }
        }
    }
}