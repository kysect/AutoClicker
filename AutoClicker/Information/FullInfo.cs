using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using AutoClicker.WorkWithDll;
using AutoClicker.WorkWithDll.InputSimulation;

namespace AutoClicker.Information
{
    public enum Actions
    {
        Sleep = 1,
        Key = 2,
        Mouse = 3,
        Move = 4
    }

    public interface IAction
    {
        public bool TryParse(string input);
        public void Execute();
    }

    public class SleepAction : IAction
    {
        private int _sleepTimeMs;
        public bool TryParse(string input)
        {
            return int.TryParse(input, out _sleepTimeMs);
        }
        public void Execute()
        {
            Thread.Sleep(_sleepTimeMs);
        }
    }

    public class KeyAction : IAction
    {
        private Key _key;
        private bool _keyDown;
        public bool TryParse(string input)
        {
            var keyContent = input.Split(", ");
            if (keyContent.Length != 2)
                return false;

            if (!Enum.TryParse(keyContent[0], out _key))
                return false;
            
            switch(keyContent[1])
            {
                case "down":
                    _keyDown = true;
                    break;
                case "up":
                    _keyDown = false;
                    break;
                default:
                    return false;
            };
            return true;
        }
        public void Execute()
        {
            SimulateInput.KeyboardEvent(_key, _keyDown);
        }
    }

    public class MouseAction : IAction
    {
        private MouseMessages _mouseMessage;
        public bool TryParse(string input)
        {
            return Enum.TryParse(input, out _mouseMessage);
        }
        public void Execute()
        {
            SimulateInput.MouseEvent(_mouseMessage);
        }
    }

    public class MoveAction : IAction
    {
        private MousePoint _mousePoint;

        public bool TryParse(string input)
        {
            var coordsContent = input.Split(", ");
            if (coordsContent.Length != 2)
                return false;
            
            if (!(int.TryParse(coordsContent[0], out int x) && 
                  int.TryParse(coordsContent[1], out int y)))
                return false;

            _mousePoint = new MousePoint(x, y);
            return true;
        }

        public void Execute()
        {
            MouseCursor.SetCursorPosition(_mousePoint);
        }
    }

    public class Parser
    {
        private static IAction ResolveAction(Actions actionType)
        {
            switch (actionType)
            {
                case Actions.Sleep:
                    return new SleepAction();
                case Actions.Key:
                    return new KeyAction();
                case Actions.Mouse:
                    return new MouseAction();
                case Actions.Move:
                    return new MoveAction();
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Failed to parse {actionType} as action");
            }
        }
        public static bool TryParse(string input, out List<IAction> actionList)
        {
            actionList = new List<IAction>();

            var actions = input.Split(';', '\n');
            foreach (var actionInfo in actions)
            {
                if (actionInfo == "")
                    continue;

                var content = actionInfo.Split('(', ')');
                if (content.Length != 3)
                    return false;

                if (!Enum.TryParse(content[0], true, out Actions actionType))
                    return false;

                IAction action = ResolveAction(actionType);
                if (!action.TryParse(content[1]))
                    return false;

                actionList.Add(action);
            }

            actionList.RemoveAt(actionList.Count - 1);
            return true;
        }
    }
}
