import "../App.css";
import React, {
  useState,
  useRef,
  useEffect,
  useMemo,
  useCallback,
} from "react";
import { render } from "react-dom";
import { AgGridReact } from "ag-grid-react"; // the AG Grid React Component

import "ag-grid-community/dist/styles/ag-grid.css"; // Core grid CSS, always needed
import "ag-grid-community/dist/styles/ag-theme-alpine.css"; // Optional theme CSS
//import { useNavigate } from 'react-router-dom';
//import React from "react";
import { useLocation, useNavigate } from "react-router-dom";

const EditBtn = () => {
  const location = useLocation();
  const data = location.state;
  //console.log("this is", data);
  const navigate = useNavigate();
  const [item, setItem] = useState(data);
  //console.log("Item", item);
  const handalGotoPage = (data) => {
    navigate("/Table1", { state: data });
  };
  //console.log("it is", Object.entries(data));
  const handalChange = (event) => {
    const { name, value } = event.target;
    //console.log("this is", event);
    setItem({ ...item, [name]: value });
  };
  //console.log("edititem", item);
  const Submitext = (item) => {
    handalGotoPage(item);
  };

  return (
    <div className="container">
      <h3>
        <button type="button" onClick={handalGotoPage} className="Todo">
          Home
        </button>
      </h3>
      <h3>Todo List</h3>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <br></br>
            <input
              type="text"
              name="id"
              value={item.id}
              onChange={handalChange}
            />
            <th>make</th>
            <br></br>
            <input
              type="text"
              name="make"
              value={item.make}
              onChange={handalChange}
            />
            <th>modal</th>
            <br></br>
            <input
              type="text"
              name="model"
              value={item.model}
              onChange={handalChange}
            />
            <th>price</th>
            <br></br>
            <input
              type="text"
              name="price"
              value={item.price}
              onChange={handalChange}
            />
          </tr>
          <br></br>
          <button
            type="submit"
            className="Todo"
            onClick={() => Submitext(item)}
          >
            Submit
          </button>
        </thead>
      </table>
    </div>
  );
};

export default EditBtn;
