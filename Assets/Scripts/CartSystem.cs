using System;

namespace Lab1
{
    public delegate double PowerDelegate(double Time);

    public struct State //Структура, описывающая текущее состояние системы
    {
        public double Way;//Перемещение системы
        public double Speed;//Скорость системы
        public double Angle; //Угол отклонения 
        public double AngleDerivative; //Производная по углу
        public double Time; //Время
    }

    class CartSystem
    {
        private double Length; //Длинна нити, м.
        private double MassOfBall; //Масса шара, кг
        private double MassOfCart; //Масса тележки, кг

        private PowerDelegate Power; //Функция, описывающая зависимостть внешней силы от времени

        private double a1;
        private double a2;
        private double a3;
        private double a4;
        private double b1;
        private double w; //w^2

        private const double g = 9.8; //Ускорение свободного падения м/с

        private State CurrentState; //Текущее состяоние системы

        
        public CartSystem(double length, double massOfBall, double massOfCart, PowerDelegate power )
        {
            Length = length;
            MassOfBall = massOfBall;
            MassOfCart = massOfCart;
            Power = power;

            b1 = 1 / Length;
            w = g / Length;

            a1 = (MassOfBall/MassOfCart)*g;
            a2 = (MassOfBall/MassOfCart)*Length;
            a3 = (MassOfBall / MassOfCart);
            a4 = (MassOfCart + MassOfBall) * w / MassOfCart;


            CurrentState = new State();
            CurrentState.Time = 0; //Устанавливаем текущее время равноо нулю
            CurrentState.Angle = 0;
            CurrentState.AngleDerivative = 0;
            CurrentState.Way = 0;
            CurrentState.Speed = 0;
        }

        private double u(double Time)
        {
            return (Power(Time) / MassOfCart);
        }

        //Функция, возвращающая состояние системы для нового момента времени
        public State CalculateStateOfSystem(double DeltaTime)
        {
            State IntermediateState = new State();

            IntermediateState.Way = CurrentState.Way + CurrentState.Speed * DeltaTime;
            IntermediateState.Angle = CurrentState.Angle + CurrentState.AngleDerivative * DeltaTime;
            IntermediateState.Speed = CurrentState.Speed + WaySecondDerivate(CurrentState) * DeltaTime;
            IntermediateState.AngleDerivative = CurrentState.AngleDerivative + AngleSecodDerivate(CurrentState) * DeltaTime;
            IntermediateState.Time = CurrentState.Time + DeltaTime;

            State newState = new State();
            newState.Way = CurrentState.Way + (IntermediateState.Speed + CurrentState.Speed) * DeltaTime / 2;
            newState.Angle = CurrentState.Angle + (IntermediateState.AngleDerivative + CurrentState.AngleDerivative) * DeltaTime / 2;
            newState.Speed = CurrentState.Speed + (WaySecondDerivate(CurrentState) + WaySecondDerivate(IntermediateState)) * DeltaTime / 2;
            newState.AngleDerivative = CurrentState.AngleDerivative + (AngleSecodDerivate(CurrentState) + AngleSecodDerivate(IntermediateState)) * DeltaTime / 2;
            newState.Time = IntermediateState.Time;

            CurrentState = newState;
            return CurrentState;
        }

        private double WaySecondDerivate (State state)
        {
            double cos = Math.Cos(state.Angle);
            double sin = Math.Sin(state.Angle);
            double time = state.Time;
            return (-1 * a1 * sin * cos - a2 * Math.Pow(state.AngleDerivative, 2) * sin + u(time)) / (1 + a3 * Math.Pow(sin, 2));
        }

        private double AngleSecodDerivate (State state)
        {
            double cos = Math.Cos(state.Angle);
            double sin = Math.Sin(state.Angle);
            double time = state.Time;
            return (-1 * a4 * sin - a3 * Math.Pow(state.AngleDerivative, 2) * sin * cos + b1 * cos * u(time)) / (1 + a3 * Math.Pow(sin, 2));
        }       
    }
}
