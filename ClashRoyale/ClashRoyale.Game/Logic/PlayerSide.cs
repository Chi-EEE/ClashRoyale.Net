using ClashRoyale.Game.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic
{
    public class PlayerSide
    {
        public Card[] Deck { get; set; }
        public float Mana { get; set; }
        public float ManaDecimal { get; set; }
        public PlayerSide(Card[] deck)
        {
            Deck = deck;
        }
        public void Tick(GameTime gameTime)
        {
            GenerateMana(gameTime);
        }
        private void GenerateMana(GameTime gameTime)
        {
            ManaDecimal += gameTime.DeltaTime;
            if (ManaDecimal > 0.1f)
            {
                ManaDecimal -= 0.1f;
                Mana += 0.1f;
            }
        }
    }
}
