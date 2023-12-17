using MongoDB.Bson;
using MongoDB.Driver;
using Movies.Models;
using System;
using System.Diagnostics;

namespace Movies.Repository
{
    public class PlayerRepository
    {
        private readonly StoreVideoContext _context;

        public PlayerRepository()
        {
            _context = new StoreVideoContext();
        }

        public IEnumerable<Player> GetByTitle(string id)
        {
            return _context.CollectionPlayer.Find(p => p.Title.Equals("a")).ToList();
        }

        public void CreatePlayer(Player player)
        {
            _context.CollectionPlayer.InsertOne(player);
        }

    }
}
