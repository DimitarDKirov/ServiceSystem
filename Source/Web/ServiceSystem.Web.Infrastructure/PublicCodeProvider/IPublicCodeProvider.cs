namespace ServiceSystem.Infrastructure.PublicCodeProvider
{
    public interface IPublicCodeProvider
    {
        string Encode(int id, string name);

        int Decode(string codedInput);
    }
}
