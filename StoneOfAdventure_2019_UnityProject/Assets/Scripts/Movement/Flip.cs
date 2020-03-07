using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flip : MonoBehaviour
{
    //переменная для определения направления персонажа вправо/влево
    [HideInInspector]
    public bool isFacingRight = true;
    [HideInInspector] public UnityEvent Flipped;
    
    public void CheckDirection(float currentDirection)
    {
        if (currentDirection > 0 && !isFacingRight) FlipObject();
        //обратная ситуация. отражаем персонажа влево
        else if (currentDirection < 0 && isFacingRight) FlipObject();
    }

    public void FlipObject()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
        Flipped.Invoke();
    }

    private void OnDestroy()
    {
        Flipped.RemoveAllListeners();
    }
}
