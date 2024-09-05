import { useState, useEffect } from "react";
import getPlayers from "../Services/ChessApiServices";

const PlayerList = () => {
  const [playerList, setPlayerList] = useState([]);
  useEffect(() => {
    const getdata = async () => {
      let data = await getPlayers();
      if (data != null) {
        setPlayerList(data);
      }
    };
    getdata();
  });
  return (
    <>
      <h1>List of Players : {playerList?.length}</h1>
      

        <table className='table table-striped' >
          <thead className='thead-dark'>
            <tr>
              <th >#</th>
              <th>Full Name</th>
              <th># Matches</th>
              <th >Wins</th>
              <th>Win Rate</th>
            </tr>
          </thead>
          <tbody >
          {playerList.map((p, i) => (
            <tr key={i}>
              <td>{p.player_id}</td>
              <td>{p.full_name}</td>
              <td>{p.total_matches}</td>
              <td>{p.total_wins}</td>
              <td>{p.win_Percentage}%</td>
              {/* <td>
                <button className="btn btn-danger"><FaTrashAlt /></button>
              </td> */}
            </tr>
          ))}
          </tbody>
        </table>
       
      
    </>
  );
};
export default PlayerList;
