namespace PulseHub.Api.Scheme.Events;

public class AccessKeyEvents
{
    public Func<HeaderReceivedContext, Task> OnHeaderReceivedContext { get; set; } = context => Task.CompletedTask;

    public Func<ForbiddenContext, Task> OnForbiddentContext { get; set; } = context => Task.CompletedTask;

    public Func<ChallengeContext, Task> OnChallengeContext { get; set; } = context => Task.CompletedTask;
}
