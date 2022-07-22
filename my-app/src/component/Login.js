import React from "react";
import { useParams} from "react-router-dom";

const Login =()=>
{
    let params =useParams();

    return<div>Welcome to login page:{params.name}</div>
    
};

export default Login;