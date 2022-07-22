import * as React from "react";
import { Form, FormElement } from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";
//import { RestData } from "./job-application-data";
//import { axios, Post } from "react-axios";
//import React from "react";
import axios from "axios";
import { Card } from "react-bootstrap";
import { CenturyViewService } from "@progress/kendo-react-dateinputs/dist/npm/calendar/services";
import { ResumeData } from "./resumedata";
import { useNavigate } from "react-router-dom";
const stepPages = [ResumeData];
const App1 = () => {
  const [result, setresult] = React.useState([]);
  let navigate = useNavigate();
  const Changepage = () => {
    navigate("/App2");
  };
  const handleSubmit = (dataItem) => {
    alert(JSON.stringify(dataItem, null, 2));
    const workPermitconfig = {
      id: "0",
      formtext: JSON.stringify(dataItem),
    };
    axios
      .post("https://localhost:5001/api/WorkPermit/wpcsave", workPermitconfig)

      .then((response) => {
        console.log("responce", response);
      })
      .catch((error) => {
        console.log("error", error);
      });
  };
  const [users, setUsers] = React.useState([]);
  const f = async () => {
    const res = await fetch("https://reqres.in/api/users/");
    const json = await res.json();
    setUsers(json.data);
  };
  React.useEffect(() => {
    f();
  }, []);

  return (
    <Form
      onSubmit={handleSubmit}
      render={(formRenderProps) => (
        <FormElement
          style={{
            width: 800,
          }}
          className="container"
        >
          {stepPages}
          {/* <div className="App">
            <h1>Hello ReqRes users!</h1>
            <div className="flex">
              {users.length &&
                users.map((user) => {
                  return (
                    <div key={user.id}>
                      <p>
                        <strong>{user.first_name}</strong>
                      </p>
                      <p>{user.email}</p>
                      <img key={user.avatar} src={user.avatar} />
                    </div>
                  );
                })}
            </div>
          </div> */}
          <Button type={"submit"} className="submitbtn">
            Create
          </Button>
          <Button type={"Cancal"} onClick={formRenderProps.onFormReset}>
            Cancel
          </Button>
        </FormElement>
      )}
    />
  );
};
export default App1;
