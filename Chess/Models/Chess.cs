using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Chess
    {
        public int match_id { get; set; }
        [Range(1, 10, ErrorMessage = "Select from player 1 - 10")]

        public int player1_id {  get; set; }
        [Range(1, 10, ErrorMessage = "Select from player 1 - 10")]

        public int player2_id { get; set; }

       //[DataType()]
        public DateTime match_date { get; set; }
        public string match_level { get; set; }
        [Range(1, 10, ErrorMessage = "Select from player 1 - 10")]
        public int winner_id {  get; set; }
        
    }
    public class Country
    {
        public int player_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string country { get; set; }
        public int current_world_ranking { get; set; }
        public int total_matches_played { get; set; }

    }

}
