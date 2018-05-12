using System;

public class PID {

    private double DesiredValue; //Желаемое значение угла отклонения
    private double IntegrationError; //Накапливаемая ошибка в угле отклонения
    private double Slack; //Допустима возвожная величина отклонения маятника    
    
    //Коэффициенты PID регулятора
    private double P;
    private double I;
    private double D;

    public PID(double desiredValue, double slack, double p, double i, double d)
    {
        DesiredValue = desiredValue;
        Slack = slack;
        P = p;
        I = i;
        D = d;
    }

    //Функция которая будет рассчитывать силу, которую нужно будет приложить к нашему вертолётику, чтобы маятник уравновесился  
    public double GetPower(double CurrentValue, double ValueDerivative)
    {
        //Если значение отколнения достаточно мало и производная отлонения тоже мала, то контроллер не срабатывает
        if (Math.Abs(CurrentValue - DesiredValue) < Slack && Math.Abs(ValueDerivative) < Slack)
        {
            IntegrationError = 0;
            return 0;
        }
        else
        {
            IntegrationError += (CurrentValue - DesiredValue);
            return GetP(CurrentValue) + GetI(IntegrationError) + GetD(ValueDerivative);
        }
    }

    private double GetP(double CurrentValue)
    {
        return -P * (CurrentValue - DesiredValue);
    }

    private double GetI(double integrationError)
    {
        return -I * IntegrationError;
    }

    private double GetD(double ValueDerivative)
    {
        return -D*ValueDerivative;
    }
}
