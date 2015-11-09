namespace LearningBasic.Mocks
{
    using System;
    using LearningBasic.RunTime;

    public class MockLoop : ILoop
    {
        private int counter;

        public MockLoop(int counter)
        {
            this.counter = counter;
        }

        public bool IsOver { get { return counter == 0; } }

        public void TakeStep()
        {
            if (counter > 0)
                counter--;
        }
    }
}
