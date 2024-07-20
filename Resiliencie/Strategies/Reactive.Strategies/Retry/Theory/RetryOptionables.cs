using Polly;
using Polly.Retry;

namespace Stragegies.Reactive.Strategies.Retry.Theory
{
    public class RetryOptionables
    {
        public RetryStrategyOptions DefaultOptions()
        {
            //showing defaults
            var options = new RetryStrategyOptions();
            //ShouldHandle -> only handles OperationCanceledEception
            //MaxRetryAttemps -> 3
            //BackoffType -> Constant ->algorithm to generate computed delay
            //Delay - > 2 seconds
            //MaxDelay -> null
            //UseJitter -> False
            //DelayGenerator -> null
            //OnRetry -> null
            return options;
        }

        public RetryStrategyOptions NoDelayOptions()
        {
            var options = new RetryStrategyOptions
            {
                Delay = TimeSpan.Zero
            }; return options;
        }

        public RetryStrategyOptions ComplexOptions()
        {
            var options = new RetryStrategyOptions()
            {
                ShouldHandle = new PredicateBuilder().Handle<TimeoutException>(),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,
                MaxRetryAttempts = 4,
                Delay = TimeSpan.FromSeconds(4),
            }; return options;
        }

        public RetryStrategyOptions CustomeDeleyGeneratorOptions()
        {
            var options = new RetryStrategyOptions()
            {
                MaxRetryAttempts = 2,
                DelayGenerator = static args =>
                {
                    var delay = args.AttemptNumber switch
                    {
                        0 => TimeSpan.Zero,
                        1 => TimeSpan.FromSeconds(1),
                        _ => TimeSpan.FromSeconds(5)
                    };

                    return new ValueTask<TimeSpan?>(delay);
                }
            }; return options;
        }

        public RetryStrategyOptions<HttpResponseMessage> ObjectExtracionDelayOptions()
        {
            var options = new RetryStrategyOptions<HttpResponseMessage>()
            {
                DelayGenerator = static args =>
                {
                    if (args.Outcome.Result is HttpResponseMessage responseMessage && TryGetDelay(responseMessage, out TimeSpan delay))
                    {
                        return new ValueTask<TimeSpan?>(delay);
                    }

                    return new ValueTask<TimeSpan?>((TimeSpan?)null);
                }
            };

            static bool TryGetDelay(HttpResponseMessage responseMessage, out TimeSpan delay)
            {
                delay = default;

                return true;
            }
            return options;
        }

        public RetryStrategyOptions OnRetryOptions()
        {
            var options = new RetryStrategyOptions()
            {
                MaxRetryAttempts = 2,
                OnRetry = static args =>
                {
                    Console.WriteLine("OnRetry, Attempt : {0}", args.AttemptNumber);

                    return default;
                }
            }; return options;
        }

        public RetryStrategyOptions ForeverRetryOptions()
        {
            var options = new RetryStrategyOptions
            {
                MaxRetryAttempts = int.MaxValue,

            };

            return options;
        }



        public void AddToPipeline()
        {
            new ResiliencePipelineBuilder().AddRetry(DefaultOptions());
            new ResiliencePipelineBuilder<HttpResponseMessage>().AddRetry(ObjectExtracionDelayOptions());

        }
    }


}
