using System.Collections.Generic;

namespace DotTrackingGame_Demo
{
    public class ReplayTransactionSource : IGameTransactionSource
    {
        private List<GameTransaction> inputData;
        private int readIndex = 0;

        public ReplayTransactionSource(List<GameTransaction> inputData)
        {
            this.inputData = inputData;
        }

        public GameTransaction GetInput()
        {
            if(readIndex < inputData.Count)
                return inputData[readIndex++];
            return null;
        }
    }
}
