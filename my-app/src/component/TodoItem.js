import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
//import '../App.css'


const TodoItem = () => {

    const [inputData, setInputData]= useState(''); 
    const [name,setName]=useState("");
    const [items, setItems]= useState([]); 
    const addItem = ()=>{
        if(!inputData){

        }else{
            setItems([...items,inputData]);
            setInputData(' ')
        }
    }
    
    let navigate=useNavigate();
    const Backpage=()=>
    {
        navigate('/Todo');
    }

    return(
    <div className='container'>
         <label> Add list</label><br></br>
         
              <input type='text' placeholder="Add items" value={inputData} onChange={ (e) => setInputData(e.target.value)}/>
              <button   onClick={addItem}>Add</button>
               {
                  items.map((elem)=>{
                    
                       return(
                        //<div className='eachItems' key={ind}>
                         <p>{elem}</p>
                       )
                   })

                }
                <br></br>
              <button type="Submit" onClick={Backpage}>Submit</button> 

    </div>
    )
}
 export default TodoItem