using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float _speed;

    float valueVert, valueHoriz;
    public void SetDirection(float axisHorisontal, float axisVertical)
    {
        valueVert = axisVertical;
        valueHoriz = axisHorisontal;
    }
    void Move()
    {
        var deltaH = valueHoriz * _speed * Time.deltaTime;
        var deltaV = valueVert * _speed * Time.deltaTime;

        Vector3 vector = transform.position;

        float newPosX = vector.x + deltaH;
        float newPosY = vector.y + deltaV;

        transform.position = new(newPosX, newPosY, vector.z);
    }
    private void Update()
    {
        Move();
    }
}
