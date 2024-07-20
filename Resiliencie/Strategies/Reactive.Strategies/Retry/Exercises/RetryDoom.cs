using Stragegies.Reactive.Strategies.Retry.Exercises.Models;
using Stragegies.Reactive.Strategies.Retry.Exercises.V7;
using Stragegies.Reactive.Strategies.Retry.Exercises.V8;

namespace Stragegies.Reactive.Strategies.Retry.Exercises
{
    public class RetryDoom
    {
        public void CallV7(int age)
        {
            var executerV7 = RetryBuilderV7.GetV7Pipeline();

            var resultV7 = executerV7.Execute(() =>
            {
                return ProcessRetryPerson(age);
            });


        }
        public void CallV8(int age)
        {
            var executer = RetryBuilderV8.GetV8Pipeline();
            var result = executer.Execute(() =>
            {
                return ProcessRetryPerson(age);
            });
        }

        public RetryPerson ProcessRetryPerson(int edad)
        {
            Console.WriteLine("aqui ando " + edad);
            return new RetryPerson { Age = edad };
        }
    }
}
