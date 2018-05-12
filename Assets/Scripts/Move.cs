using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppliedForce //Класс описывающий приложенную силу на движущийся объект
{
    private double Direction; //Направление силы
    private double CurrentPower;//Текущая сила которая прикладывается к объекту
    private double DefaultPower; //Модуль силы, который прикладывается к системе, если есть воздейсвие

    public AppliedForce(double Power)
    {
        Direction = 1;
        CurrentPower = 0;
        DefaultPower = Power;
    }

    public void ChangeDirection()
    {
        Direction = -1 * Direction;
        CurrentPower = 0;
    }

    public void Stop()
    {
        CurrentPower = 0;
    }

    public void AddForce() //Функция приложения силы
    {
        CurrentPower = DefaultPower;
    }

    public double GetPower //Свойство возвращающее текущую силу с учетом направления
    {
        get { return Direction * CurrentPower; }
    }
}

