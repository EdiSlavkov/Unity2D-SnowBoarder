using Assets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Terrain : MonoBehaviour
{
    private SpriteShapeController shape;
    [SerializeField] private GameObject finishLine;
    [SerializeField] private List<GameObject> environmentElementsList = new List<GameObject>();
    [SerializeField] private List<GameObject> cloudsList = new List<GameObject>();
    [SerializeField] private int numberOfPoints = 50;
    [SerializeField] private int distanceBetweenPoints = 10;
    [SerializeField] private float pointMinHeight = 5f;
    [SerializeField] private float pointMaxHeight = 20f;
    [SerializeField] private int pointIndex = 2;
    [SerializeField] private float tangentMinWidth = 1f;
    [SerializeField] private float tangentMaxWidth = 5f;
    [SerializeField] private float minSplineHeight = 1f;
    [SerializeField] private float maxSplineHeight = 4f;
    [SerializeField] private int cloudMinSize = 1;
    [SerializeField] private int cloudMaxSize = 5;
    [SerializeField] private int cloudMinHeight = 10;
    [SerializeField] private int cloudMaxHeight = 16;

    private void Start()
    {
        shape = GetComponent<SpriteShapeController>();
        for (int i = pointIndex; i < numberOfPoints; i++)
        {
            float xPosition = i * distanceBetweenPoints;
            float yPosition = Random.Range(pointMinHeight, pointMaxHeight);
            shape.spline.InsertPointAt(i, new Vector3(xPosition, yPosition, 0));
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-Random.Range(tangentMinWidth, tangentMaxWidth), 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(Random.Range(tangentMinWidth, tangentMaxWidth), 0, 0));
            shape.spline.SetHeight(i, Random.Range(minSplineHeight, maxSplineHeight));
            if (i == numberOfPoints - 1)
            {
                finishLine.transform.position = new Vector3(xPosition, yPosition + pointIndex, 0);
            }
            else
            {
                SetRandomEnvirontmentObject(xPosition, yPosition);
                SetRandomCloud(xPosition, yPosition);
            }
        }
    }

    private void SetRandomEnvirontmentObject(float xPosition, float yPosition)
    {
        GameObject environmentElement = environmentElementsList[Random.Range(0, environmentElementsList.Count)];
        if (environmentElement.CompareTag(Utils.TreeTag))
        {
            Instantiate(environmentElement, new Vector3(xPosition, yPosition + pointIndex + 1, 0), Quaternion.identity);
        } 
        else
        {
            Instantiate(environmentElement, new Vector3(xPosition, yPosition + pointIndex - 1, 0), Quaternion.identity);
        }
    }

    private void SetRandomCloud(float xPosition, float yPosition)
    {
        int randomSize = Random.Range(cloudMinSize, cloudMaxSize);
        int randomHeight = Random.Range(cloudMinHeight, cloudMaxHeight);
        GameObject randomCloud = cloudsList[Random.Range(0, cloudsList.Count)];
        randomCloud.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        Instantiate(randomCloud, new Vector3(xPosition, yPosition + randomHeight, 0), Quaternion.identity);
    }
}