import { useState, useEffect } from "react";
import { getCountry } from "../Services/ChessApiServices";

const CountryList = () => {
  const [country, setCountry] = useState("");
  const [countryList, setcountryList] = useState([]);

  const buttonHandler = async (e) => {
    console.log("Country:" + country);

    e.preventDefault();
    const getdata = async () => {
      let data3 = await getCountry(country);
      if (data3 != null) {
        setcountryList(data3);
      }
    };

    if (country == "") {
      alert("Enter a country");
    } else {
      getdata();
    }
  };

  return (
    <>
      <form className="form-group">
        <div>
          Country:
          <input
            className="form-control"
            onChange={(e) => {
              setCountry(e.target.value);
            }}
            type="text"
            placeholder="eg. USA"
          />
        </div>

        <button
          className="btn btn-primary m-2 p-3"
          type="submit"
          onClick={buttonHandler}
        >
          Submit
        </button>
      </form>
      <h1>List of Matches</h1>

      <table className="table table-striped">
        <thead className="thead-dark">
          <tr>
            <th>#</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Country</th>
            <th>World Ranking</th>
            <th>Total Matches</th>
          </tr>
        </thead>
        <tbody>
          {countryList.map((q, i) => (
            <tr key={i}>
              <td>{q.player_id}</td>
              <td>{q.first_name}</td>
              <td>{q.last_name}</td>
              <td>{q.country}</td>
              <td>{q.current_world_ranking}</td>
              <td>{q.total_matches_played}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};
export default CountryList;
