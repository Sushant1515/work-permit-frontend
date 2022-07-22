import * as React from "react";
import {
  GridColumnMenuFilter,
  GridColumnMenuCheckboxFilter,
} from "@progress/kendo-react-grid";
import product from "./product.json";
export const ColumnMenu = (props) => {
  return (
    <div>
      <GridColumnMenuFilter {...props} />
    </div>
  );
};
export const ColumnMenuCheckboxFilter = (props) => {
  return (
    <div>
      <GridColumnMenuCheckboxFilter {...props} data={product} />
    </div>
  );
};
