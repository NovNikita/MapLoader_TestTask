using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{

    //Load data from JSON file into MapCell objects
    public static List<MapCell> LoadMapCellsFromResources(string jsonFilename)
    {
        TextAsset json = Resources.Load(jsonFilename) as TextAsset;

        MapCellList mapCellList;

        mapCellList = JsonUtility.FromJson<MapCellList>(json.text);

        LoadTexturesInMapCells(mapCellList);

        return mapCellList.List;
    }



    //Adding textures to MapCell objects
    private static void LoadTexturesInMapCells(MapCellList mapCellList)
    {
        foreach (MapCell mapCell in mapCellList.List)
        {
            mapCell.texture = Resources.Load<Texture2D>(mapCell.Id);
        }
    }
}
