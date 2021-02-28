using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace PlayerClickers
{

    public abstract class PlayerClicker2
    {
        public abstract string ClickerName { get; }
        public abstract string ClickerType { get; }
        public abstract string Description { get; }
        public abstract BigNumber Click(int level);       
        public abstract void DelayedDestroy(GameObject effect);

        public bool Activated { get; protected set; }
        public bool Unlocked { get; protected set; }
        public void Deactivate() => Activated = false;
        public void Unlock(bool unlock) => Unlocked = unlock;
    }

    public interface IClickerTimer
    {
        float Timer { get; }
        float Timer2 { get; set; }        
        bool TimerInEffect { get; set; }
        IEnumerator ClickTimer();
    }

    public class PlayerClickerFactory
    {
        private static Dictionary<string, Type> clickersByName;
        private static bool IsInitialized => clickersByName != null;

        private static void InitializeFactory()
        {
            //Check if this exists
            if (IsInitialized)
                return;

            //Create new dictionary
            clickersByName = new Dictionary<string, Type>();

            // Sets clickerTypes to Type[], then for each Type, create a temp instance and add to dictionary
            var clickerTypes = Assembly.GetAssembly(typeof(PlayerClicker2)).GetTypes().Where(tgt => tgt.IsClass && !tgt.IsAbstract && tgt.IsSubclassOf(typeof(PlayerClicker2)));
            foreach (var type in clickerTypes)
            {
                var temp = Activator.CreateInstance(type) as PlayerClicker2;
                clickersByName.Add(temp.ClickerType, type);
            }
        }

        public static PlayerClicker2 GetClicker(string input)
        {
            InitializeFactory();
            if (clickersByName.ContainsKey(input))
            {
                Type type = clickersByName[input];
                var clicker = Activator.CreateInstance(type) as PlayerClicker2;
                return clicker;
            }
            return null;
        }
    }
}


