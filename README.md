### BoardGameSolver

#### Rules:
Game has board (N x N)  
Board initializes with Holes and Balls, located on a field. Each Hole and Ball are numerated.  
User can move Board in 4 directions [Top, Bottom, Right, Left].  
The aim of Game is to hit every Ball to the corresponding Hole using sequence of Board movements. When Ball hits wrong Hole - game is lost.  
Implemented algorithmm search most optimal sequence of movements to solve the game. If game has no solution - algorithm detects this on early stage and stop executing.  

#### Comments:
I will assume that 1 move means "move a board in the selected direction so balls can travel 1 cell in that direction".  
Unit tests in the solution are mainly added for the "presentation" purpose. It definitely can be fully covered with tests but I don't have enough time for it unfortunately.
