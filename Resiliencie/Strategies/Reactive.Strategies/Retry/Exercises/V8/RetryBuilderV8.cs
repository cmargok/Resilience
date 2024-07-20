using Polly;
using Polly.Retry;
using Stragegies.Reactive.Strategies.Retry.Exercises.Models;

namespace Stragegies.Reactive.Strategies.Retry.Exercises.V8
{
    public class RetryBuilderV8
    {
        public static ResiliencePipeline<RetryPerson> GetV8Pipeline()
        {
            return new ResiliencePipelineBuilder<RetryPerson>().AddRetry(OptionsV8()).Build();
        }

        private static RetryStrategyOptions<RetryPerson> OptionsV8()
        {

            var options = new RetryStrategyOptions<RetryPerson>
            {
                MaxRetryAttempts = 3,
                ShouldHandle = new PredicateBuilder<RetryPerson>().HandleResult(c => c.Age > 8),
                OnRetry = static args =>
                {
                    Console.WriteLine("OnRetry, Attempt : {0}", args.AttemptNumber);

                    return default;
                },
                Delay = TimeSpan.FromSeconds(1),
            };

            return options;
        }
    }
}
