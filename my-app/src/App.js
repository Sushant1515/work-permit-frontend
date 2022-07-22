//  import logo from './logo.svg';
//   import './App.css';
//   import TodoItem from './component/TodoItem';
//   function App() {
//     return (
//       <div>
//         <TodoItem />
//       </div>
//     );
//     }
//   export default App;
/*import ReactDOM from "react-dom/client";
  import Todo from "./component/Todo";
  import Completed from "./component/Completed";
  import TodoItem from "./component/TodoItem";
  import Page from "./component/Routing";
  import Header from "./component/Header";
  import Home from "./component/Home";
  import Login from "./component/Login";
  import {BrowserRouter,Routes,Route,Link} from "react-router-dom";
  const root = ReactDOM.createRoot(document.getElementById("root"));
  function App(){
    return(
    <BrowserRouter>
    <Header/>
      <Routes>
            <Route path="/Todo" element={<Todo />}/>
            <Route path="/Completed" element={<Completed />}/>
            <Route path="/TodoItem" element={<TodoItem />}/>
            <Route path="/page" element={<Page />} /> 
            <Route path="/Home" element={<Home />} />    
            <Route path="/Login/:name"element={<Login />}/>   
      </Routes>
    </BrowserRouter>
    );
  }
  export default App*/
// import React, { useState } from "react";
// import UserTable from "./component/UserTable";
// import EditUserForm from "./component/EditUserForm";

// const App = () => {
//   const usersData = [
//     { id: 1, name: "Tania", username: "floppydiskette" },
//     { id: 2, name: "Craig", username: "siliconeidolon" },
//     { id: 3, name: "Ben", username: "benisphere" }
//   ];
//   const initialFormState = { id: null, name: "", username: "" };

//   const [users, setUsers] = useState(usersData);
//   const [editing, setEditing] = useState(false);
//   const [currentUser, setCurrentUser] = useState(initialFormState);

//   const addUser = (user) => {
//     user.id = users.length + 1;
//     setUsers([...users, user]);
//   };

//   const deleteUser = (id) => {
//     setEditing(false);
//     setUsers(users.filter((user) => user.id !== id));
//   };

//   const editRow = (user) => {
//     setEditing(true);

//     setCurrentUser(user);
//   };

//   const updateUser = (id, updatedUser) => {
//     setEditing(false);
//     setUsers(users.map((user) => (user.id === id ? updatedUser : user)));
//   };

//   return (
//     <div className="container">
//       <h1>CRUD App with Hooks</h1>
//       <div className="flex-row">
//         <div className="flex-large">
//           <div>
//             <h2>{editing ? "Edit user" : "Add user"}</h2>
//             <EditUserForm
//               editing={editing}
//               setEditing={setEditing}
//               currentUser={currentUser}
//               setCurrentUser={setCurrentUser}
//               updateUser={updateUser}
//               addUser={addUser}
//             />
//           </div>
//         </div>
//         <div className="flex-large">
//           <h2>View users</h2>
//           <UserTable users={users} editRow={editRow} deleteUser={deleteUser} />
//         </div>
//       </div>
//     </div>
//   );
// };

// export default App;
import "./App.css";
import Todo from "./component/Todo";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import TodoList from "./component/TodoList";
import TodoTable from "./component/TodoTable";
import TodoItem from "./component/TodoItem";
import PracticeTodo from "./component/PracticeTodo";
import Grid from "./component/Grid";
import Header from "./component/Header";
import AgGrid from "./component/AgGrid";
import Table from "./component/Table";
import Table1 from "./component/Table1";
import TableChild from "./component/TableChild";
import EditBtn from "./component/EditBtn";
function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Todo />}></Route>
        <Route path="/TodoList" element={<TodoList />}></Route>
        <Route path="/TodoTable" element={<TodoTable />}></Route>
        <Route path="/TodoItem" element={<TodoItem />}></Route>
        <Route path="/PracticeTodo" element={<PracticeTodo />}></Route>
        <Route path="/Grid" element={<Grid />}></Route>
        <Route path="/AgGrid" element={<AgGrid />}></Route>
        <Route path="/Table" element={<Table />}></Route>
        <Route path="/Table1" element={<Table1 />}></Route>
        <Route path="/EditBtn" element={<EditBtn />}></Route>
        <Route path="/TableChild" element={<TableChild />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
