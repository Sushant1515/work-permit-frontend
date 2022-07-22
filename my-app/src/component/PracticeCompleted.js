import React, { Component } from 'react'

const PracticeCompleted =(props)=> {
   
    return (
      <div>
        <h3>PracticeCompleted</h3>
        <table>
        <thead>
         <tr>
             <th>Item Name</th>
             <th>Edit</th>
             <th>Delete</th>
           </tr>
         </thead>
         <tbody>
                    {props.tabledata.length > 0 ? (props.tabledata.map((item,id) => {
                        return (
                            <tr key={id}>
                               <td>
                                {item.name}
                               </td>
                                <td>
                                    <button type="button" onClick={() => { props.editRow(item)}}>Edit</button>
                                </td>
                                <td>
                                    <button type="button" onClick={() => {props.DeleteItem(item.id)}}>Delete</button>
                                </td>
                            </tr>
                        );
                    })
                    ) : (
                        <tr>
                            <td colSpan={3}>No Massage</td>
                        </tr>
                    )}

                </tbody>
        </table>
      </div>
    )
  
}

export default PracticeCompleted