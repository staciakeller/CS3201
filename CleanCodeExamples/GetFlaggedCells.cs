// Version 1
private List<int[]> getThem()
{
   List<int[]> list1 = new ArrayList<int[]>();
   for (int[] x : theList)
   {
		if (x[0] == 4)
		list1.add(x);
   }

   return list1;
}

// Version 2
private List<int[]> getFlaggedCells()
{
   List<int[]> flaggedCells = new List<int[]>();
   for (int[] cell : gameBoard)
   {
		if (cell[STATUS_VALUE] == FLAGGED)
			flaggedCells.add(cell);
   }
   return flaggedCells;
}

// Version 3
private List<Cell> getFlaggedCells()
{
   List<Cell> flaggedCells = new List<Cell>();

   for (Cell cell : this.gameBoard)
   {
		if (cell.IsFlagged())
		{
			flaggedCells.Add(cell);
		}
   }

   return flaggedCells;
}


// Version 4
private ICollection<Cell> getFlaggedCells()
{
   var flaggedCells = new List<Cell>();

   for (Cell cell : this.gameBoard)
   {
		if (cell.IsFlagged())
		{
			flaggedCells.Add(cell);
		}
   }

   return flaggedCells;
}

// Version 5
private IEnumerable<Cell> getFlaggedCells()
{
   var flaggedCells = from cell in this.gameBoard where cell.IsFlagged() select cell;
   return flaggedCells;
}

// Version 6
private IEnumerable<Cell> getFlaggedCells()
{
   var flaggedCells = this.gameBoard.Where(cell => cell.IsFlagged());
   return flaggedCells;
}

// Version 7
private IEnumerable<Cell> getFlaggedCells()
{
   return this.gameBoard.Where(cell => cell.IsFlagged());
}



