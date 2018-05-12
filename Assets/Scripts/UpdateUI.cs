using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lab1;

public class UpdateUI : MonoBehaviour {

    public UnityEngine.UI.Text  AngleText, SpeedText, AngleDerivativeText, X;

    public void UpdateUserInterface(State state)
    {
        AngleText.text = state.Angle.ToString("N3");
        SpeedText.text = state.Speed.ToString("N3");
        AngleDerivativeText.text = state.AngleDerivate.ToString("N3");
        X.text = state.Way.ToString("N3");
    }
}
