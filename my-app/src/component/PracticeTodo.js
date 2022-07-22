import React, { Component, useState } from 'react'
import PracticeCompleted from './PracticeCompleted';

const PracticeTodo=()=> {

    const [item,setItem]=useState('');
    const[tabledata,setTableData]=useState([]);
    const [isHide,setIsHide] =useState(true);
    const [editData,setEditData]=useState(null);

    const handleonchange=(event)=>{
        setItem(event.target.value);
    }

    const addItem = () => {
      var id = tabledata.length + 1
      setTableData([...tabledata, { id, name: item }]);
      setItem('')
      console.log(tabledata)
  }

    const DeleteItem=(id)=>{
        //console.log("thisis",id)
        setTableData(tabledata.filter(element=>element.id!==id))

    }

    const editRow=(user)=>{

      console.log("item",user)
      setItem(user.name)
      console.log("link",user.name)
      setIsHide(false)
      setEditData(user)
    }
    const updateItem =()=>{
      console.log("Edit",editData)

   const newTableData = tabledata.map(element =>{
       if(element.id === editData.id){
           return {...element,name:item}
       }else{
           return element;
       }
   }) 
   
   setTableData(newTableData);
   setItem('')
   setIsHide(true)   

   }

    return (
      <>
      <div>
        <p>
      <input type="text" onChange={handleonchange} value={item}/>
      {isHide?<button type='button' onClick={addItem}>Add</button>:<button type="button" onClick={updateItem} >Update</button>} 
      </p>
       <PracticeCompleted tabledata={tabledata} DeleteItem={DeleteItem} editRow={editRow}/>
      </div>
      </>

    )

};

export default PracticeTodo