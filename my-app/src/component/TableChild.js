import "../App.css";
// import React, {
//   useState,
//   useRef,
//   useEffect,
//   useMemo,
//   useCallback,
// } from "react";
import { render } from "react-dom";
import { AgGridReact } from "ag-grid-react"; // the AG Grid React Component

import "ag-grid-community/dist/styles/ag-grid.css"; // Core grid CSS, always needed
import "ag-grid-community/dist/styles/ag-theme-alpine.css"; // Optional theme CSS
//import { useNavigate } from 'react-router-dom';
//import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
const TableChild = () => {
  const location = useLocation();
  const data = location.state;
  console.log("this is", data);
  const navigate = useNavigate();
  const handalGotoPage = () => {
    navigate("/Table1", { state: data });
  };
  //console.log("it is", Object.entries(data));
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
            <th>make</th>
            <th>modal</th>
            <th>price</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{data.id}</td>
            <td>{data.make}</td>
            <td>{data.model}</td>
            <td>{data.price}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default TableChild;
