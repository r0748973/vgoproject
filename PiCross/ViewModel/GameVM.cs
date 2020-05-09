using PiCross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GameVM
    {
        private readonly MainVM mainVM;
        public GameVM(MainVM mainVM)
        {
            
            playablePuzzle = new PiCrossFacade().CreatePlayablePuzzle(puzzle);
            PVM = new PuzzleVM(playablePuzzle);
            this.mainVM = mainVM;

        }

        private Puzzle puzzle = Puzzle.FromRowStrings(
                "xxxxx",
                "xx..x",
                "x...x",
                "x...x",
                "xxxxx"
            );
        public IPlayablePuzzle playablePuzzle { get; private set; }
        public PuzzleVM PVM { get; private set; }
    }
}
