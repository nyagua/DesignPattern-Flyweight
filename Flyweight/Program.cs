using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Flyweight
{
    public interface IPlayer
    {
        void AssignWeapon(string weapon);
        void Mission();
    }

    public class Terrorist : IPlayer
    {
        private string _task;
        private string _weapon;

        public Terrorist()
        {
            _task = "colocar bomba";
        }

        public void AssignWeapon(string weapon)
        {
            _weapon = weapon;
        }

        public void Mission()
        {
            Console.WriteLine($"El terrorista con el arma {_weapon} tiene la tarea de {_task}");
        }
    }

    public class CounterTerrorist : IPlayer
    {
        private string _task;
        private string _weapon;

        public CounterTerrorist()
        {
            _task = "Desarmar bomba";
        }

        public void AssignWeapon(string weapon)
        {
            _weapon = weapon;
        }

        public void Mission()
        {
            Console.WriteLine($"El counterterrorista con el arma {_weapon} tiene la tarea de {_task}");
        }
    }

    public class Factory
    {
        Dictionary<string, IPlayer> _players = new Dictionary<string, IPlayer>();

        public int GetNumberOfInstance()
        {
            return _players.Count;
        }

        public IPlayer GetPlayer(string key)
        {
            IPlayer player = null;
            if (_players.ContainsKey(key))
            {
                player = _players[key];
            }
            else
            {
                switch (key)
                {
                    case "Terrorist":
                        player = new Terrorist();
                        break;
                    case "CounterTerrorist":
                        player = new CounterTerrorist();
                        break;
                }
                _players.Add(key, player);
            }
            return player;
        }
    }

    class Program
    {
        private static string[] playerType = { "Terrorist", "CounterTerrorist" };
        private static string[] weapons = { "Ak47", "Gut Knife", "M17", "Granade" };

        public static string GetPlayerRandom()
        {
            Random r = new Random();
            int rad = r.Next(playerType.Length);
            return playerType[rad];
        }

        public static string GetWeaponRandom()
        {
            Random r = new Random();
            int rad = r.Next(weapons.Length);
            return weapons[rad];
        }

        static void Main(string[] args)
        {
            Console.WriteLine("FLYWEIGHT" + '\n');
            Console.WriteLine("Sirve para controlar la creacion de objetos. Se usa cuando la creacion de objetos es demasiado grande y necesitamos ir controlandola." + '\n');
            Console.WriteLine("Tiene un pool de objetos y cuando se necesitan los revisa si los tiene y sino los crea. Se utiliza cuando son objetos similares." + '\n');


            var factory = new Factory();
            for (int i = 0; i < 10; i++)
            {
                IPlayer p = factory.GetPlayer(GetPlayerRandom());
                p.AssignWeapon(GetWeaponRandom());
                p.Mission();
            }
            Console.WriteLine(factory.GetNumberOfInstance());

            Console.ReadLine();
        }
    }
}