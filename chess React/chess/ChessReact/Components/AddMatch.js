import { useState } from "react";
import { addMatch } from "../Services/ChessApiServices";


const AddMatch = () => {
  const [match, setMatch] = useState({
    player1_id: "",
    player2_id: "",
    match_date: "",
    match_level: "",
    winner_id: "",
});

  const onChange = (e) => {
    setMatch({ ...match, [e.target.id]: e.target.value });
  };

  async function handleSubmit(e)  {
    e.preventDefault();
    const res = await addMatch(match);
    console.log(res);

    if (!res) {
      alert("Match added successfully");
    } else {
      alert("Failed to add Match");
    }
  };

  return (
    <>
      <h1>Add a new Match</h1>
      <form className="form-group" >
        <div>
          Player 1 ID:{" "}
          <input
            className="form-control"
            value={match.player1_id}
            onChange={onChange}
            type="number"
            id="player1_id"
          />
        </div>
        <div>
        Player 2 ID: {" "}
          <input
            className="form-control"
            value={match.player2_id}
            onChange={onChange}
            type="number"
            id="player2_id"
          />
        </div>
        <div>
          Match Date :{" "}
          <input
            className="form-control"
            value={match.match_date}
            onChange={onChange}
            type="text"
            id="match_date"
          />
        </div>
        <div>
          Match Level:{" "}
          <input
            className="form-control"
            value={match.match_level}
            onChange={onChange}
            type="text"
            id="match_level"
          />
        </div>
        <div>
          Winner ID :{" "}
          <input
            className="form-control"
            value={match.winner_id}
            onChange={onChange}
            type="number"
            id="winner_id"
          />
        </div>

        <button
          className="btn btn-primary m-2 p-3"
          type="submit"
          onClick={handleSubmit}
        >
          Add new Match
        </button>
      </form>
    </>
  );
};
export default AddMatch;
