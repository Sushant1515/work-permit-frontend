import * as React from "react";
import { Form, FormElement } from "@progress/kendo-react-form";
import { Button } from "@progress/kendo-react-buttons";
import { ResumeData } from "./resumedata";
//import { axios, Post } from "react-axios";
import axios from "axios";
import { Card } from "react-bootstrap";
import { CenturyViewService } from "@progress/kendo-react-dateinputs/dist/npm/calendar/services";
const stepPages = [ResumeData];
const App = () => {
  const [WorkPermitconfig, setresult] = React.useState(["id:0", "text:null"]);
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
  //axios.post("http://localhost:3000",result)
  //   function Save_Json(values) {
  //     var WorkPermitconfig = { id: 0, text: values };
  //     $.ajax({
  //       url: "/Home/PostOrder_Main",
  //       type: "POST",
  //       data: JSON.stringify({ WorkPermitconfig: WorkPermitconfig }),
  //       dataType: "json",
  //       traditional: true,
  //       contentType: "application/json; charset=utf-8",
  //       success: function (data) {
  //         alert(data.msg);
  //       },
  //       error: function () {},
  //     });
  //   }
  // };
  //console.log("data is", result);
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
            Submit
          </Button>
          <Button type={"Cancal"} onClick={formRenderProps.onFormReset}>
            Cancel
          </Button>
        </FormElement>
      )}
    />
  );
};
export default App;
