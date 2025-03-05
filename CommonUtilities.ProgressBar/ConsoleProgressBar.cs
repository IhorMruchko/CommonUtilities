using System;
using System.Collections;

namespace CommonUtilities.ProgressBar
{
    public class ConsoleProgressBar : ProgressBar
    {
        private (int CursorLeft , int CursorTop) _prev;
        private int _lockPos;
        public ConsoleProgressBar(IEnumerable source) : base(source)
        {
            OnEnter += _OnEnter;
            OnExit += _OnExit;
        }

        private void _OnExit(ProgressInfoArgs infoargs)
        {
        }

        private void _OnEnter(ProgressInfoArgs infoargs)
        {
            if (infoargs.CurrentStage == 1)
            {
                _lockPos = Console.CursorTop;
            }
            Console.WriteLine($"Lock pos: {_lockPos}");
            Console.WriteLine($"Current Cursor top: {Console.CursorTop}");
            
            _prev.CursorTop = Console.CursorTop;
            _prev.CursorLeft = Console.CursorLeft;
            Console.CursorTop = _lockPos;
            Console.CursorLeft = 0;
            Console.Write($"Current Cursor top222: {Console.CursorTop}");
            Console.WriteLine(new string('|', (int)infoargs.Percentage) +
                          new string('_', (int)(100 - infoargs.Percentage)));
            Console.CursorTop = _prev.CursorTop + (infoargs.CurrentStage == 1 ? 1 : 0);
            Console.CursorLeft = _prev.CursorLeft;
        }
    }
}