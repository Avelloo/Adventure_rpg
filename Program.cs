namespace Adventure_rpg
{
    public static class Program
    {
        static void Main(string[] args)
        {
           

            Character character = new();
            character.CreateCharacter();
            character.Greetings();

        }

    }
}