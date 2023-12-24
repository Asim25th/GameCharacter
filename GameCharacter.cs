namespace GameCharacter
{
    class GameCharacter
    {
        private string name; // имя персонажа
        private int x; // координата по оси x (столбцы поля)
        private int y; // координата по оси y (строки поля)
        private bool camp; // принадлежность к лагерю (союзник / враг)
        private int damage; // урон
        private int maxHealth; // максимальный уровень здоровья
        private int health; // текущий уровень здоровья
        private int fullHealCount; // количество возможностей полного восстановления за игру
        private int healCount;
        public GameCharacter[] characters = new GameCharacter[4];

        public GameCharacter(string name, int x, int y, bool camp, int damage, int maxHealth, int health, int fullHealCount, int healCount)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.camp = camp;
            this.damage = damage;
            this.maxHealth = maxHealth;
            this.health = health;
            this.fullHealCount = fullHealCount;
            this.healCount = healCount;
        }

        public void Game(GameCharacter[] characters) // структура игры
        {
            while (true)
            {
                Console.Clear();
                camp = CampChoice(camp); // выбор лагеря
               
                Output(characters, camp); // вывод всех персонажей из выбранного лагеря
                Console.Write("\nВведите номер желаемого персонажа: ");
                int choice = Convert.ToInt32(Console.ReadLine()); // выбор персонажа
                int i = choice - 1;

                Console.WriteLine();
                CharacterActions(characters, i); // вызов метода для дальнейших действий в игре
            }
        }

        private static bool CampChoice(bool camp) // выбор лагеря
        {
            Console.WriteLine("Выберите один из двух лагерей:\n");
            Console.WriteLine("1. Союзники");
            Console.WriteLine("2. Враги");
            Console.Write("\nВведите номер желаемого лагеря: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    camp = true; // лагерь союзников
                    break;
                case 2:
                    camp = false; // лагерь врагов
                    break;
            }
            return camp;
        }

        private static void Output(GameCharacter[] characters, bool camp) // вывод всех персонажей из выбранного лагеря
        {
            Console.WriteLine("Доступные для выбора персонажи (в скобках - координаты персонажа): \n");
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].camp == camp) // проверка, чтобы выводились только те персонажи, у которых указан выбранных пользователем лагерь
                {
                    if (characters[i].health == 0) // проверка, чтобы убитые персонажи не выводились
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {characters[i].name} ({characters[i].x}, {characters[i].y})"); // вывод имени и координат персонажа
                    }
                }
            }
        }

        private void CharacterActions(GameCharacter[] characters, int i) // действия над выбранным персонажем
        {
            CharacterInfo(characters, i); // вывод информации о выбранном персонаже
            Console.ReadKey();
            Console.WriteLine();

            ActionsMenu(); // вывод меню со списком доступных действий
            Console.Write("\nВведите номер желаемого действия: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    ChangePosition(characters, i);
                    break;
                case 2:
                    Console.WriteLine();
                    ChangeCamp(characters, i);
                    break;
                case 3:
                    Console.WriteLine();
                    FullHeal(characters, i);
                    Console.Write("Нажмите Enter для возвращения в меню");
                    Console.ReadKey();
                    Console.Clear();
                    CharacterActions(characters, i);
                    break;
                case 4:
                    Console.WriteLine();
                    Heal(characters, i);
                    Console.Write("Нажмите Enter для возвращения в меню");
                    Console.ReadKey();
                    Console.Clear();
                    CharacterActions(characters, i);
                    break;
                case 5:
                    Console.Clear();
                    Game(characters);
                    break;
                case 6:
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Game(characters);
                    break;
            }
            Console.WriteLine();
        }

        private static void CharacterInfo(GameCharacter[] characters, int i)
        {
            Console.WriteLine("ИНФОРМАЦИЯ О ПЕРСОНАЖЕ");
            Console.WriteLine($"Имя: {characters[i].name}");
            Console.WriteLine($"Лагерь (True - союзники, False - враги): {characters[i].camp}");
            Console.WriteLine($"Координата по оси х: {characters[i].x}");
            Console.WriteLine($"Координата по оси y: {characters[i].y}");
            Console.WriteLine($"Здоровьe: {characters[i].health}/{characters[i].maxHealth}");
            Console.WriteLine($"Урон: {characters[i].damage}");
            Console.WriteLine($"Доступно лечений: {characters[i].healCount}");
            Console.WriteLine($"Доступно полных восстановлений: {characters[i].fullHealCount}");
        }

        private static void ActionsMenu() // меню действий
        {
            Console.WriteLine("Доступные действия над персонажем:\n");
            Console.WriteLine("1. Перемещение");
            Console.WriteLine("2. Сменить лагерь");
            Console.WriteLine("3. Полное восстановление");
            Console.WriteLine("4. Лечение");
            Console.WriteLine("5. Смена персонажа");
            Console.WriteLine("6. Выход");
        }

        private void ChangePosition(GameCharacter[] characters, int i) // перемещение по осям 
        {
            Tip(characters, i);

            Console.Write($"\nУкажите новое значение x (сейчас {characters[i].x}): ");
            characters[i].x = Convert.ToInt32(Console.ReadLine());
            characters[i].x = Checking(characters[i].x);

            Console.Write($"Укажите новое значение x (сейчас {characters[i].y}): ");
            characters[i].y = Convert.ToInt32(Console.ReadLine());
            characters[i].y = Checking(characters[i].y);

            Console.WriteLine($"Новые координаты: ({characters[i].x}; {characters[i].y})");

            EnemiesCheck(characters, i); // проверка, есть ли еще кто-то на той же клетке

            Console.Write("Нажмите Enter для возвращения в меню");
            Console.ReadKey();
            Console.Clear();

            CharacterActions(characters, i);
        }

        private static void Tip(GameCharacter[] characters, int i) // подсказка при перемещении (показывает, на каких координатах находятся враги)
        {
            Console.WriteLine("ПОДСКАЗКА: Координаты расположения противников на поле\n");
            for (int j = 0; j < characters.Length; j++)
            {
                if (characters[j].camp == !characters[i].camp && characters[j].health != 0) // при нахождении на одной клетке с противником и ненулевом здоровье
                {
                    Console.WriteLine($"{j + 1}. {characters[j].name} ({characters[j].x}; {characters[j].y})");
                }
            }
        }

        private void ChangeCamp(GameCharacter[] characters, int i) // смена лагеря
        {
            Console.Write($"СМЕНА ЛАГЕРЯ: Лагерь {characters[i].camp} -> ");
            characters[i].camp = !characters[i].camp; // изменение лагеря на противоположный
            Console.WriteLine($"{characters[i].camp}");
            Console.Write("Нажмите Enter для возвращения в меню");
            Console.ReadKey();
            Console.Clear();
            CharacterActions(characters, i);
        }

        private static void FullHeal(GameCharacter[] characters, int i) // полное восстановление здоровья
        {
            if (characters[i].fullHealCount == 0)
            {
                Console.WriteLine("Вы не можете полностью восстановить здоровье, так как вы привысили количество полных восстановлений за игру\n");
            }
            else
            {
                if (characters[i].health <= 50)
                {
                    characters[i].health = characters[i].maxHealth;
                    characters[i].fullHealCount -= 1; // списание одного шанса на полное восстановление здоровья
                    Console.WriteLine($"Вы полностью восстановили здоровье!\n");
                    Console.WriteLine($"Уровень здоровья: {characters[i].health}/{characters[i].maxHealth}");
                    Console.WriteLine($"Осталось восстановлений: {characters[i].fullHealCount}\n");
                }
                else
                {
                    Console.WriteLine("Вы не можете применить полное восстановление, так как текущий уровень вашего здоровья выше 50\n");
                }
            }
        }

        private static void Heal(GameCharacter[] characters, int i) // частичное лечение
        {
            if (characters[i].healCount == 0)
            {
                Console.WriteLine("Вы уже израсходовали все свои шансы на лечение!\n");
            }

            else if (characters[i].health > 50 && characters[i].healCount > 0) // слишком высокий уровень здоровья для лечения
            {
                Console.WriteLine("Ваш уровень здоровья слишком высок для применения лечения (50 и меньше)\n");
            }
            else
            {
                Random rnd = new Random();
                int healVolume = rnd.Next(10, 30);
                characters[i].health += healVolume;
                characters[i].healCount -= 1; // списание одного шанса на частичное восстановление здоровья
                Console.WriteLine($"Вы восстановили {healVolume} здоровья!\n");
                Console.WriteLine($"Уровень здоровья: {characters[i].health}/{characters[i].maxHealth}");
                Console.WriteLine($"Осталось лечений: {characters[i].healCount}\n");
            }
        }

        private static int Checking(int i) // проверка на корректность введенных значений
        {
            while (i > 10 || i < 1)
            {
                Console.Write("Вы ввели некорректное значение. Попробуйте снова: ");
                i = Convert.ToInt32(Console.ReadLine());
            }
            return i;
        }

        private void EnemiesCheck(GameCharacter[] characters, int k) // проверка, есть ли кто-то на той клетке, где стоим мы
        {
            for (int i = 0; i < characters.Length; i++) // проверка каждого персонажа на нахождение в данной клетке
            {
                if (i == k) // пропуск самого себя
                {
                    continue;
                }
                else
                {
                    if (characters[i].y == characters[k].y && characters[i].x == characters[k].x) // если персонажи находятся на одной и той же клетке
                    {
                        if (characters[i].camp == !characters[k].camp) // если этот персонаж противник
                        {
                            Console.WriteLine("\nВНИМАНИЕ: На одной клетке с вами находится вражеский персонаж!\n");
                            CharacterInfo(characters, i); // вывод информации о противнике
                            Console.WriteLine("\nЖелаете сразиться с ним?\n");
                            Console.WriteLine("1. Да, вперед в бой!\n2. Нет, я сейчас не готов к сражениям");
                            Console.Write("\nВведите номер выбранного варианта: ");
                            int choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("Вы осмелились сразиться с противником. Тогда в бой!");
                                    Fight(characters, k, i); // метод сражения (игрок с индексом k и противник - i)
                                    break;
                                case 2:                                    
                                    Console.WriteLine("Порой побег - это смелое решение... Возвращаемся в меню доступных действий");
                                    Console.ReadKey();
                                    Console.Clear();
                                    CharacterActions(characters, i);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void Fight(GameCharacter[] characters, int k, int i) // сражение
        {
            Console.WriteLine("\nВАШ ПЕРСОНАЖ\n");
            CharacterInfo(characters, k);

            Console.WriteLine("\nПЕРСОНАЖ ПРОТИВНИКА\n");
            CharacterInfo(characters, i);

            Console.WriteLine($"\nДа начнется великая битва!");
            Console.Write("Нажмите Enter для перехода к сражению");
            Console.ReadKey();
            Console.Clear();

            while (true) // пока один из персонажей не умрет
            {
                characters[k].health -= characters[i].damage; // противник наносит игроку урон
                Console.WriteLine($"Персонаж {characters[k].name} получает от игрока {characters[i].name} урон в размере {characters[i].damage} единиц");

                if (characters[k].health <= 0) // если игрок убит
                {
                    characters[k].health = 0; // проверка, чтобы здоровье не могло стать отрицательным

                    Console.WriteLine($"Ваш уровень здоровья: {characters[k].health}/{characters[k].maxHealth}");
                    Console.WriteLine($"Уровень здоровья противника: {characters[i].health}/{characters[i].maxHealth}");

                    Console.WriteLine("\nВаш персонаж героически пал в пылу битвы!");
                    characters[k].x = 0;
                    characters[k].y = 0;
                    characters[k].health = 0;
                    characters[k].maxHealth = 0;
                    characters[k].damage = 0;
                    characters[k].healCount = 0;
                    characters[k].fullHealCount = 0;

                    CharactersCountCheck(characters); // проверка, остался ли кто-то живой в команде

                    Console.Write("\nНажмите Enter для возвращения в меню");
                    Console.ReadKey();
                    Console.Clear();

                    Game(characters); // заново запускается метод игры
                }

                characters[i].health -= characters[k].damage; // игрок наносит урон противнику
                Console.WriteLine($"Персонаж {characters[i].name} получает от игрока {characters[k].name} урон в размере {characters[k].damage} единиц\n");

                if (characters[i].health <= 0) // если противник был убит
                {
                    characters[i].health = 0; // проверка, чтобы здоровье не могло стать отрицательным

                    Console.WriteLine($"Ваш уровень здоровья: {characters[k].health}/{characters[k].maxHealth}");
                    Console.WriteLine($"Уровень здоровья противника: {characters[i].health}/{characters[i].maxHealth}");

                    Console.WriteLine($"\nВы уничтожили персонажа {characters[i].name}! Это победа!");
                    characters[i].x = 0;
                    characters[i].y = 0;
                    characters[i].health = 0;
                    characters[i].maxHealth = 0;
                    characters[i].damage = 0;
                    characters[i].healCount = 0;
                    characters[i].fullHealCount = 0;

                    CharactersCountCheck(characters); // проверка, остался ли кто-то живой в команде

                    Console.Write("\nНажмите Enter для возвращения в меню");
                    Console.ReadKey();
                    Console.Clear();

                    CharacterActions(characters, k); // возвращение в меню
                }

                else // если по итогу битвы никто не умер
                {
                    Console.WriteLine($"Ваш уровень здоровья: {characters[k].health}/{characters[k].maxHealth}");
                    Console.WriteLine($"Уровень здоровья противника: {characters[i].health}/{characters[i].maxHealth}");

                    Console.WriteLine("\nУ вас есть время для перевязки ран, выберите свое следующее действие\n");
                    Console.WriteLine("1. Лечение\n2. Полное восстановление здоровья\n3. Отказ от какого-либо лечения");
                    Console.Write("\nВведите номер своего следующего действия: ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine();
                            Heal(characters, k);
                            break;
                        case 2:
                            Console.WriteLine();
                            FullHeal(characters, k);
                            break;
                        case 3:
                            Console.WriteLine("\nВы отказались от медицинской помощи");
                            break;
                    }
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void CharactersCountCheck(GameCharacter[] characters) // проверка, сколько осталось живых персонажей
        {
            int TrueCampCount = 0;
            int FalseCampCount = 0;

            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].health != 0 && characters[i].camp == true) // считаем количество союзников
                {
                    TrueCampCount++;
                }
                else if (characters[i].health != 0 && characters[i].camp == false) // считаем количество врагов
                {
                    FalseCampCount++;
                }
            }

            if (TrueCampCount == 0)
            {
                Console.WriteLine("Вся ваша команда была уничтожена! Вы проиграли!");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
            else if (FalseCampCount == 0)
            {
                Console.WriteLine("Вы уничтожили всю команду противника! Поздравляем с победой!");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
        }
    }
}