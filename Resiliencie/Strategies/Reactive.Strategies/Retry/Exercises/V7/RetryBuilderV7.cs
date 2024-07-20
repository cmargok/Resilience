using Polly;
using Polly.Retry;
using Stragegies.Reactive.Strategies.Retry.Exercises.Models;

namespace Stragegies.Reactive.Strategies.Retry.Exercises.V7
{
    public class RetryBuilderV7
    {


        public static RetryPolicy<RetryPerson> GetV7Pipeline()
        {
            return Policy
                .HandleResult<RetryPerson>(c => c.Age > 8)
                .WaitAndRetry(
                    3,
                    delay => TimeSpan.FromSeconds(1),
                    (response, delay, retryCount, context) =>
                    {
                        Console.WriteLine("OnRetry, Attempt : {0}", retryCount);
                    });
        }
    }
}
