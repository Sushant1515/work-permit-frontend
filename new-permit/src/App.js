import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import New_workPermit from "./New_workPermit";
import CreateNewWorkpermit from "./CreateNewWorkpermit";
import NewPermit from "./New_Permit";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/New_workPermit" element={<New_workPermit />}></Route>
        <Route path="/NewPermit" element={<NewPermit />}></Route>
        <Route
          path="/CreateNewWorkpermit"
          element={<CreateNewWorkpermit />}
        ></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
