﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    //переменная для определения направления персонажа вправо/влево
    [HideInInspector]
    public bool isFacingRight = true;
    
    public void CheckDirection(float currentDirection)
    {
        if (currentDirection > 0 && !isFacingRight) FlipObject();
        //обратная ситуация. отражаем персонажа влево
        else if (currentDirection < 0 && isFacingRight) FlipObject();
    }

    private void FlipObject()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
}
