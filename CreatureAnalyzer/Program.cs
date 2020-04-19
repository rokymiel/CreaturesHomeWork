using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using CreaturesLibrary;

namespace CreatureAnalyzer
{
    class MainClass
    {
        public const string xmlPath = "../../../creatures.xml";
        public static void Main(string[] args)
        {
            try
            {
                List<Creature> creatures = ReadCreaturesFromXml(xmlPath);
                WriteSwimming(creatures);
                Console.WriteLine(new string('=', 50));
                WriteCreaturesSortedByHeath(creatures, "Сортировка по здоровью");
                Console.WriteLine(new string('=', 50));
                var group = WriteGroupOfCreatures(creatures);
                Console.WriteLine(new string('=', 50));
                WriteCreaturesSortedByHeath(group, "Сортировка предыдущего списка по здоровью");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Файл не найден.");
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("Ошибка создания списка объектов из файла.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Ошибка ввода/вывода.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("У вас нет разрешения на создание файла.");
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine("Ошибка безопасности.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка. {ex.Message}");
            }
            
        }
        /// <summary>
        /// Выводит группы перемноженные внутри.
        /// </summary>
        /// <param name="creatures">Список существ.</param>
        /// <returns>Группы.</returns>
        public static IEnumerable<Creature> WriteGroupOfCreatures(List<Creature> creatures)
        {
            Console.WriteLine("Деление по группам и перемножение внутри групп");
            var group = creatures.GroupBy(x => x.MovementType, (movementType, resGroup) => resGroup.Aggregate((first, second) => first * second));
            foreach (var item in group)
            {
                Console.WriteLine(item);
            }
            return group;
        }
        /// <summary>
        /// Выводит количество плавающих существ в списке.
        /// </summary>
        /// <param name="creatures">Список существ.</param>
        public static void WriteSwimming(List<Creature> creatures)
        {
            Console.WriteLine($"Количество плавающих существ: {creatures.Count(x => x.MovementType == MovementType.Swimming)}");
        }
        /// <summary>
        /// Выводит список отсортированный по здоровью.
        /// </summary>
        /// <param name="creatures">Список существ.</param>
        public static void WriteCreaturesSortedByHeath(IEnumerable<Creature> creatures, string message)
        {
            Console.WriteLine(message);
            creatures.OrderBy(x => x.Health).Take(10).ToList().ForEach(x => Console.WriteLine(x));
        }
        /// <summary>
        /// Чтение списка существ из файла в формате XML.
        /// </summary>
        /// <param name="path">Путь файла.</param>
        /// <returns>Список существ.</returns>
        public static List<Creature> ReadCreaturesFromXml(string path)
        {
            List<Creature> creatures;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(List<Creature>));
                creatures = (List<Creature>)ser.ReadObject(fs);
            }
            return creatures;
        }
    }
}
