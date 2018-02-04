using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Create : MonoBehaviour {

    //Floor Yaratılması 
    [Header("Floor")]
    public GameObject parentPrefabs;
    public GameObject atomPrefabs;
    public int zPiece;
    public int xPiece;
    public int floor_Extends;

    [Space]

    //Factory
    [Header("Factory")]
    public GameObject factory_Prefabs;
    public float factory_X_piece , factory_Z_piece ;

    private void Awake()
    {
        Create_Map( atomPrefabs , floor_Extends, xPiece , zPiece); //Oyun için Map Olusturulur.
    }

    void Create_Map(GameObject atom ,int floor_extends, int x_piece , int z_piece)
    {
        float zStartPosition = 0;       
        float xStartPosition = (x_piece - 1) * - floor_extends;
        float zPosition = zStartPosition;
        float xPosition = xStartPosition;

        for (int z = 0; z < z_piece; z++)
        {
            for (int x = 0; x < x_piece; x++)
            {
                Vector3 center = new Vector3(xPosition, 0, zPosition);
                GameObject parent = Instantiate(parentPrefabs, center, Quaternion.identity);
                Create_Floor(atom ,center, floor_extends, parent);
                //Mark
                Create_Factory(floor_Extends, parent , factory_Prefabs , factory_X_piece , factory_Z_piece);

                xPosition += 2 * floor_extends;
            }

            zPosition += 2 * floor_extends;
            xPosition = xStartPosition;
        }
    }

    void Create_Floor( GameObject atom , Vector3 localCenter, int placeSize, GameObject parent)
    {
        int atomMaxHeight = 3;

        float atomBounds = 2 * atom.GetComponent<Renderer>().bounds.extents.z; //Uzunluk Hesaplaması

        for (float z = localCenter.z - placeSize; z < localCenter.z + placeSize; z += atomBounds) //Yarıcapı kadar bir alan içinde 
        {
            for (float x = localCenter.x - placeSize; x < localCenter.x + placeSize; x += atomBounds)
            {
                GameObject floor = Instantiate(atom , new Vector3(x, 0, z), Quaternion.identity);
                floor.transform.localScale += new Vector3(0, Random.Range(0, atomMaxHeight), 0);
                floor.transform.SetParent(parent.transform);
            }
        }
    }

    void Create_Factory(float parent_Extends, GameObject parent, GameObject obje, float factory_X_piece, float factory_Z_piece)
    {
        float extens_x = parent_Extends;
        float extens_z = parent_Extends;
        Vector3 center = parent.transform.position;

        float factory_extends_x = extens_x / factory_X_piece;
        float factory_extends_z = extens_z / factory_Z_piece;

        for (float i = center.x - extens_x; i < center.x + extens_x; i += factory_extends_x * 2)
        {
            for (float k = center.z - extens_z; k < center.z + extens_z; k += factory_extends_z * 2)
            {
                GameObject Factory = Instantiate(obje, new Vector3(i + factory_extends_x, 0, k + factory_extends_z), Quaternion.identity);
                Factory.transform.SetParent(parent.transform);
                Factory.GetComponent<Factory>().factory_extends_x = factory_extends_x;
                Factory.GetComponent<Factory>().factory_extends_z = factory_extends_z;
            }
        }
    }

}
