namespace MySpot.Api.Commands;

public interface ICommandHandler<T> where T : ICommand
{
    void Handle(T command);
}