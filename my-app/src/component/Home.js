import React,{useState} from "react";
import {useNavigate} from "react-router-dom"; 

const Home =()=>{
    let nevigate=useNavigate();
    const [name,setName]=useState("");
    const Changepage = ()=>{
        nevigate(`/Login/${name}`)
    }; 

    const handlechange = (event) =>{
        setName(event.target.value);
    };

    return(
        <>
            <div> This is Home page </div>
            <input type="text" value={name} onChange={handlechange}/>
            <button onClick={Changepage}>Go to login </button>
        </>
        
    )
    }

export default Home;