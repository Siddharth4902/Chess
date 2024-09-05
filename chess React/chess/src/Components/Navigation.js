import { Link } from "react-router-dom";

export default function PlayerNavigation() {
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container-fluid">
        <h1 className="navbar-brand">Chess</h1>
        
    <div className="navbar-nav">
      <Link to="/" className="nav-link">
        Home
      </Link>
      <Link to="/players" className="nav-link " >Performance</Link>
      <Link to="/aboveavg" className="nav-link">Above average</Link>
      <Link to="/country" className="nav-link">Get by country</Link>
      <Link to="/add" className="nav-link" >Add Match</Link>
      </div>
      </div>
      
    </nav>
  );
}
