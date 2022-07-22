import "../App.css";
import React from "react";
import { useState } from "react";
import TodoList from "./TodoList";
import { useLocation, useNavigate } from "react-router-dom";
import Completed from "./Completed";
const Todo = () => {
  const navigate = useNavigate("");
  const [item, setItem] = useState("");
  const [isHide, setIsHide] = useState(true);
  const [tableData, setTableData] = useState([]);
  const [editData, setEditData] = useState(null);

  const handalChange = (event) => {
    console.log("this is", event);
    setItem(event.target.value);
  };

  const addItem = () => {
    var id = tableData.length + 1;
    setTableData([...tableData, { id, name: item, status: "todo" }]);
    setItem("");
    console.log(tableData);
  };

  const deleteItem = (id) => {
    setTableData(tableData.filter((element) => element.id !== id));
  };

  const editRow = (user) => {
    console.log("item", user);
    setItem(user.name);
    console.log("link", user.name);
    setIsHide(false);
    setEditData(user);
  };

  const updateItem = () => {
    console.log("Edit", editData);

    const newTableData = tableData.map((element) => {
      if (element.id === editData.id) {
        return { ...element, name: item };
      } else {
        return element;
      }
    });

    setTableData(newTableData);
    setItem("");
    setIsHide(true);
  };
  const checkBoxClick = (id) => {
    const newTableData = tableData.map((element) => {
      if (element.id === id) {
        return { ...element, status: "completed" };
      } else {
        return element;
      }
    });
    setTableData(newTableData);
  };
  const uncheck = (id) => {
    const newTableData = tableData.map((element) => {
      if (element.id === id) {
        return { ...element, status: "todo" };
      } else {
        return element;
      }
    });
    setTableData(newTableData);
  };
  const handalGotoPage = () => {
    navigate("/TodoTable", { state: tableData });
  };
  return (
    <>
      <div className="container">
        <p>
          <h3>Add Item</h3>
          <input
            id="new_task"
            type="text"
            onChange={handalChange}
            value={item}
          />
          {isHide ? (
            <button type="button" onClick={addItem}>
              Add
            </button>
          ) : (
            <button type="button" onClick={updateItem}>
              Update
            </button>
          )}
        </p>
        <h3>
          <button type="button" onClick={handalGotoPage} className="Todo">
            TodoList
          </button>
        </h3>
        <TodoList
          tableData={tableData}
          editRow={editRow}
          deleteItem={deleteItem}
          checkBoxClick={checkBoxClick}
        />
        <Completed
          tableData={tableData}
          editRow={editRow}
          deleteItem={deleteItem}
          uncheck={uncheck}
        />
      </div>
    </>
  );
};

export default Todo;
