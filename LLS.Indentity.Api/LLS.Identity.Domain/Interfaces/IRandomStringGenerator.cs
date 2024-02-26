namespace LLS.Identity.Domain.Interfaces;

public interface IRandomStringGenerator
{
    string Generate(int length);
}