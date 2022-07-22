import '../App.css'
import React from "react";
import {useLocation ,useNavigate} from 'react-router-dom';
const TodoTable = ()=>{
    const location = useLocation();
    const data = location.state;
    const navigate = useNavigate()
    const handalGotoPage = ()=>{
        navigate("/",{state:data})
     }
return(
    <div className="container">
        <h3><button type='button' onClick={handalGotoPage} className="Todo">Home</button></h3>
        <h3>Todo List</h3> 
    <table>
    <thead>
        <tr>
            <th>Make</th>
            <th>Modal</th>
            <th>price</th>
        </tr>
    </thead>
        <tbody>
        {data.length > 0 ? (data.map((item, i) => {
                    return (
                    
                        <tr key={i}>
                            <td>
                                {item.id}
                            </td>
                            <td>
                                {item.name}
                            </td>
                            <td>
                                {item.status}
                            </td>
                        </tr>                        
                    );
                })
                ) : (
                    <tr>
                        <td colSpan={3}>No Msg</td>
                    </tr>
                )}
        </tbody>
        </table>
    </div>
)
}

export default TodoTable