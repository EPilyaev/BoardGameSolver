### BoardGameSolver

#### Rules:
Game has board (N x N)  
Board initializes with Holes and Balls, located on a field. Each Hole and Ball are numerated.  
User can move Board in 4 directions [Top, Bottom, Right, Left].  
The aim of Game is to hit every Ball to the corresponding Hole using sequence of Board movements. When Ball hits wrong Hole - game is lost.  
Implemented algorithmm search most optimal sequence of movements to solve the game. If game has no solution - algorithm detects this on early stage and stop executing.

#### About application
Application has one build-in board state. Board is 10x10 with 3 balls and 3 holes. You can change this in the code.  
When application is started, it draws this initial state.  
Then, application searches for solution and after some time it displays the result which includes:  
- Count of moves performed to find a path.  
- List of moves to make to win the game.  
- Detailed move (with board drawn on each step).  

#### Comments:
I assumed that move means "move a board in the selected direction so balls can travel 1 cell in that direction".  
Unit tests in the solution are mainly added for the "presentation" purpose. It definitely can be fully covered with tests but I don't have enough time for it unfortunately.  
The used algorithm is breadth-first search. It's not the best solution for this task in terms of performance. Parallelization and better algorithms can be applied to improve execution time.