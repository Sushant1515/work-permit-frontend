import * as React from "react";
import * as ReactDOM from "react-dom";
import { process } from "@progress/kendo-data-query";
import { Grid, GridColumn as Column } from "@progress/kendo-react-grid";
import { ColumnMenu, ColumnMenuCheckboxFilter } from "./columnMenu";
import product from "./product.json";
import { IntlService } from "@progress/kendo-react-intl";
import { useInternationalization } from "@progress/kendo-react-intl";
import { Button } from "@progress/kendo-react-buttons";

const createDataState = (dataState) => {
  return {
    result: process(product.slice(0), dataState),
    dataState: dataState,
  };
};

const Datagrid = () => {
  const intl = useInternationalization();

  const locales = [
    {
      language: "en",
      locale: "en",
    },
    {
      language: "tr",
      locale: "tr",
    },
    {
      language: "id",
      locale: "id",
    },
  ];
  const [currentLocale, setCurrentLocale] = React.useState(locales[0]);

  let initialState = createDataState({
    take: 16,
    skip: 0,
  });
  const [result, setResult] = React.useState(initialState.result);
  console.log("result", result);
  const [dataState, setDataState] = React.useState(initialState.dataState);
  console.log("dataState", dataState);

  const dataStateChange = (event) => {
    let updatedState = createDataState(event.dataState);
    setResult(updatedState.result);
    setDataState(updatedState.dataState);
  };

  return (
    <div>
      <Button type="submit">NewPermit</Button>

      <Grid
        data={result}
        {...dataState}
        onDataStateChange={dataStateChange}
        sortable={true}
      >
        <Column
          field="Number"
          title={intl.NUMBER}
          filter={"numeric"}
          columnMenu={ColumnMenu}
        />
        <Column
          field="ProductName"
          title="TITLE"
          sortable={false}
          columnMenu={ColumnMenuCheckboxFilter}
        />
        <Column
          field="UnitPrice"
          title="VALID-FROM"
          filter={"numeric"}
          columnMenu={ColumnMenu}
        />
        <Column
          field="ProductName"
          title="VALID-TO"
          filter={"numeric"}
          columnMenu={ColumnMenuCheckboxFilter}
        />
        <Column
          field="UnitPrice"
          title="WORKFLOW-STATUS"
          filter={"numeric"}
          columnMenu={ColumnMenu}
        />
        <Column
          field="UnitPrice"
          title="Buttons"
          filter={"numeric"}
          columnMenu={ColumnMenu}
        />
      </Grid>
    </div>
  );
};

export default Datagrid;
