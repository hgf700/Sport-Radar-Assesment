namespace Backend.Patterns;

public class RetryService
{
    public async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries, TimeSpan delay)
    {
        int retryCount = 0;

        while (true)
        {
            try
            {
                return await action();
            }
            catch (Exception ex) 
            {
                retryCount++;

                if (retryCount > maxRetries)
                {
                    throw new Exception($"Operation failed after {maxRetries} retries.", ex);
                }

                await Task.Delay(delay);
            }
        }
    }
}