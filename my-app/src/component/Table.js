import React, { useState, useRef, useEffect, useMemo, useCallback} from 'react';
import { render } from 'react-dom';
import { AgGridReact } from 'ag-grid-react'; // the AG Grid React Component

import 'ag-grid-community/dist/styles/ag-grid.css'; // Core grid CSS, always needed
import 'ag-grid-community/dist/styles/ag-theme-alpine.css'; // Optional theme CSS
import { Color } from 'ag-grid-community';

const Table=()=>{
    const data=[
        {name:'Dan',age:20},
        {name:'Max',age:23},
        {name:'David',age:24},
        {name:'Dan',age:27},
    ]

    const columns=[
        {
            headerName:"Name",field:'name',
        },
        {
            headerName:"Age",field:'age',
        },
    ]
    const defaultColDef={
        sortable:true,editable:true,filter:true,floatingFilter:true,flex:1
    }
    let gridApi;
    const onGridReady=params=>{
        gridApi=params.api
    }
    const onExportClick=()=>{
        gridApi.exportDataAsCsv();
    }
    return(
        <div>
        <button type="button"onClick={()=>onExportClick()}>Export</button>
        <div className="ag-theme-alpine"
        style={{
            height:300,
            width:'100%'}}>
            <AgGridReact rowData={data} columnDefs={columns} defaultColDef={defaultColDef}
            onGridReady={onGridReady}/>
        </div>
        </div>
    )
};
export default Table