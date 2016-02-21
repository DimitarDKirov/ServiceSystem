namespace ServiceSystem.Services.Web
{
    using System;

    public class PublicCodeProvider : IPublicCodeProvider
    {
        public int Decode(string codedInput)
        {
            string numbers = codedInput.Substring(0, codedInput.Length - 3);
            int orderNumber = int.Parse(numbers);
            return orderNumber;
        }

        public string Encode(int id, string name)
        {
            var nameTrimmed = name.Trim();
            string idString = id.ToString();
            string nameToCode;
            if (nameTrimmed.Length < 3)
            {
                var rand = new Random();
                nameToCode = nameTrimmed.PadRight(idString.Length + 3, (char)('a' + rand.Next(0, 26)));
            }
            else
            {
                nameToCode = nameTrimmed.Substring(0, 3);
            }

            var code = id.ToString() + nameToCode;
            return code;
        }
    }
}
