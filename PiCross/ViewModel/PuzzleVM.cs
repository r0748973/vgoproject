using Cells;
using DataStructures;
using PiCross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class PuzzleVM
    {
        private readonly IPlayablePuzzle playablePuzzle;
        public PuzzleVM(IPlayablePuzzle playablePuzzle)
        {
            this.playablePuzzle = playablePuzzle;
        }
        public IGrid<SquareVM> Grid => playablePuzzle.Grid.Map(square => new SquareVM(square)).Copy();
        public ISequence<ConstraintsVM> RowConstraints => playablePuzzle.RowConstraints.Map(row => new ConstraintsVM(row)).Copy();
        public ISequence<ConstraintsVM> ColumnConstraints => playablePuzzle.ColumnConstraints.Map(col => new ConstraintsVM(col)).Copy();
    }

    public class ConstraintsVM
    {
        private readonly IPlayablePuzzleConstraints playablePuzzleConstraints;
        public ConstraintsVM(IPlayablePuzzleConstraints playablePuzzleConstraints)
        {
            this.playablePuzzleConstraints = playablePuzzleConstraints;
               
            
        }
        public object Values => playablePuzzleConstraints.Values;
        public Cell<bool> IsSatisfied => playablePuzzleConstraints.IsSatisfied;

    }

}
