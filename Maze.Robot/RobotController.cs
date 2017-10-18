using Maze.Library;

namespace Maze.Solver
{
    /// <summary>
    /// Moves a robot from its current position towards the exit of the maze
    /// </summary>
    public class RobotController
    {
        private IRobot robot;

        /// <summary>
        /// Initializes a new instance of the <see cref="RobotController"/> class
        /// </summary>
        /// <param name="robot">Robot that is controlled</param>
        public RobotController(IRobot robot)
        {
            // Store robot for later use
            this.robot = robot;
        }

        /// <summary>
        /// Moves the robot to the exit
        /// </summary>
        /// <remarks>
        /// This function uses methods of the robot that was passed into this class'
        /// constructor. It has to move the robot until the robot's event
        /// <see cref="IRobot.ReachedExit"/> is fired. If the algorithm finds out that
        /// the exit is not reachable, it has to call <see cref="IRobot.HaltAndCatchFire"/>
        /// and exit.
        /// </remarks>
        public void MoveRobotToExit()
        {
            // Try all directions
            if (!findExit(Direction.Right))
                if (!findExit(Direction.Left))
                    if (!findExit(Direction.Up))
                        if (!findExit(Direction.Down));
        }

        // Backtracker that moves the robot to the exit
        public bool findExit(Direction direction)
        {
            if (!robot.CanIMove(direction))
                return false;

            // Ending condition
            var reachedEnd = false;
            robot.ReachedExit += (_, __) => reachedEnd = true;

            if (reachedEnd)
                return true;

            robot.Move(direction);

            if (findExit(Direction.Right))
                return true;
            if (findExit(Direction.Left))
                return true;
            if (findExit(Direction.Up))
                return true;
            if (findExit(Direction.Down))
                return true;

            robot.HaltAndCatchFire();
            return false;
        }
    }
}
