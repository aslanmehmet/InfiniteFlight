using UnityEngine;

public class Player_Controller : MonoBehaviour {

    
    public float speed = 100f;
    float axisSpeed;
    float minAxisSpeed = 20;
 
    Rigidbody rb;

    public float smooth = 2.0F;
    public float tiltAngle = 30.0F;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //Ship Model
        Transform temp = this.gameObject.transform.GetChild(GetShipIndex());
        temp.gameObject.SetActive(true);

        //Spedd ayarlamaları yapılarcak
        int x = PlayerPrefs.GetInt(GetShipIndex() + "xSpeed");
        axisSpeed = minAxisSpeed + (2 * x);
        
    }

    //ekranın kararmasını engelleme
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    //Geminin Indexsinin alınması
    int GetShipIndex()
    {
        if (PlayerPrefs.HasKey("ShipIndex"))
        {
            return PlayerPrefs.GetInt("ShipIndex");
        }
        else
            return 0;

    }

    private void FixedUpdate()
    {
        Move();
    }

    //Ship Hareketi
    void Move()
    {
        rb.velocity = Vector3.forward * speed * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");

        float mobil = Input.acceleration.x * 3 ;

        Rotate(mobil);

        if (x != 0)
        {
            this.transform.position += new Vector3( x * axisSpeed * Time.deltaTime, 0, 0);
        }

        if (mobil != 0)
        {
            this.transform.position += new Vector3(mobil * axisSpeed * Time.deltaTime, 0, 0);
        }
    }

    //Ship Dönme
    void Rotate(float mobil)
    {
       
        float tiltAroundZ = mobil * tiltAngle;

        Quaternion target = Quaternion.Euler(0, 0, -tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    //Ship Hızının Artırılması.
    public void ChangeSpeed(int value)
    {
        speed += value * 100;
    }

    
   
}
