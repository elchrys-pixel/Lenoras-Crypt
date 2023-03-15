using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionVisualHandler : MonoBehaviour
{
    [Header("Set On Creation")]
    public Sprite tileSprite;
    public Vector2 forcePosition;
    public float impactForce;
    public PhysicsMaterial2D physicsMaterial;

    [Header("References")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parentPrefab;
    [SerializeField] private List<Sprite> spriteMasks;

    public void BreakSpriteToMasks()
    {
        List<GameObject> newObjects = new List<GameObject>();
        GameObject parent = Instantiate(parentPrefab, transform.position, Quaternion.identity);

        for (int i = 0; i < spriteMasks.Count; i++) // CREATE SPRITE OBJECTS, ADD TO LIST
        {
            GameObject newGO = Instantiate(prefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            newObjects.Add(newGO);
        }

        SpriteUpdater.UpdateSpriteObject(parent, newObjects, spriteMasks, tileSprite, physicsMaterial); // UPDATE SPRITE VARIABLES & FORCE UPDATE COLLISION SHAPE

        foreach (var rb in newObjects) // APPLY FORCE TO EACH SPRITE
        {
            Vector3 forceDirection = transform.position - (Vector3)forcePosition;
            //Debug.Log(forceDirection);
            rb.GetComponent<Rigidbody2D>().velocity = forceDirection * impactForce;
        }
        Destroy(gameObject);
    }
}
