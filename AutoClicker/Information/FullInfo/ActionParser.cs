using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Information.FullInfo.Actions;

namespace AutoClicker.Information.FullInfo
{
    public static class ActionParser
    {
        private static IAction ResolveAction(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Sleep:
                    return new SleepAction();
                case ActionType.Key:
                    return new KeyAction();
                case ActionType.Mouse:
                    return new MouseAction();
                case ActionType.Move:
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

                if (!Enum.TryParse(content[0], true, out ActionType actionType))
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
