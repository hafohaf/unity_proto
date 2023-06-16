using UnityEngine;

public class RandomPlacement : MonoBehaviour
{
    public GameObject[] objectsToPlace; // Array mit den Gameobjects, die platziert werden sollen
    public Transform[] placementPoints; // Array mit den Platzierungspunkten

    private void Start()
    {
        PlaceObjectsRandomly();
    }

     public void PlaceObjectsRandomly()
    {
        int objectCount = objectsToPlace.Length;
        int pointCount = placementPoints.Length;

        if (objectCount != pointCount)
        {
            Debug.LogError("Die Anzahl der zu platzierenden Objekte stimmt nicht mit der Anzahl der Platzierungspunkte überein.");
            return;
        }

        // Mische die Platzierungspunkte zufällig
        for (int i = 0; i < pointCount - 1; i++)
        {
            int randomIndex = Random.Range(i, pointCount);
            Transform temp = placementPoints[randomIndex];
            placementPoints[randomIndex] = placementPoints[i];
            placementPoints[i] = temp;
        }

        // Platziere die Gameobjects an den zufällig sortierten Platzierungspunkten
        for (int i = 0; i < objectCount; i++)
        {
            objectsToPlace[i].transform.position = placementPoints[i].position;
        }
    }
}