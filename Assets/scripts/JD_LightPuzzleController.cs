using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_LightPuzzleController : MonoBehaviour
{
    [System.Serializable]
    public class CollisionPair
    {
        public GameObject objectA; // First object
        public GameObject objectB; // Second object
    }

    public CollisionPair[] collisionPairs; // List of object pairs to track

    private Dictionary<GameObject, GameObject> collisionLookup = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        // Populate the lookup dictionary for fast collision checks
        foreach (var pair in collisionPairs)
        {
            if (pair.objectA != null && pair.objectB != null)
            {
                collisionLookup[pair.objectA] = pair.objectB;
                collisionLookup[pair.objectB] = pair.objectA; // Add both directions
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        // Check if the current object is one of our tracked pairs
        if (collisionLookup.TryGetValue(gameObject, out GameObject expectedObject) && expectedObject == otherObject)
        {
            Debug.Log("IT FUCKING WORKED");
        }
    }
}
