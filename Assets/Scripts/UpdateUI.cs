using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lab1;

public class UpdateUI : MonoBehaviour {

    public UnityEngine.UI.Text  AngleText, SpeedText, AngleDerivativeText, X, PIDText;

    public void UpdateUserInterface(State state, bool PID)
    {
        AngleText.text = state.Angle.ToString("N3");
        SpeedText.text = state.Speed.ToString("N3");
        AngleDerivativeText.text = state.AngleDerivative.ToString("N3");
        X.text = state.Way.ToString("N3");
        if (PID)
            PIDText.text = "ON";
        else
            PIDText.text = "OFF";
    }
}
