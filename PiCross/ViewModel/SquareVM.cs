using Cells;
using PiCross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using System.Windows.Input;

namespace ViewModel
{
    public class SquareVM
    {
        private readonly IPlayablePuzzleSquare puzzleSquare;

        public SquareVM(IPlayablePuzzleSquare playablePuzzleSquare)
        {
            this.puzzleSquare = playablePuzzleSquare;
        }

        public Cell<Square> Contents => puzzleSquare.Contents;
        public ICommand Fill => new Fill(Contents);
        public ICommand Unknown => new Unknown(Contents);
        public ICommand Empty => new Empty(Contents);


    }
    public class Fill : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Cell<Square> content;
        public Fill(Cell<Square> content)
        {
            this.content= content;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            content.Value = Square.FILLED;
            
        }
    }
    public class Unknown : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Cell<Square> content;
        public Unknown(Cell<Square> content)
        {
            this.content = content;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            content.Value = Square.UNKNOWN;

        }
    }
    public class Empty : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Cell<Square> content;
        public Empty(Cell<Square> content)
        {
            this.content = content;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            content.Value = Square.EMPTY;

        }
    }
}
