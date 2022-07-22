import { Outlet, Link } from "react-router-dom";

const Header = () => {
  return (

      <nav>
        <ul>
          <li><Link to="/Todo">Home</Link></li>
          <li><Link to="/Home">Blogs</Link></li>
          <li><Link to="/TodoItem">Contact</Link></li>
        </ul>
      </nav>
    
  )
};

export default Header;