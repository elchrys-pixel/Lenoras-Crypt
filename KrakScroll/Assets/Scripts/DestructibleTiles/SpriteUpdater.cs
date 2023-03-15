using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteUpdater
{
    public static void UpdateSpriteObject(GameObject parent, List<GameObject> newGO, List<Sprite> sprites, Sprite tileSprite, PhysicsMaterial2D physicsMaterial)
    {
        for (int i = 0; i < newGO.Count; i++)
        {
            newGO[i].GetComponent<Rigidbody2D>().sharedMaterial = physicsMaterial;
            newGO[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
            newGO[i].AddComponent<PolygonCollider2D>();
            newGO[i].GetComponent<PolygonCollider2D>().sharedMaterial = physicsMaterial;

            UpdateColliderToSprite.UpdateShapeToSprite(newGO[i].GetComponent<PolygonCollider2D>());

            newGO[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = tileSprite;
            newGO[i].transform.GetChild(0).GetComponent<SpriteMask>().sprite = sprites[i];
            newGO[i].transform.parent = parent.transform;

            newGO[i].transform.localScale *= 0.95f;
        }
    }
}

public static class UpdateColliderToSprite
{
    public static void UpdateShapeToSprite(PolygonCollider2D collider)
    {
        Sprite sprite = collider.GetComponent<SpriteRenderer>().sprite;

        if (collider != null && sprite != null)
        {
            collider.pathCount = sprite.GetPhysicsShapeCount();
            List<Vector2> path = new List<Vector2>();
            for (int i = 0; i < collider.pathCount; i++)
            {
                path.Clear();
                sprite.GetPhysicsShape(i, path);
                collider.SetPath(i, path.ToArray());
            }
        }
    }
}