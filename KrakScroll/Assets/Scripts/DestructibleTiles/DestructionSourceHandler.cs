using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DestructionSourceHandler : MonoBehaviour
{
    [TextArea(1, 12), SerializeField] string howToUse = "Spawn this object next to a wall, adjust width and depth using the defaultRadius; x = width, y = depth.\n\nDirection of destruction is relative to this objects position (place to the left of a tile and the impact direction is right, place below, direction up, etc)\n\nPrefab can be found in: Assets/Prefabs/DestructibleTiles > ImpactPoint";

    [Header("Destruction Settings")]
    public float impactForce;
    public Vector2Int defaultRadius;
    public PhysicsMaterial2D physicsMaterial;
    public Vector2 startPositionOffset;

    [Header("Prefab Reference")]
    [SerializeField] private GameObject tileSplitterPrefab;

    private Tilemap destructibleTileMap;

    private List<Vector3Int> tilesToBreak;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        if (destructibleTileMap == null) destructibleTileMap = GameObject.Find("DestructibleTileMap").GetComponent<Tilemap>();

        tilesToBreak = GetAffectedTiles(transform.position, defaultRadius, GetDirectionFromImpactPosition(transform.position));
    }

    private void FixedUpdate()
    {
        if (tilesToBreak.Count > 0)
        {
            // CONVERTING TILE IN LIST
            List<Vector3Int> tilesToRemoveFromList = new List<Vector3Int>();
            List<DestructionVisualHandler> convertedTiles = new List<DestructionVisualHandler>();

            foreach (var tile in tilesToBreak)
            {
                Vector3Int position = tile;
                position.z = (int)destructibleTileMap.transform.position.z;
                if (destructibleTileMap.GetTile(position))
                {
                    if (!tilesToRemoveFromList.Contains(position))
                    { 
                        // CREATE NEW SPRITE OBJECT
                        GameObject newTile = Instantiate(tileSplitterPrefab, position, Quaternion.identity);
                        DestructionVisualHandler handler = newTile.GetComponent<DestructionVisualHandler>();

                        // TRANSFER VALUES TO NEW OBJECT
                        handler.tileSprite = destructibleTileMap.GetSprite(position);
                        handler.forcePosition = transform.position;
                        handler.physicsMaterial = physicsMaterial;
                        handler.impactForce = impactForce;

                        // REMOVE TILE FROM WORLD, QUEUE TO REMOVE THIS POSITION FROM LIST, ADD TO CONVERTED LIST
                        destructibleTileMap.SetTile(position, null);
                        tilesToRemoveFromList.Add(position);
                        convertedTiles.Add(handler);
                    }
                }
                else tilesToRemoveFromList.Add(position);
            }

            foreach (var tile in tilesToRemoveFromList) // REMOVE TILES AFTER CREATION - CANNOT REMOVE DURING 'FOREACH'
            {
                tilesToBreak.Remove(tile);
            }
            foreach (var tile in convertedTiles) // BREAK CONVERTED SPRITES INTO INDIVIDUAL SEGMENTS/MASKS
            {
                tile.BreakSpriteToMasks();
            }

            // CLEAR LISTS INCASE OF ERROR
            tilesToRemoveFromList.Clear();
            convertedTiles.Clear();
        }
        else Destroy(this.gameObject);
    }

    private List<Vector3Int> GetAffectedTiles(Vector2 impactPosition, Vector2Int impactRadius, Vector2 direction)
    {
        List<Vector3Int> allTiles = new List<Vector3Int>();
        Vector3 rightDirection = Quaternion.Euler(0, 0, -90f) * direction;

        // IF NO RADIUS IS APPLIED, USE DEFAULT
        if (impactRadius == new Vector2Int(0, 0)) impactRadius = defaultRadius;

        // GET STARTING POSITION W/OFFSET FROM IMPACT POSITION
        Vector3Int startPosition = AdjustStartPosition(impactPosition, rightDirection, direction);

        if (destructibleTileMap.GetTile(startPosition))
        {
            List<Vector3Int> startTiles = new List<Vector3Int>();

            // GET WIDTH FOR EACH DIRECTION (LEFT/RIGHT)
            int width = impactRadius.x;
            if (impactRadius.x > 1) impactRadius.x = (width - 1) / 2;

            if (direction.x == 0) // UP & DOWN
            {
                if (width > 1)
                {
                    for (int x = -impactRadius.x; x < impactRadius.x; x++) // FAN OUT FROM CENTER START POSITION
                    {
                        Vector3Int pos1 = startPosition + new Vector3Int(x, 0, 0);
                        if (destructibleTileMap.GetTile(pos1) && !startTiles.Contains(pos1)) startTiles.Add(pos1);

                        Vector3Int pos2 = startPosition - new Vector3Int(x, 0, 0);
                        if (destructibleTileMap.GetTile(pos2) && !startTiles.Contains(pos2)) startTiles.Add(pos2);
                    }
                }
                else if (destructibleTileMap.GetTile(startPosition) && !startTiles.Contains(startPosition)) startTiles.Add(startPosition); // IF RADIUS OF 1

                int multiplyer; // SET DIRECTION
                if (direction.y > 0) multiplyer = 1;
                else multiplyer = -1;

                foreach (var point in startTiles) // GET DEPTH
                {
                    allTiles.Add(point);
                    for (int y = 0; y < impactRadius.y; y++)
                    {
                        Vector3Int pos = point + new Vector3Int(0, y * multiplyer, 0);
                        if (destructibleTileMap.GetTile(pos) && !allTiles.Contains(pos)) allTiles.Add(pos);
                    }
                }
            }
            if (direction.y == 0) // LEFT & RIGHT
            {
                if (width > 1)
                {
                    for (int y = -impactRadius.x; y < impactRadius.x; y++) // FAN OUT FROM CENTER START POSITION
                    {
                        Vector3Int pos1 = startPosition + new Vector3Int(0, y, 0);
                        if (destructibleTileMap.GetTile(pos1) && !startTiles.Contains(pos1)) startTiles.Add(pos1);

                        Vector3Int pos2 = startPosition - new Vector3Int(0, y, 0);
                        if (destructibleTileMap.GetTile(pos2) && !startTiles.Contains(pos2)) startTiles.Add(pos2);
                    }
                }
                else if (destructibleTileMap.GetTile(startPosition) && !startTiles.Contains(startPosition)) startTiles.Add(startPosition); // IF RADIUS OF 1

                int multiplyer; // SET DIRECTION
                if (direction.x > 0) multiplyer = 1;
                else multiplyer = -1;

                foreach (var point in startTiles) // GET DEPTH
                {
                    allTiles.Add(point);
                    for (int x = 0; x < impactRadius.y; x++)
                    {
                        Vector3Int pos = point + new Vector3Int(x * multiplyer, 0, 0);
                        if (destructibleTileMap.GetTile(pos) && !allTiles.Contains(pos)) allTiles.Add(pos);
                    }
                }
            }
        }

        return allTiles;
    }

    private Vector3Int AdjustStartPosition(Vector2 impactPosition, Vector3 rightDirection, Vector2 direction)
    {
        Vector3Int position = new Vector3Int();

        if (direction.x == 0)
        {
            int offset;
            int xOffset;
            if (direction.y > 0) // UP
            {
                offset = 1;
                xOffset = -1;
            }
            else // DOWN
            {
                offset = -2;
                xOffset = 0;
            }
            position = new Vector3Int((int)(impactPosition.x + rightDirection.x + xOffset), (int)(impactPosition.y + offset), 0);
        }
        if (direction.y == 0)
        {
            int offset;
            int yOffset;
            if (direction.x > 0) // RIGHT
            {
                offset = 1;
                yOffset = 0;
            }
            else // LEFT
            {
                offset = -2;
                yOffset = -1;
            }
            position = new Vector3Int((int)(impactPosition.x + offset), (int)(impactPosition.y + rightDirection.y + yOffset), 0);
        }
        return position;
    }

    private Vector2 GetDirectionFromImpactPosition(Vector2 impactPosition) // GET DIRECTION BASED ON TILE POSITION RELATIVE TO IMPACT POSITION, TEST IN 4 DIRECTIONS, OUTPUT ACTIVE TILE DIRECTION
    {
        Vector2 direction = Vector2.zero;
        Vector3Int position = new Vector3Int((int)(impactPosition.x + startPositionOffset.x), (int)(impactPosition.y + startPositionOffset.y), 0);

        if (destructibleTileMap.GetTile(position + new Vector3Int(1, 0, 0))) direction.x = 1; // RIGHT
        else if (destructibleTileMap.GetTile(position + new Vector3Int(-2, 0, 0))) direction.x = -1; // LEFT
        else if (destructibleTileMap.GetTile(position + new Vector3Int(0, 1, 0))) direction.y = 1; // UP
        else if (destructibleTileMap.GetTile(position + new Vector3Int(0, -2, 0))) direction.y = -1; // DOWN

        return direction;
    }
}
