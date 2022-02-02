using System;
using System.Collections.Generic;
using AutoClicker.Full.Actions;
using AutoClicker.Full.Types;

namespace AutoClicker.Full.Parsers;

public class ActionParser
{
    private readonly string _input;

    public ActionParser(string input) =>
        _input = input;
    
    public bool TryParse(out List<IAction> actionList)
    {
        actionList = new List<IAction>();

        var actions = _input
            .Trim('\n')
            .Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var actionInfo in actions)
        {
            var action = ParseAction(actionInfo);

            if (action is null)
                return false;

            actionList.Add(action);
        }

        // actionList.RemoveAt(actionList.Count - 1);

        return true;
    }

    private static IAction ParseAction(string actionInfo)
    {
        var content = actionInfo.Split('(', ')');

        if (content.Length != 3
            || !Enum.TryParse(content[0], true, out ActionType actionType))
            return null;

        IAction action = ResolveAction(actionType);

        return action.TryInitialize(content[1])
            ? action
            : null;
    }

    private static IAction ResolveAction(ActionType actionType) => 
        actionType switch 
        {
            ActionType.Sleep => new SleepAction(),
            ActionType.Key => new KeyAction(),
            ActionType.Mouse => new MouseAction(),
            ActionType.Move => new MoveAction(),
            _ => throw new ArgumentOutOfRangeException(
                $"Failed to parse {actionType} as action")
        };
}
