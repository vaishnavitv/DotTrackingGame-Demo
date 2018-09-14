using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotTrackingGame_Demo
{
    public class GameTransactionSource : IGameTransactionSource
    {
        private Random rand = new Random();
        private Canvas canvas;

        private double posX = 0, posY = 0;
        private double maxX = 0, maxY = 0;
        private double speedX = 0, speedY = 0;
        private long ticks = 0;

        private const double smoothingFactor = 0.125;
        private const double intensity = 16;
        private const int speedTicks = 8;

        public GameTransactionSource(double maxX, double maxY, Canvas canvas)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.canvas = canvas;
        }

        public GameTransaction GetInput()
        {
            ticks++;
            if ( (speedTicks <= 1) || (ticks % speedTicks) == 0)
            {
                speedX = (2 * intensity * rand.NextDouble()) - intensity;
                speedY = (2 * intensity * rand.NextDouble()) - intensity;
            }


            //We can lerp, but we will go with exponential smoothing for position.
            double newPosX = (posX + speedX) * (1 - smoothingFactor)  + (posX) * smoothingFactor;
            double newPosY = (posY + speedY) * (1 - smoothingFactor)  + (posY) * smoothingFactor;

            posX = Clamp(newPosX, 0, maxX);
            posY = Clamp(newPosY, 0, maxY); //Don't get it too close to the bottom.

            Point mouseInput = Mouse.GetPosition(canvas);

            GameTransaction gameTransaction = new GameTransaction
            {
                MousePoint = mouseInput,
                GamePoint = new Point(posX, posY)
            };

            return gameTransaction;
        }


        private double Clamp(double valueToClamp, double minValue, double maxValue)
        {
            if (valueToClamp < minValue)
                return minValue;
            if (valueToClamp > maxValue)
                return maxValue;

            return valueToClamp;
        }
    }
}
