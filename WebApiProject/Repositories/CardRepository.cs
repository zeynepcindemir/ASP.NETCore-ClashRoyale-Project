using Dapper;
using System.Data;
using WebApiProject.Connection;
using WebApiProject.Dto;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public class CardRepository : ICardRepository
    {

        private readonly IDatabaseConnectionFactory _connectionFactory;

        public CardRepository(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        private int GetSpeedId(string speedName, IDbConnection connection)
        {
            string query = "SELECT Id FROM Speeds WHERE Speed = @SpeedName";
            var parameters = new { SpeedName = speedName };
            int speedId = connection.QuerySingleOrDefault<int>(query, parameters);
            return speedId;
        }

        private int GetElixirUsageId(int elixirUsage, IDbConnection connection)
        {
            string query = "SELECT Id FROM ElixirUsages WHERE ElixirUsage = @ElixirUsage";
            var parameters = new { ElixirUsage = elixirUsage };
            int elixirUsageId = connection.QuerySingleOrDefault<int>(query, parameters);
            return elixirUsageId;
        }

        private int GetTargetId(string targetName, IDbConnection connection)
        {
            string query = "SELECT Id FROM Targets WHERE Target_ = @TargetName";
            var parameters = new { TargetName = targetName };
            int targetId = connection.QuerySingleOrDefault<int>(query, parameters);
            return targetId;
        }

        private int GetTypeId(string typeName, IDbConnection connection)
        {
            string query = "SELECT Id FROM Types WHERE Type_ = @TypeName";
            var parameters = new { TypeName = typeName };
            int typeId = connection.QuerySingleOrDefault<int>(query, parameters);
            return typeId;
        }

        private int GetRarityId(string rarityName, IDbConnection connection)
        {
            string query = "SELECT Id FROM Rarities WHERE Rarity = @RarityName";
            var parameters = new { RarityName = rarityName };
            int rarityId = connection.QuerySingleOrDefault<int>(query, parameters);
            return rarityId;
        }

        public async Task<Card> CreateCard(CreateCardDto card)
        {
            var query1 = @"
                 INSERT INTO Cards (Name, Description,Count, HitSpeed, SpeedId, ElixirUsageId, RarityId, TargetId, TypeId)
                 VALUES (@Name, @Description, @Count, @HitSpeed, @SpeedId, @ElixirUsageId, @RarityId, @TargetId, @TypeId);
                 SELECT CAST(SCOPE_IDENTITY() as int)";


            using (var connection = _connectionFactory.CreateConnection())
            {
                // service layer
                int speedId = GetSpeedId(card.Speed.Speed_, connection);
                int elixirUsageId = GetElixirUsageId(card.ElixirUsage.ElixirUsage_, connection);
                int targetId = GetTargetId(card.Target.Target_, connection);
                int typeId = GetTypeId(card.Type.Type_, connection);
                int rarityId = GetRarityId(card.Rarity.Rarity_, connection);

                var parameters = new DynamicParameters();
                parameters.Add("Name", card.Name, DbType.String);
                parameters.Add("Description", card.Description, DbType.String);
                parameters.Add("Count", card.Count, DbType.Int32);
                parameters.Add("HitSpeed", card.HitSpeed, DbType.Decimal);

                parameters.Add("SpeedId", speedId, DbType.Int32);
                parameters.Add("ElixirUsageId", elixirUsageId, DbType.Int32);
                parameters.Add("RarityId", rarityId, DbType.Int32);
                parameters.Add("TargetId", targetId, DbType.Int32);
                parameters.Add("TypeId", typeId, DbType.Int32);

                var id = await connection.QuerySingleAsync<int>(query1, parameters);

                var createdCard = new Card
                {
                    Id = id,
                    Name = card.Name,
                    Description = card.Description,
                    Count = card.Count,
                    HitSpeed = card.HitSpeed,
                    RarityId = rarityId,
                    TargetId = targetId,
                    SpeedId = speedId,
                    ElixirUsageId = elixirUsageId,
                    TypeId = typeId
                };

                return createdCard;
            }
        }




        public async Task DeleteCard(int Id)
        {
            var query = "DELETE FROM Cards WHERE Id = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id });
            }
        }

        public async Task<IEnumerable<Card>> GetAllCards()
        {
            var query = "SELECT * FROM Cards";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var cards = await connection.QueryAsync<Card>(query);
                return cards.ToList();
            }
        }

        public async Task<Card> GetCardById(int Id)
        {
            var query = "SELECT * FROM Cards WHERE Id = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var card = await connection.QuerySingleOrDefaultAsync<Card>(query, new { Id });
                return card;
            }
        }

        public async Task UpdateCard(int Id, UpdateCardDto card)
        {
            var query = @"
                        UPDATE Cards
                        SET Name = @Name, Description = @Description, Count = @Count, HitSpeed = @HitSpeed, SpeedId = @SpeedId, ElixirUsageId = @ElixirUsageId, RarityId = @RarityId, TargetId = @TargetId,  TypeId = @TypeId
                        WHERE Id = @Id";

            using (var connection = _connectionFactory.CreateConnection())
            {
                int speedId = GetSpeedId(card.Speed.Speed_, connection);
                int elixirUsageId = GetElixirUsageId(card.ElixirUsage.ElixirUsage_, connection);
                int targetId = GetTargetId(card.Target.Target_, connection);
                int typeId = GetTypeId(card.Type.Type_, connection);
                int rarityId = GetRarityId(card.Rarity.Rarity_, connection);

                var parameters = new DynamicParameters();
                parameters.Add("Id", Id, DbType.Int32);
                parameters.Add("Name", card.Name, DbType.String);
                parameters.Add("Description", card.Description, DbType.String);
                parameters.Add("Count", card.Count, DbType.Int32);
                parameters.Add("HitSpeed", card.HitSpeed, DbType.Decimal);

                parameters.Add("SpeedId", speedId, DbType.Int32);
                parameters.Add("ElixirUsageId", elixirUsageId, DbType.Int32);
                parameters.Add("RarityId", rarityId, DbType.Int32);
                parameters.Add("TargetId", targetId, DbType.Int32);
                parameters.Add("TypeId", typeId, DbType.Int32);

                await connection.ExecuteAsync(query, parameters);

            }
        }

    }
}
