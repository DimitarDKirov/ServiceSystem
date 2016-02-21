namespace ServiceSystem.Services.Web
{
    public interface IPublicCodeProvider
    {
        string Encode(int id, string name);

        int Decode(string codedInput);
    }
}
