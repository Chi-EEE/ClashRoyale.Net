using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.GameLoop
{
    public class GameTime
    {
        private float _deltaTime = 0.0f;
        private float _timeScale = 1.0f;
        public float TimeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }
        public float DeltaTime
        {
            get { return _deltaTime * _timeScale; }
            set { _deltaTime = value; }
        }
        public float DeltaTimeUnscaled
        {
            get { return _deltaTime; }
        }
        public float TotalTimeElapsed
        {
            get;
            private set;
        }
        public void Update(float deltaTime, float totalTimeElapsed)
        {
            _deltaTime = deltaTime;
            TotalTimeElapsed = totalTimeElapsed;
        }
    }
}
