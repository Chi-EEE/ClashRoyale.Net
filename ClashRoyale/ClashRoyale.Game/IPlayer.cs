using System.Threading.Tasks;

namespace TicTacToe.Game
{
    public interface IPlayer
    {
        PlayerTypes Type { get; }
        //Task<int> MakeMove();
        //void GameEnded() { }
    }

    public enum PlayerTypes
    {
        Human,
        RandomBot,
        QLearning
    }
}