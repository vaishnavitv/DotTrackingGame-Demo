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
        private double intensity = 16;

        public GameTransactionSource(double maxX, double maxY, Canvas canvas)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.canvas = canvas;
        }

        public GameTransaction GetInput()
        {
            double speedX = (2 * intensity * rand.NextDouble()) - intensity;
            double speedY = (2 * intensity * rand.NextDouble()) - intensity;

            posX = Clamp(posX + speedX, 0, maxX);
            posY = Clamp(posY + speedY, 0, maxY); //Don't get it too close to the bottom.

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
