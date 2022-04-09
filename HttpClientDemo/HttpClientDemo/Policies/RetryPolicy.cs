using Polly;
using Polly.Retry;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace HttpClientDemo.Policies;

public static class RetryPolicy
{
    public static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
            .WaitAndRetryAsync
            (
                retryCount: 5, 
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (ex, time) => Debug.WriteLine($"--> TimeSpan: {time.TotalSeconds}")
            );
    }
}
