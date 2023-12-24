namespace GameCharacter
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            GameCharacter[] characters = new GameCharacter[8];
            GameCharacter character = new GameCharacter("", 0, 0, false, 0, 0, 0, 0, 0);

            bool camp = true;

            for (int i = 0; i < characters.Length; i++)
            {
                Console.Write("Введите имя персонажа: ");
                string name = Console.ReadLine();

                Console.Write("Введите координату по оси x (от 1 до 10): ");
                int x = Convert.ToInt32(Console.ReadLine());
                x = Checking(x);

                Console.Write("Введите координату по оси y (от 1 до 10): ");
                int y = Convert.ToInt32(Console.ReadLine());
                y = Checking(y);

                Console.Write("Выберите лагерь (1 - союзники, 2 - враги): ");
                int campNumb = Convert.ToInt32(Console.ReadLine());
                campNumb = CheckingCamp(campNumb);
                if (campNumb == 1) // распределение по лагерям
                {
                    camp = true; // лагерь союзников
                }
                else if (campNumb == 2)
                {
                    camp = false; // лагерь врагов
                }

                int damage = rnd.Next(20, 45); // рандомное значение урона
                int maxHealth = rnd.Next(100, 200); // рандомное значение здоровья
                int health = maxHealth;

                int fullHealCount = rnd.Next(1, 3); // возможное количество полных восстановлений за игру (для каждого персонажа индивидуально)
                int healCount = rnd.Next(1, 5);

                Console.WriteLine();

                characters[i] = new GameCharacter(name, x, y, camp, damage, maxHealth, health, fullHealCount, healCount);
            }

            character.Game(characters);
        }

        static int Checking(int i)
        {
            while (i > 10 || i < 1)
            {
                Console.Write("Вы ввели некорректное значение. Попробуйте снова: ");
                i = Convert.ToInt32(Console.ReadLine());
            }
            return i;
        }

        static int CheckingCamp(int i)
        {
            while (i != 1 && i != 2)
            {
                Console.Write("Вы ввели некорректное значение. Попробуйте снова: ");
                i = Convert.ToInt32(Console.ReadLine());
            }
            return i;
        }
    }
}