using System;
using System.Linq;
using System.Xml;
using System.Runtime.Serialization;
namespace CreaturesLibrary
{
    [DataContract]
    public class Creature
    {
        public Creature(string name, MovementType movementType, double health)
        {
            Name = name;
            MovementType = movementType;
            Health = health;
        }
        /// <summary>
        /// Имя существа.
        /// </summary>
        [DataMember]
        public string Name { get; private set; }
        /// <summary>
        /// Тип существа.
        /// </summary>
        [DataMember]
        public MovementType MovementType { get; private set; }
        /// <summary>
        /// Здоровье существа.
        /// </summary>
        [DataMember]
        public double Health { get; private set; }

        public override string ToString()
        {
            return $"{MovementType} creature {Name}: Health = {Health:f3}";
        }
        /// <summary>
        /// Переопределенное умножение.
        /// </summary>
        public static Creature operator *(Creature first, Creature second)
        {
            if (first.MovementType != second.MovementType)
                throw new ArgumentException("Объекты нельзя перемножить, так как у них разные значения MovementType");
            return new Creature(
                GetName(first.Name, second.Name),
                first.MovementType,
                (first.Health + second.Health) / 2
                );
        }
        /// <summary>
        /// Получение имени при умножении.
        /// </summary>
        /// <param name="first">Первое имя.</param>
        /// <param name="second">Второе имя.</param>
        /// <returns>Перемноженное имя.</returns>
        private static string GetName(string first, string second)
        {
            if (first.Length >= second.Length)
            {
                return first.Substring(0, (int)Math.Ceiling(first.Length / 2.0)) + second.Substring((int)Math.Ceiling(second.Length / 2.0));

            }
            else
            {
                return second.Substring(0, (int)Math.Ceiling(second.Length / 2.0)) + first.Substring((int)Math.Ceiling(first.Length / 2.0));
            }
        }
        public override bool Equals(object obj)
        {
            var other = obj as Creature;
            return Name==other.Name&&MovementType==other.MovementType&&Health==other.Health;
        }
    }
}
