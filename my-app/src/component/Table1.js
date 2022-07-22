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
import { useNavigate, useLocation } from "react-router-dom";
import TableChild from "./TableChild";
//import { CellRendererComponent } from "ag-grid-community/dist/lib/components/framework/componentTypes";

function Table1() {
  const navigate = useNavigate("");
  const [item, setItem] = useState("");
  const location = useLocation();
  const data = location.state;
  //console.log("Data is", location);
  useEffect(() => {
    editRowdata(data.id, data);
  }, []);
  console.log("idis", data.id);
  const columnDefs = [
    { headerName: "Make", field: "make" },
    { headerName: "Model", field: "model" },
    { headerName: "price", field: "price" },
    {
      headerName: "Action",
      field: "price",
      cellRendererFramework: (params) => (
        <div>
          <button
            type="button"
            onClick={() => viewBtnClick(params)}
            className="Todo"
          >
            View
          </button>
        </div>
      ),
    },
    {
      headerName: "Action",
      field: "price",
      cellRendererFramework: (params) => (
        <div>
          <button
            type="button"
            className="Todo"
            onClick={() => editRow(params)}
          >
            Edit
          </button>
        </div>
      ),
    },
    {
      headerName: "Action",
      field: "price",
      cellRendererFramework: (params) => (
        <div>
          <button
            type="button"
            className="Todo"
            onClick={() => DeleteItem(params)}
          >
            Delete
          </button>
        </div>
      ),
    },
  ];
  const [rowData, setrowData] = useState([
    { id: 1, make: "Toyota", model: "Celica", price: 35000 },
    { id: 2, make: "Ford", model: "Mondeo", price: 32000 },
    { id: 3, make: "Porche", model: "Boxter", price: 72000 },
  ]);
  const viewBtnClick = (params) => {
    handalGotoPage(params.data);
  };

  const DeleteItem = (params) => {
    //const raw = Object.entries(user);
    //console.log("hii", params.data.id);
    //console.log("simple", rowData.id);
    setrowData(rowData.filter((element) => element.id !== params.data.id));
  };
  const editRow = (params) => {
    handalgotoPage(params.data);
  };
  const editRowdata = (id, updateditem) => {
    setrowData(rowData.map((elem) => (elem.id === id ? updateditem : elem)));
  };
  const defaultColDef = {
    sortable: true,
    filter: true,
  };
  const handalGotoPage = (Data) => {
    navigate("/TableChild", { state: Data });
  };
  const handalgotoPage = (Data) => {
    navigate("/EditBtn", { state: Data });
  };

  return (
    <div
      className="ag-theme-alpine"
      style={{
        height: 300,
        width: "100%",
      }}
    >
      <AgGridReact
        rowData={rowData}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
      />
    </div>
  );
}
export default Table1;
