using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    float lerpTime = 1;
    float currentLerpTime = 0;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 vector;

    bool isTop = true;
    bool isLeft = true;
    Vector3 pos;

    bool animDirY;

    private void LateUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        ScriptRestart();
    }

    float RandomLerpTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void ChangePos(Vector3 position , bool animY)
    {
        animDirY = animY;
        pos = position;
    }

    //Start Degerlerini girer.
    void ScriptRestart()
    {
        lerpTime = RandomLerpTime(0.5f, 3.0f);
        currentLerpTime = 0;
        isTop = true;
        setPos(pos);
    }

    //Baslayacagı pozisyonu ayarlar.
    void setPos(Vector3 trans)
    {
        //uzunlugunu alır.
        if (animDirY)
        {
            float rangeY = GetComponent<Renderer>().bounds.extents.y;
            vector = new Vector3(0, rangeY + 3, 0);
        }
        else
        {
            vector = new Vector3( 5 , 0, 0);
        }
        
        startPos = trans;
        endPos = startPos - vector;
    }

    //Animasyon
    void controlY()
    {
        if (endPos == transform.position)
        {
            if (isTop)
            {
                startPos = transform.position;
                endPos = startPos + vector;
                currentLerpTime = 0;
                isTop = !isTop;
            }
            else
            {
                startPos = transform.position;
                endPos = startPos - vector;
                currentLerpTime = 0;
                isTop = !isTop;
            }
        }
    }

    //Animasyon X
    void controlX()
    {
        if (endPos == transform.position)
        {
            if (isTop)
            {
                startPos = transform.position;
                endPos = startPos + 2 * vector;
                currentLerpTime = 0;
                isTop = !isTop;
            }
            else
            {
                startPos = transform.position;
                endPos = startPos - 2 * vector;
                currentLerpTime = 0;
                isTop = !isTop;
            }
        }
    }

    void ControlSelect(bool state)
    {
        if (state)
        {
            controlY();
        }
        else
        {
            controlX();
        }
    }

    //Animasyon
    void Move()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float Perc = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startPos, endPos, Perc);

        //Loop için 
        ControlSelect(animDirY);
    }

}
