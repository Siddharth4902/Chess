import { useState, useEffect } from "react";
import { getWinners } from "../Services/ChessApiServices";

const WinnerList = () => {
  const [winnerList, setWinnerList] = useState([]);
  useEffect(() => {
    const getdata = async () => {
      let data1 = await getWinners();
      if (data1 != null) {
        setWinnerList(data1);
      }
    };
    getdata();
  });
  return (
    <>
      <h1>List of Players : {winnerList?.length}</h1>
      

        <table className='table table-striped' >
          <thead className='thead-dark'>
            <tr>
              <th >#</th>
              <th>Full Name</th>
              <th>#Matches</th>
              <th >Wins</th>
              <th>Win Rate</th>
            </tr>
          </thead>
          <tbody >
          {winnerList.map((p, i) => (
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
export default WinnerList;
