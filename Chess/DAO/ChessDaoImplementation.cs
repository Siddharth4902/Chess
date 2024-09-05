using Npgsql;
using WebApplication1.Models;
using System.Data;
using NpgsqlTypes;
using System.Numerics;

namespace WebApplication1.DAO
{
    public class ChessDaoImplementation: IChessDao
    {
        private readonly NpgsqlConnection _connection;

        public ChessDaoImplementation(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Chess>> GetMatches()
        {
            string query = @" select * from chess.Matches ";
            List<Chess> matches = new List<Chess>();
            Chess match = null;
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(query, _connection);
                    Command.CommandType = CommandType.Text;
                    //Command.Parameters.AddWithValue("@pid", id);
                    NpgsqlDataReader reader = await Command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            match = new Chess();
                            match.match_id = reader.GetInt32(0);
                            match.player1_id = reader.GetInt32(1);
                            match.player2_id = reader.GetInt32(2);
                            match.match_date = reader.GetDateTime(3);
                            match.match_level = reader.GetString(4);
                            match.winner_id = reader.GetInt32(5);
                            matches.Add(match);


                        }

                    }
                    reader?.Close();
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------Exception-----" + e.Message);
            }
            return matches;

        }


        public async Task<List<Performance>> GetPerformances()
        {
            string query = @" SELECT 
                                p.player_id,
                                p.first_name || ' ' || p.last_name AS full_name,
                                COUNT(m.match_id) AS total_matches,
                                COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) AS total_wins,
                                COALESCE(
                                    ROUND(
                                        (COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END)::decimal / COUNT(m.match_id)) * 100, 2
                                    ), 0
                                ) AS win_percentage
                            FROM chess.players p
                            LEFT JOIN chess.matches m ON p.player_id = m.player1_id OR p.player_id = m.player2_id
                            GROUP BY p.player_id, p.first_name, p.last_name
                            ORDER BY win_percentage desc, total_wins desc ; ";
            List<Performance> matches = new List<Performance>();
            Performance match = null;
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(query, _connection);
                    Command.CommandType = CommandType.Text;
                    //Command.Parameters.AddWithValue("@pid", id);
                    NpgsqlDataReader reader = await Command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            match = new Performance();
                            match.player_id = reader.GetInt32(0);
                            match.full_name = reader.GetString(1);
                            match.total_matches = reader.GetInt32(2);
                            match.total_wins = reader.GetInt32(3);
                            match.win_Percentage = reader.GetDouble(4);
                            matches.Add(match);


                        }

                    }
                    reader?.Close();
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------Exception-----" + e.Message);
            }
            return matches;

        }

        public async Task<List<Performance>> GetWinners()
        {
            string query = @" WITH average_wins AS (
                                SELECT AVG(total_wins) AS avg_wins
                                FROM (
                                    SELECT COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) AS total_wins
                                    FROM chess.players p
                                    LEFT JOIN chess.matches m ON p.player_id = m.player1_id OR p.player_id = m.player2_id
                                    GROUP BY p.player_id
                                )
                            )
                            SELECT 
                                p.player_id,
                                p.first_name || ' ' || p.last_name AS full_name,
                                COUNT(m.match_id) AS total_matches,
                                COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) AS total_wins,
                                COALESCE(
                                    ROUND(
                                        (COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END)::decimal / COUNT(m.match_id)) * 100, 2
                                    ), 0
                                ) AS win_percentage
                            FROM 
                                chess.players p
                            LEFT JOIN 
                                chess.matches m ON p.player_id = m.player1_id OR p.player_id = m.player2_id
                            GROUP BY 
                                p.player_id, p.first_name, p.last_name
                            HAVING 
                                COUNT(CASE WHEN m.winner_id = p.player_id THEN 1 END) > (SELECT avg_wins FROM average_wins)
                            ORDER BY 
                                total_wins DESC; ";
            List<Performance> matches = new List<Performance>();
            Performance match = null;
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(query, _connection);
                    Command.CommandType = CommandType.Text;
                    //Command.Parameters.AddWithValue("@pid", id);
                    NpgsqlDataReader reader = await Command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            match = new Performance();
                            match.player_id = reader.GetInt32(0);
                            match.full_name = reader.GetString(1);
                            match.total_matches = reader.GetInt32(2);
                            match.total_wins = reader.GetInt32(3);
                            match.win_Percentage = reader.GetDouble(4);
                            matches.Add(match);


                        }

                    }
                    reader?.Close();
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------Exception-----" + e.Message);
            }
            return matches;

        }
        public async Task<int> InsertMatch(Chess c)
        {
            int rowsInserted = 0;
            string message;
            string insertQuery = $"insert into chess.Matches ( player1_id, player2_id, match_date, match_level,winner_id) VALUES ({c.player1_id},{c.player2_id},'{c.match_date}','{c.match_level}',{c.winner_id})";
            Console.WriteLine("Query" + insertQuery);
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(insertQuery, _connection);
                    Command.CommandType = CommandType.Text;
                    rowsInserted = await Command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
                Console.WriteLine("------Exception-----" + message);
            }
            return rowsInserted;
        }
        public async Task<List<Country>> GetPlayerByCountry(string country)
        {
            string query = @" select * from chess.Players where country ilike @country order by current_world_ranking  ";
            List<Country> players = new List<Country>();
            Country player = null;
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand Command = new NpgsqlCommand(query, _connection);
                    Command.CommandType = CommandType.Text;
                    Command.Parameters.AddWithValue("country",country);
                    NpgsqlDataReader reader = await Command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            player = new Country();
                            player.player_id = reader.GetInt32(0);
                            player.first_name = reader.GetString(1);
                            player.last_name = reader.GetString(2);
                            player.country = reader.GetString(3);
                            player.current_world_ranking = reader.GetInt32(4);
                            player.total_matches_played = reader.GetInt32(5);
                            players.Add(player);


                        }

                    }
                    reader?.Close();
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("------Exception-----" + e.Message);
            }
            return players;

        }

        
    }
}
