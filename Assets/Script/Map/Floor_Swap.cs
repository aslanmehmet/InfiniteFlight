using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Swap : MonoBehaviour {

    public Map_Create floorSpawn;
    public Transform player;
    public float zSensor, xSensor;

    GameObject[] parentFloor;
    Transform[,] matrixLayer;

    int leftIndex , rightIndex, topIndex ,  botIndex ;
    int rowCount , columnCount;

    float backgrounSize;

    void Start()
    {
        SetMatrixLayer();

        backgrounSize = 2 * floorSpawn.floor_Extends;
       
        rowCount = matrixLayer.GetLength(0) ;
        columnCount = matrixLayer.GetLength(1) ;
         
        botIndex = 0;      
        topIndex = rowCount - 1;
        leftIndex = 0;
        rightIndex = columnCount - 1;
    }

    private void Update()
    {
        FloorSwap();
    }

    void FloorSwap()
    {

        if (Camera.main.transform.position.x < matrixLayer[0, leftIndex].position.x + xSensor) //MatrixLayerin Leftleri Kameranın sagında kalırsa
        {
            ScrollingLeftArray();
        }
        if (Camera.main.transform.position.x > matrixLayer[0, rightIndex].position.x - xSensor) //MatrixLayerin Rightları Kameranın solunda kalırsa
        {
            ScrollingRightArray();
        }
        if (Camera.main.transform.position.z > matrixLayer[botIndex, 0].position.z + zSensor) //MatrixLayerin botları Kameranın arkasında kalırsa
        {
            ScrollingTopArray();
        }
    }

    void SetMatrixLayer()
    {
        matrixLayer = new Transform[floorSpawn.zPiece , floorSpawn.xPiece ];  //Matrix için yer acılır
        parentFloor = GameObject.FindGameObjectsWithTag("ParentFloor");            //Parentlar bir diziye set edilir.
         
        int index = 0;
        for (int row = 0; row < floorSpawn.zPiece ; row++)
        {
            for (int column = 0; column < floorSpawn.xPiece ; column++)
            {
                matrixLayer[row , column ] = parentFloor[index++].GetComponent<Transform>();    //Parent disizindeki parentler matrix dizisine set edilir.
            }
        } 
    }

    void ScrollingTopArray()
    {
        //int lastLeft = leftIndex;
        for (int i = 0; i < columnCount ; i++)
        {
            //int lastRight = rightIndex;
            matrixLayer[botIndex, i].position = matrixLayer[topIndex, i].position + new Vector3(0, 0, backgrounSize); // En sagdaki boje , en soldaki objenin yanina konur.
        }
        topIndex = botIndex;         //Artık sen soldaki obje , eskiden en sagda olan objedir.        
        botIndex++;                   //bununla birlikte en sagdaki obje artık ortada kaldıgı için onu tekrar en sagda oldugunu belirtiriz.


        if (botIndex > rowCount - 1)
            botIndex = 0;
    }

    void ScrollingLeftArray()
    {
        for (int i = 0; i < rowCount ; i++)
        {
            //int lastRight = rightIndex;
            matrixLayer[i, rightIndex].position = matrixLayer[i, leftIndex].position - new Vector3(backgrounSize, 0, 0); // En sagdaki boje , en soldaki objenin yanina konur.
        }
        leftIndex = rightIndex;         //Artık sen soldaki obje , eskiden en sagda olan objedir.        
        rightIndex--;                   //bununla birlikte en sagdaki obje artık ortada kaldıgı için onu tekrar en sagda oldugunu belirtiriz.


        if (rightIndex < 0)
            rightIndex = columnCount - 1;
    }

    void ScrollingRightArray()
    {
        //int lastLeft = leftIndex;
        for (int i = 0; i < rowCount; i++)
        {
            //int lastRight = rightIndex;
            matrixLayer[i, leftIndex].position = matrixLayer[i, rightIndex].position + new Vector3(backgrounSize, 0, 0); // En sagdaki boje , en soldaki objenin yanina konur.
        }
        rightIndex = leftIndex;         //Artık sen soldaki obje , eskiden en sagda olan objedir.        
        leftIndex++;                   //bununla birlikte en sagdaki obje artık ortada kaldıgı için onu tekrar en sagda oldugunu belirtiriz.


        if (leftIndex > columnCount - 1)
            leftIndex = 0;
    }
}
