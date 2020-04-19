using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using CreaturesLibrary;
using System.Xml;

namespace CreatureGenerator
{
    class MainClass
    {
        public static readonly Random random = new Random();
        public const string xmlPath = "../../../creatures.xml";
        public static void Main(string[] args)
        {
            try
            {
                List<Creature> creatures = GenerateCreatures();
                Console.WriteLine(string.Join(",", creatures));
                WriteCreaturesToXml(creatures, xmlPath);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("Ошибка сериализации объекта.");
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
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла непредвиденная ошибка. {ex.Message}");
            }
        }
        public static void WriteCreaturesToXml(List<Creature> creatures, string path)
        {
            using (FileStream writer = new FileStream(path, FileMode.Create))
            {
                DataContractSerializer ser = new DataContractSerializer(creatures.GetType());
                ser.WriteObject(writer, creatures);
            }
        }

        /// <summary>
        /// Создание списка существ.
        /// </summary>
        /// <returns>Список существ.</returns>
        public static List<Creature> GenerateCreatures()
        {
            List<Creature> creatures = new List<Creature>();
            for (int i = 0; i < 30; i++)
            {
                creatures.Add(new Creature(GetName(), GetMovementType(), GetHealth()));

            }
            return creatures;
        }
        /// <summary>
        /// Генерация имени.
        /// </summary>
        /// <returns>Имя существа.</returns>
        public static string GetName()
        {
            int leagth = random.Next(6, 11);
            StringBuilder sb = new StringBuilder(leagth);
            sb.Append((char)random.Next('A', 'Z' + 1));
            for (int i = 1; i < leagth; i++)
            {
                sb.Append((char)random.Next('a', 'z' + 1));
            }
            return sb.ToString();
        }
        /// <summary>
        /// Генерация типа существа.
        /// </summary>
        /// <returns>Тип существа.</returns>
        public static MovementType GetMovementType()
        {
            return (MovementType)random.Next(0, 3);
        }
        /// <summary>
        /// Генерация здоровья существа.
        /// </summary>
        /// <returns>Здоровье существа.</returns>
        public static double GetHealth()
        {
            return random.NextDouble() * 10;
        }
    }
}
