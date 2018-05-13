using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lab1;

public class ControlScript : MonoBehaviour {

    [SerializeField]
    private GameObject Pendulum;
    [SerializeField]
    private double MassOfCart = 1;
    [SerializeField]
    private double MassOfBall = 1;
    [SerializeField]
    private double Lenght = 1.5;
    [SerializeField]
    private double Power = 15;
    [SerializeField]
    private double MaxSpeed = 100;
    
    //Параметры PID контроллера
    [SerializeField]
    private double P,I,D, Slack, DesiredValue;
    

    private AppliedForce cartMove;
    private CartSystem mySystem;
    private State state;
    private Vector3 newPosition;
    private PID pid;
    private bool pidIsOn;

	void Start () {
        mySystem = new CartSystem(Lenght, MassOfBall, MassOfCart, CalculatePower);
        cartMove = new AppliedForce(Power);
        pid = new PID(DesiredValue, Slack, P,I,D);
    }
	
	void FixedUpdate () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            pidIsOn = !pidIsOn;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (cartMove.GetPower <=0)
                cartMove.ChangeDirection();
            if (state.Speed < MaxSpeed)
                cartMove.AddForce();
            else
                cartMove.Stop();
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (cartMove.GetPower >= 0)
                    cartMove.ChangeDirection();
                if (state.Speed > -MaxSpeed)
                    cartMove.AddForce();
                else
                    cartMove.Stop();
            }
            else
            {
                cartMove.Stop();
            }
        }


        state = mySystem.CalculateStateOfSystem(Time.deltaTime);
        Pendulum.transform.RotateAround(transform.position, Vector3.back, (float)state.AngleDerivate*Time.deltaTime*57);
        newPosition = transform.position;
        newPosition.x = (float)state.Way;
        transform.position = newPosition;

        GetComponent<UpdateUI>().UpdateUserInterface(state, pidIsOn);
    }

    private double CalculatePower(double Time) 
    {
        if (pidIsOn)
        return cartMove.GetPower + pid.GetPower(state.Angle, state.AngleDerivate);
        return cartMove.GetPower;
    }
}
