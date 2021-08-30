using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField]
    private string jsonFileToLoad;

    private List<MapCell> mapCellList;

    public static MapBordersCorrdinates mapBordersCoordinates;

    void Awake()
    {
        mapCellList = ResourceLoader.LoadMapCellsFromResources(jsonFileToLoad);

        BuildMap();

        SetMapBordersCoordinates();
    }



    private void BuildMap()
    {
        //Parent object for map tiles
        GameObject newMap = new GameObject("Map");


        foreach (MapCell mapCell in mapCellList)
        {
            GameObject mapCellGameObject = new GameObject(mapCell.Id);

            mapCellGameObject.transform.SetParent(newMap.transform);

            //creating sprite out of texture and assigning it to the newly created SpriteRenderer component
            //-----------------------------------------------------------------------------------------
            Sprite mapCellSprite = Sprite.
                Create(mapCell.texture, new Rect(0, 0, mapCell.texture.width, mapCell.texture.height), new Vector2(0, 0));

            mapCellGameObject.AddComponent<SpriteRenderer>().sprite = mapCellSprite;
            //-----------------------------------------------------------------------------------------


            //Adding layer and checking IsTrigger for future raycasting
            mapCellGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            mapCellGameObject.layer = 6;


            //Setting position based on MapCell data
            mapCellGameObject.transform.position = new Vector3(mapCell.X, mapCell.Y, 0);
        }
    }


    //Calculating map borders. A little overkill for test task, but it looks for tiles, which 
    //stretch the most on corresponding sides(considering texture width/height)
    private void SetMapBordersCoordinates()
    {

        MapCell leftMostCell = mapCellList[0];
        MapCell rightMostCell = mapCellList[0];
        MapCell topMostCell = mapCellList[0];
        MapCell bottomMostCell = mapCellList[0];


        foreach (MapCell mapCell in mapCellList)
        {
            if (mapCell.X + mapCell.Width > rightMostCell.X + rightMostCell.Width) rightMostCell = mapCell;
            if (mapCell.Y + mapCell.Height > topMostCell.Y + topMostCell.Height) topMostCell = mapCell;

            if (mapCell.X < leftMostCell.X) leftMostCell = mapCell;
            if (mapCell.Y < bottomMostCell.Y) bottomMostCell = mapCell;
        }


        float leftBorder = leftMostCell.X;
        float rightBorder = rightMostCell.X + rightMostCell.Width;
        float topBorder = topMostCell.Y + topMostCell.Height;
        float downBorder = bottomMostCell.Y;


        mapBordersCoordinates = new MapBordersCorrdinates(leftBorder, rightBorder, topBorder, downBorder);
    }

}
